using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.Player;
using MongoDB.Driver;
using System.Runtime.Serialization;
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
            var player = await _context.CollectionPlayers.Find(x => x.PlayerId == user.Id).FirstOrDefaultAsync();

            var interactivity = ctx.Client.GetInteractivity();

            var embed = new DiscordEmbedBuilder();

            if(player.MyCharacter.AtributesCharacter.PontosLivres > 0)
            {
                embed.WithDescription($"Você possui {player.MyCharacter.AtributesCharacter.PontosLivres} pontos de atributo:");
                embed.WithFooter("Deseja utiliza-los ?");
                embed.WithColor(DiscordColor.Blue);
                await ctx.RespondAsync(embed.Build());

                var YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id && player.PlayerId == ctx.User.Id);
                if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo, começa denovo ai");

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
                        embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte}\n" +
                            $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma}\n" +
                            $"Força = {player.MyCharacter.AtributesCharacter.Forca} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia}\n" +
                            $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                        embed.WithDescription($"Vitalidade -- Sorte\n" +
                            $"Agilidade -- Carisma\n" +
                            $"Forca -- Inteligencia\n" +
                            $"Sabedoria");
                        await ctx.RespondAsync(embed.Build());

                        var waitAtributeToAsign = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id && player.PlayerId == ctx.User.Id);

                        if (waitAtributeToAsign.TimedOut) await ctx.RespondAsync("Cabou o tempo denovo irmao, para de ser burro");

                        switch(waitAtributeToAsign.Result.Content)
                        {
                            case "Vitalidade":
                                player.MyCharacter.AtributesCharacter.Vitalidade += int.Parse(quantityUp.Result.Content);
                                embed.WithDescription($"Vitalidade = {player.MyCharacter.AtributesCharacter.Vitalidade} -- Sorte = {player.MyCharacter.AtributesCharacter.Sorte}\n" +
                                $"Agilidade = {player.MyCharacter.AtributesCharacter.Agilidade} -- Carisma = {player.MyCharacter.AtributesCharacter.Carisma}\n" +
                                $"Força = {player.MyCharacter.AtributesCharacter.Forca} -- Inteligencia = {player.MyCharacter.AtributesCharacter.Inteligencia}\n" +
                                $"Sabedoria = {player.MyCharacter.AtributesCharacter.Sabedoria}");
                                await ctx.RespondAsync(embed.Build());
                                break;
                        }

                        break;
                }
            }
        }
    }
}
