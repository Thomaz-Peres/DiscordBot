using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using FirstBotDiscord.Database;
using MongoDB.Driver;
using FirstBotDiscord.Entities.Rpg.Player;
using FirstBotDiscord.Repository;
using FirstBotDiscord.Entities.Rpg;
using System;
using MongoDB.Bson;

namespace FirstBotDiscord.Commands
{
    public class StartCommands : BaseCommandModule
    {
        private readonly DataContext _context;
        private readonly StartsRepository _repository;

        public StartCommands(DataContext context, StartsRepository repository)
        {
            _context = context;
            _repository = repository;
        }
            

        [Command("createPlayer")]
        [Aliases("cP")]
        [Description("Cria um Player")]
        public async Task CreatePlayer(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var embed = new DiscordEmbedBuilder();

            var player = await _repository.FindUser(ctx.User);

            
            
            if (player == null)
            {
                player = new PlayerEntity();
                player.PlayerId = ctx.User.Id;
                player.NamePlayer = ctx.User.Username;
                player.MyCharacter.CurrentLocalization = new LocalizationEntity(ctx.Channel.Id, ctx.Channel.Name);
                await _context.CollectionPlayers.InsertOneAsync(player);

                embed.AddField("Nome do seu cadastro:", $"{player.NamePlayer}");
                embed.AddField("Seu cadastro foi criado em:", $"{player.DateCreatePlayer}");

                embed.WithThumbnail(ctx.User.AvatarUrl);
                embed.WithColor(DiscordColor.Blue);
                embed.WithFooter($"Conta criada com sucesso. \nUse o comando 'x' para descobrir quantos pontos de habilidade você ganhara");


                await ctx.RespondAsync(embed.Build());
                return;
            }

            if (player.PlayerId == ctx.User.Id)
            {
                embed.WithDescription("Você ja possui uma conta, seu noia");
                await ctx.RespondAsync(embed.Build());
                return;
            }
        }

        [Command("playDice")]
        [Aliases("pd")]
        [Description("Cria um personagem dentro do player")]
        public async Task PlayDice(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var embed = new DiscordEmbedBuilder();
            Random random = new Random();

            int atributtesNum = random.Next(1, 6);

            var player = await _repository.FindUser(ctx.User);
            

            if (player.PlayerId == ctx.User.Id && player.PlayDice == false)
            {
                var update = Builders<PlayerEntity>.Update.Set("MyCharacter.AtributesCharacter.PontosLivres", atributtesNum);
                var update2 = Builders<PlayerEntity>.Update.Set("PlayDice", true);
                
                
                embed.AddField($"{ctx.User.Username} ganhou {atributtesNum} pontos de atributos iniciais:", $"{atributtesNum}");
                await ctx.RespondAsync(embed.Build());
                
                
                var filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);
                var filter2 = Builders<PlayerEntity>.Filter.Eq(x => x.PlayDice, true);

                await _context.CollectionPlayers.UpdateOneAsync(filter, update);
                await _context.CollectionPlayers.UpdateOneAsync(filter, update2);
                return;
            }
            else
            {
                await ctx.RespondAsync($"{ctx.User.Mention} Você ja ganhou seus pontos de atributo, seu arrombado");
            }
        }
    }
}