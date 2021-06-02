using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class CharactersRepository : BaseCommandModule
    {
        private readonly DataContext _context;

        public CharactersRepository(DataContext context) =>
            _context = context;

        public async Task AssignPointsCharacter(DiscordUser user, CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var player = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            var embed = new DiscordEmbedBuilder();

            if(player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints == 0)
            {
                embed.WithDescription("Você nao possui pontos de atributos livres");
            }

            if(player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints > 0)
            {
                embed = new DiscordEmbedBuilder();
                embed.WithDescription($"Você possui {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints} pontos de atributo:");
                embed.WithFooter("Deseja utiliza-los ? Responda sim ou nao");
                embed.WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed.Build());

                var YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id && player.PlayerId == ctx.User.Id);
                if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo, começa de novo ai");

                switch(YesOrNot.Result.Content.ToString().Trim())
                {
                    case "sim":
                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Quantos pontos de atributo você deseja atribuir ? Min. 1");
                        await ctx.RespondAsync(embed.Build());
                        var quantityUp = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id && player.PlayerId == ctx.User.Id);

                        if (quantityUp.TimedOut) await ctx.RespondAsync("Advinha ? O tempo acabou denovo");

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Estes são seus atributos atuais");
                        embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                            $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                            $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                            $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                        embed.WithDescription($"Vitalidade -- Sorte\n" +
                            $"Agilidade -- Carisma\n" +
                            $"Forca -- Inteligencia\n" +
                            $"Sabedoria");
                        await ctx.RespondAsync(embed.Build());

                        var waitAtributeToAsign = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id && player.PlayerId == ctx.User.Id);

                        if (waitAtributeToAsign.TimedOut) await ctx.RespondAsync("Cabou o tempo de novo irmao, para de ser burro");

                        switch(waitAtributeToAsign.Result.Content.ToLower())
                        {
                            case "vitalidade":
                                player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);
                                                                
                                var addLife = new StatusRepository(_context);
                                addLife.AddLifeStatus(player);
                                                                
                                var update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                var filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "sorte":
                                player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "agilidade":
                                player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "carisma":
                                player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "força":
                                player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "inteligencia":
                                player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            case "sabedoria":
                                player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints += double.Parse(quantityUp.Result.Content);
                                player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints -= double.Parse(quantityUp.Result.Content);



                                update = Builders<PlayerEntity>.Update.Set("MyCharacter", player.MyCharacter);
                                filter = Builders<PlayerEntity>.Filter.Eq(x => x.PlayerId, ctx.User.Id);

                                await _context.CollectionPlayers.UpdateOneAsync(filter, update);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade.CurrentValuePoints} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade.CurrentValuePoints} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma.CurrentValuePoints}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca.CurrentValuePoints} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria.CurrentValuePoints}");
                                embed.WithFooter($"Pontos livres = {player.MyCharacter.AtributesCharacter.PontosLivres.CurrentValuePoints}");
                                await ctx.RespondAsync(embed.Build());

                                break;

                            default:
                                embed = new DiscordEmbedBuilder();
                                embed.WithDescription("Valor invalido, use o comando novamente");
                                await ctx.RespondAsync(embed.Build());

                                break;                                
                        }                        
                        break;
                    
                    case "nao":
                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Seus pontos não vão ser utilizados nem descontados, sinta-se a vontade para fazer o que quiser\n Na proxima chama apenas se for usar eles carai");

                        await ctx.RespondAsync(embed.Build());
                    break;

                    default:
                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Apenas sim ou nao meu mano, larga de ser noia, ta escrito na pergunta\n Se escreveu sem querer esqueça a linha de cima\n envie o comando novamente");

                        await ctx.RespondAsync(embed.Build());
                    break;

                }
            }
        }
    }
}
