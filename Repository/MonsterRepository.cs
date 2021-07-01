using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FirstBotDiscord.Repository
{
    public class MonsterRepository
    {
        private readonly DataContext _context;

        public MonsterRepository(DataContext context) =>
            _context = context;

        public async Task CreateMonster(CommandContext ctx, string monsterName, int level)
        {
            var monster = new BaseMonstersEntity();

            var embed = new DiscordEmbedBuilder();

            var interactivity = ctx.Client.GetInteractivity();

            monster.MonsterName = monsterName;
            monster.Level = level;
            //monster.Spawn = spawnTime;

            embed.WithTitle("Novo monstro:");
            embed.AddField("Nome:", monster.MonsterName, true);
            embed.AddField("Level:", monster.Level.ToString(), true);

            embed.AddField("Vida do monstro:", monster.MonsterLifePoints.CurrentOrMinValuePoints.ToString(), true);
            embed.AddField("Mana do monstro:", monster.MonsterManaPoints.CurrentOrMinValuePoints.ToString(), true);

            //embed.AddField("Tempo de spawn:", monster.Spawn.ToString());
            embed.WithFooter("Todos os monstros novos começam com status zerados\n Veja os comando abaixo para adicionar os atributos");
            await ctx.RespondAsync(embed.Build());

            var upando = true;

            while(upando == true)
            {
                var statusRepository = new MonsterStatusRepository();

                embed = new DiscordEmbedBuilder();
                embed.WithDescription("Quantos pontos de atributo você deseja atribuir ? Min. 1");
                await ctx.RespondAsync(embed.Build());
                var quantityUp = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                if (quantityUp.TimedOut) 
                    await ctx.RespondAsync("Cabou o tempo");

                embed = new DiscordEmbedBuilder();
                embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                embed.WithDescription(
                    $"Vitalidade -- Sorte\n" +
                    $"Agilidade -- Carisma\n" +
                    $"Forca -- Inteligencia\n" +
                    $"Sabedoria");

                await ctx.RespondAsync(embed.Build());

                var waitAtributeToAsign = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                if (waitAtributeToAsign.TimedOut) await ctx.RespondAsync("Cabou o tempo de novo irmão, para de ser burro");

                switch(waitAtributeToAsign.Result.Content.ToLower())
                {
                    case "vitalidade":
                        monster.MonsterAtributes.Vitalidade.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddLifeStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        var YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                        
                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "sorte":
                        monster.MonsterAtributes.Sorte.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddLuckStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "agilidade":
                        monster.MonsterAtributes.Agilidade.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddEvasionStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "carisma":
                        monster.MonsterAtributes.Carisma.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddPersuationStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "força":
                        monster.MonsterAtributes.Forca.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddPhysicalAttackStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "inteligencia":
                        monster.MonsterAtributes.Inteligencia.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddMagicAttackStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    case "sabedoria":
                        monster.MonsterAtributes.Sabedoria.CurrentValuePoints += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                        $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                        await ctx.RespondAsync(embed.Build());

                        statusRepository.AddManaStatus(monster);

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        upando = YesOrNot.Result.Content.ToLower() == "sim" ? true : false;

                        break;

                    default:
                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Escolha invalida, tente novamente");
                        await ctx.RespondAsync(embed.Build());

                        upando = true;

                        //embed = new DiscordEmbedBuilder();
                        //embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                        //embed.WithDescription(
                        //    $"Vitalidade -- Sorte\n" +
                        //    $"Agilidade -- Carisma\n" +
                        //    $"Forca -- Inteligencia\n" +
                        //    $"Sabedoria");
                        //await ctx.RespondAsync(embed.Build());

                        //waitAtributeToAsign = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        //if (waitAtributeToAsign.TimedOut) await ctx.RespondAsync("Cabou o tempo de novo irmão, para de ser burro");

                        break;
                }
            }
            
            if (upando == false) 
                await ctx.RespondAsync("Monstro completo 👾");
            
            await _context.CollectionMonsters.InsertOneAsync(monster);
        }
    }
}
