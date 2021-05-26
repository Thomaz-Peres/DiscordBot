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

        public async Task CreateMonster(CommandContext ctx, string monsterName, int level, int monsterLife, int monsterMana)
        {
            var monster = new BaseMonstersEntity();

            var embed = new DiscordEmbedBuilder();

            var interactivity = ctx.Client.GetInteractivity();

            monster.MonsterName = monsterName;
            monster.Level = level;
            monster.MonsterLife = monsterLife;
            monster.MonsterMana = monsterMana;
            //monster.Spawn = spawnTime;

            embed.WithTitle("Novo monstro:");
            embed.AddField("Nome:", monster.MonsterName, true);
            embed.AddField("Level:", monster.Level.ToString(), true);

            embed.AddField("Vida do monstro:", monster.MonsterLife.ToString(), true);
            embed.AddField("Mana do monstro:", monster.MonsterMana.ToString(), true);

            //embed.AddField("Tempo de spawn:", monster.Spawn.ToString());
            embed.WithFooter("Veja os comando abaixo para adicionar os atributos");
            await ctx.RespondAsync(embed.Build());
            //_context.CollectionMonsters.InsertOne(monster);

            embed = new DiscordEmbedBuilder();
            embed.WithDescription("Quantos pontos de atributo você deseja atribuir ? Min. 1");
            await ctx.RespondAsync(embed.Build());
            var quantityUp = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

            if (quantityUp.TimedOut) await ctx.RespondAsync("Cabou o tempo");
            
            embed = new DiscordEmbedBuilder();
            embed.WithTitle("Estes são os atributos do monstro");
            embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
            await ctx.RespondAsync(embed.Build());

            var upando = true;

            while(upando == true)
            {
                embed = new DiscordEmbedBuilder();
                embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                embed.WithDescription($"Vitalidade -- Sorte\n" +
                    $"Agilidade -- Carisma\n" +
                    $"Forca -- Inteligencia\n" +
                    $"Sabedoria");
                await ctx.RespondAsync(embed.Build());

                var waitAtributeToAsign = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                if (waitAtributeToAsign.TimedOut) await ctx.RespondAsync("Cabou o tempo de novo irmão, para de ser burro");

                switch(waitAtributeToAsign.Result.Content.ToLower())
                {
                    case "vitalidade":
                        monster.MonsterAtributes.Vitalidade += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        var YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                        
                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    case "sorte":
                        monster.MonsterAtributes.Sorte += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    case "agilidade":
                        monster.MonsterAtributes.Agilidade += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;
                        break;

                    case "carisma":
                        monster.MonsterAtributes.Carisma += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    case "força":
                        monster.MonsterAtributes.Forca += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    case "inteligencia":
                        monster.MonsterAtributes.Inteligencia += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    case "sabedoria":
                        monster.MonsterAtributes.Sabedoria += int.Parse(quantityUp.Result.Content);

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Seus atributos agora");
                        embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade} -- Sorte = {monster.MonsterAtributes.Sorte}\n" +
                        $"Agilidade = {monster.MonsterAtributes.Agilidade} -- Carisma = {monster.MonsterAtributes.Carisma}\n" +
                        $"Força = {monster.MonsterAtributes.Forca} -- Inteligencia = {monster.MonsterAtributes.Inteligencia}\n" +
                        $"Sabedoria = {monster.MonsterAtributes.Sabedoria}");
                        await ctx.RespondAsync(embed.Build());

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                        await ctx.RespondAsync(embed.Build());
                        YesOrNot = await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (YesOrNot.TimedOut) await ctx.RespondAsync("Cabou o tempo burro");

                        if (YesOrNot.Result.Content.ToLower() == "sim") upando = true;
                        else upando = false;

                        break;

                    default:
                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Escolha invalida, tente novamente");
                        await ctx.RespondAsync(embed.Build());

                        await interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        break;
                }
            }
        }
    }
}
