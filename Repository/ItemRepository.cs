using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Enums;
using System.Threading.Tasks;
using MongoDB.Driver;
using FirstBotDiscord.Extensions;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;

namespace FirstBotDiscord.Repository
{
    public class ItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context) =>
            _context = context;

        public async Task CreateItem(CommandContext ctx, string itemName, decimal price, bool canSell, bool canStack, bool canTrade, int itemType)
        {
            await Task.Run(() =>
            {
                var item = new BaseItemsEntity(itemName, price, canSell, canStack, canTrade, itemType);

                var sort = Builders<BaseItemsEntity>.Sort.Descending(x => x.ItemId);
                var filterItems = Builders<BaseItemsEntity>.Filter.Empty;

                var lastId = _context.CollectionItems.Find(filterItems).Sort(sort).FirstOrDefault();

                var embed = new DiscordEmbedBuilder();

                if (lastId == null)
                    item.ItemId = 1;
                else
                    item.ItemId = lastId.ItemId + 1;

                embed.WithDescription("Qual a descrição do item ?");
                ctx.RespondAsync(embed.Build());

                var interactivity = ctx.Client.GetInteractivity();
                var itemDescription = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                if (itemDescription.Result.TimedOut)
                {
                    embed.WithDescription("Tempo de resposta expirou, começa denovo ai, seu burro KKKKKKKK");
                    ctx.RespondAsync(embed.Build());
                }

                item.Description = itemDescription.Result.Result.Content;

                _context.CollectionItems.InsertOne(item);

                embed = new DiscordEmbedBuilder();
                embed.WithTitle("Seu novo item:");
                embed.WithDescription(item.Description);
                embed.AddField("Nome do item:", $"{item.Name}");
                embed.AddField("Preço de compra do item", $"{item.Price}");
                embed.AddField("É possivel vender o item ?", $"{item.CanSell}");
                embed.AddField("É possivel fazer pilhas desse item ?", $"{item.CanStack}");
                embed.AddField("É possivel trocar com algum player ?", $"{item.CanTrade}");
                embed.AddField("Qual o tipo do item ?", $"{item.ItemType.GetEnumDescription()}");

                ctx.RespondAsync(embed.Build());
            });
        }

        #region Create Item por partes, o bot manda um comando "qual o nome do item" e voce responde, e assim por diante
        //public async Task CreateItem(CommandContext ctx)
        //{
        //    var interactivity = ctx.Client.GetInteractivity();
        //    var result = new BaseItemsEntity();



        //    var embed = new DiscordEmbedBuilder();
        //    embed.WithDescription("Qual o nome do item ?");
        //    await ctx.RespondAsync(embed.Build());
        //    var itemName = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //    if(itemName.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo");
        //        await ctx.RespondAsync(embed.Build());
        //    }
        //    result.Name = itemName.Result.Content.ToString();


        //    embed = new DiscordEmbedBuilder();
        //    embed.WithDescription("Qual o preço do item ?");
        //    await ctx.RespondAsync(embed.Build());
        //    var itemPrice = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //    if (itemPrice.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo");
        //        await ctx.RespondAsync(embed.Build());
        //    }
        //    result.Price = decimal.Parse(itemPrice.Result.Content);


        //    embed = new DiscordEmbedBuilder();
        //    embed.WithDescription("É possivel vender o item");
        //    embed.WithFooter("Sim ou Não ?");
        //    await ctx.RespondAsync(embed.Build());
        //    var itemCanSell = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

        //    if (itemCanSell.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo");
        //        await ctx.RespondAsync(embed.Build());
        //    }

        //    switch (itemCanSell.Result.Content.ToString().Trim())
        //    {
        //        case "sim":
        //            result.CanSell = true;
        //            break;
        //        case "não":
        //            result.CanSell = false;
        //            break;
        //        default:
        //            await ctx.RespondAsync($"{ctx.User.Mention} seu burro, responda apenas sim ou não ");
        //            await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //            break;
        //    }


        //    embed = new DiscordEmbedBuilder();
        //    embed.WithDescription("É possivel fazer pilhas desse item?");
        //    await ctx.RespondAsync(embed.Build());
        //    var itemCanStack = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //    if (itemCanStack.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo");
        //        await ctx.RespondAsync(embed.Build());
        //    }

        //    switch (itemCanStack.Result.Content.ToString().Trim())
        //    {
        //        case "sim":
        //            result.CanStack = true;
        //            break;
        //        case "não":
        //            result.CanStack = false;
        //            break;
        //        default:
        //            await ctx.RespondAsync($"{ctx.User.Mention} seu burro, responda apenas sim ou não ");
        //            await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //            break;
        //    }

        //    embed = new DiscordEmbedBuilder();
        //    embed.WithDescription("É possivel fazer trocas com outro jogador com este item ?");
        //    await ctx.RespondAsync(embed.Build());
        //    var itemCanTrade = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //    if (itemCanTrade.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo");
        //        await ctx.RespondAsync(embed.Build());
        //    }

        //    switch (itemCanStack.Result.Content.ToString().Trim())
        //    {
        //        case "sim":
        //            result.CanTrade = true;
        //            break;
        //        case "não":
        //            result.CanTrade = false;
        //            break;
        //        default:
        //            await ctx.RespondAsync($"{ctx.User.Mention} seu burro, responda apenas sim ou não ");
        //            await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //            break;
        //    }

        //    var str = new StringBuilder();
        //    var ind = 0;
        //    foreach(var i in Enum.GetValues(typeof(ItemType)).Cast<ItemType>())
        //    {
        //        str.AppendLine($"{ind} -> {i.GetEnumDescription()}");
        //        ind++;
        //    }
        //    embed = new DiscordEmbedBuilder();
        //    embed.WithDescription($"Qual o tipo de item voce deseja criar\n" + str.ToString());
        //    await ctx.RespondAsync(embed.Build());
        //    var itemType = await ctx.WaitForEnumAsync<ItemType>(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
        //    if(itemType.TimedOut)
        //    {
        //        embed.WithDescription("Tempo de resposta expirou, comece denovo KKKK");
        //        await ctx.RespondAsync(embed.Build());
        //    }
        //    result.ItemType = itemType.Value;

        //    embed = new DiscordEmbedBuilder();
        //    embed.WithTitle("Seu novo item:");
        //    embed.AddField("Nome do item:", $"{itemName}");
        //    embed.AddField("Preço de compra do item", $"{result.Price}");
        //    embed.AddField("É possivel vender o item ?", $"{result.CanSell}");
        //    embed.AddField("É possivel fazer pilhas desse item ?", $"{result.CanStack}");
        //    embed.AddField("É possivel trocar com algum player ?", $"{result.CanTrade}");
        //    embed.AddField("Qual o tipo do item ?", $"{result.ItemType.GetEnumDescription()}");
        //    //embed.WithThumbnail(urlImage);
        //    //embed.WithDescription(description);
        //    await ctx.RespondAsync(embed.Build());



        //    await _context.CollectionItems.InsertOneAsync(result);
        #endregion
    }
}
