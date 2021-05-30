using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class StartsRepository
    {
        private readonly DataContext _context;
        public StartsRepository(DataContext context)=>
            _context = context;

        public async Task CreateUser(DiscordUser user, CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder();
            var player = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            if (player == null)
            {
                player = new PlayerEntity
                {
                    PlayerId = ctx.User.Id,
                    NamePlayer = ctx.User.Username
                };

                player.MyCharacter.CurrentLocalization = new LocalizationEntity(ctx.Channel.Id, ctx.Channel.Name);
                await _context.CollectionPlayers.InsertOneAsync(player);

                embed.AddField("Nome do seu cadastro:", $"{player.NamePlayer}");
                embed.AddField("Seu cadastro foi criado em:", $"{player.DateCreatePlayer}");

                embed.WithThumbnail(ctx.User.AvatarUrl);
                embed.WithColor(DiscordColor.Blue);
                embed.WithFooter($"Conta criada com sucesso. \nUse o comando 'pd(play Dice)' para descobrir quantos pontos de habilidade você ganhara");


                await ctx.RespondAsync(embed.Build());
                //return player;
            }

            if (player.PlayerId == ctx.User.Id)
            {
                embed.WithDescription("Você ja possui uma conta, seu noia");
                await ctx.RespondAsync(embed.Build());
                //return null;
            }
        }

        public async Task PlayNewUserDice(DiscordUser user, CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder();
            var player = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            Random random = new Random();

            int atributtesNum = random.Next(1, 20);


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
