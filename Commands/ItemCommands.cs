using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Items;
using FirstBotDiscord.Enums;
using FirstBotDiscord.Extensions;
using FirstBotDiscord.Repository;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class ItemCommands : BaseCommandModule
    {
        private readonly ItemRepository _repository;
        private readonly DataContext _context;

        public ItemCommands(ItemRepository repository, DataContext context)
        {
            _repository = repository;
            _context = context;
        }


        [Command("createNewItem")]
        [Aliases("ci")]
        [Description("Permite criar um novo item")] 
        public async Task CreateNewItem(CommandContext ctx, string itemName, decimal price, bool canSell, bool canStack, bool canTrade, int itemType)
        {
            #region teste
            //try
            //{
            //    await ctx.TriggerTypingAsync();
            //    var interactivity = ctx.Client.GetInteractivity();

            //    var embed = new DiscordEmbedBuilder();
            //    embed.WithDescription("Qual a descrição do item ?");
            //    await ctx.RespondAsync(embed.Build());

            //    var itemDescription = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id).ConfigureAwait(true);
            //    if (itemDescription.TimedOut)
            //    {
            //        embed.WithDescription("Tempo de resposta expirou, começa denovo ai, seu burro KKKKKKKK");
            //        await ctx.RespondAsync(embed.Build());
            //    }

            //    var result = new BaseItemsEntity(itemName, price, canSell, canStack, canTrade, itemType)
            //    { Description = itemDescription.Result.Content, ItemId = 1 };

            //    await _context.CollectionItems.InsertOneAsync(result);

            //    embed = new DiscordEmbedBuilder();
            //    embed.WithTitle("Seu novo item:");
            //    embed.AddField("Nome do item:", $"{result.Name}");
            //    embed.AddField("Preço de compra do item", $"{result.Price}");
            //    embed.AddField("É possivel vender o item ?", $"{result.CanSell}");
            //    embed.AddField("É possivel fazer pilhas desse item ?", $"{result.CanStack}");
            //    embed.AddField("É possivel trocar com algum player ?", $"{result.CanTrade}");
            //    embed.AddField("Qual o tipo do item ?", $"{result.ItemType.GetEnumDescription()}");
            //    embed.WithDescription(result.Description);

            //    await ctx.RespondAsync(embed.Build());
            //}
            //catch (Exception)
            //{
            //    throw new Exception();
            //}
            #endregion

            await ctx.TriggerTypingAsync();

            await _repository.CreateItem(ctx, itemName, price, canSell, canStack, canTrade, itemType);
        }
    }
}
