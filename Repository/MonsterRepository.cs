using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using FirstBotDiscord.Database;
using FirstBotDiscord.Entities.Rpg;
using FirstBotDiscord.Entities.Rpg.RpgMonsters;
using FirstBotDiscord.Repository.Status;
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

        public async Task CreateMonster(CommandContext ctx, string monsterName, int level, bool isBoss)
        {
            await Task.Run(() =>
            {
                var monster = new BaseMonstersEntity();

                var embed = new DiscordEmbedBuilder();
                embed.WithDescription("O monstro vai ser criado no mapa (canal) atual, deseja continuar ? Sim ou Não ?");
                ctx.RespondAsync(embed.Build());

                var continueCreateMonster = ctx.Client.GetInteractivity().WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                if (continueCreateMonster.Result.TimedOut)
                {
                    ctx.RespondAsync("Cabou o tempo, começa de novo");
                    return;
                }

                if (continueCreateMonster.Result.Result.Content.ToString().ToLower() == "não" || continueCreateMonster.Result.Result.Content.ToString().ToLower() == "nao" || continueCreateMonster.Result.Result.Content.ToString().ToLower() == "n")
                {
                    embed = new DiscordEmbedBuilder();
                    embed.WithDescription("Criação de monstro cancelada");
                    ctx.RespondAsync(embed.Build());
                    return;
                }                    
                else if (continueCreateMonster.Result.Result.Content.ToString().ToLower() == "sim" || continueCreateMonster.Result.Result.Content.ToString().ToLower() == "s")
                {
                    var sort = Builders<BaseMonstersEntity>.Sort.Descending("MonsterId");
                    var filterMonsters = Builders<BaseMonstersEntity>.Filter.Empty;
                    var lastId = _context.CollectionMonsters.Find(filterMonsters).Sort(sort).FirstOrDefault();

                    if (lastId == null)
                        monster.MonsterId = 1;
                    else
                        monster.MonsterId = lastId.MonsterId + 1;

                    embed = new();
                    monster.MonsterName = monsterName;
                    monster.Level = level;
                    monster.IsBoss = isBoss;
                    //monster.Spawn = spawnTime;
                    monster.MonsterLifePoints.MaxValuePoints *= monster.Level;
                    monster.MonsterLifePoints.CurrentOrMinValuePoints = monster.MonsterLifePoints.MaxValuePoints;

                    monster.MonsterManaPoints.MaxValuePoints *= monster.Level;
                    monster.MonsterManaPoints.CurrentOrMinValuePoints = monster.MonsterManaPoints.MaxValuePoints;

                    monster.MonsterAtributes.PontosLivres.CurrentValuePoints += 5 * monster.Level;
                    monster.MonsterLocalization = new LocalizationEntity(ctx.Channel.Id, ctx.Channel.Name, ctx.Guild.Id);

                    embed.WithTitle("Novo monstro:");
                    embed.AddField("Nome:", monster.MonsterName, true);
                    embed.AddField("Level:", monster.Level.ToString(), true);
                    embed.AddField("É um Boss:", monster.IsBoss.ToString(), true);

                    embed.AddField("Vida do monstro:", monster.MonsterLifePoints.CurrentOrMinValuePoints.ToString(), true);
                    embed.AddField("Mana do monstro:", monster.MonsterManaPoints.CurrentOrMinValuePoints.ToString(), true);
                    embed.AddField("Localização do monstro:", monster.MonsterLocalization.ChannelName);

                    //embed.AddField("Tempo de spawn:", monster.Spawn.ToString());
                    embed.WithFooter("Todos os monstros novos começam com status zerados, mas com alguns pontos de atributos de acordo com seu level\nVeja os comando abaixo para adicionar os atributos");
                    ctx.RespondAsync(embed.Build());

                    var upando = true;
                    #region Whiles
                    while (upando == true)
                    {
                        var statusRepository = new MonsterStatusRepository();
                        var interactivity = ctx.Client.GetInteractivity();

                        embed = new DiscordEmbedBuilder();
                        embed.WithDescription("Quantos pontos de atributo você deseja atribuir ? Min. 1");
                        embed.WithFooter($"Pontos de atributos livres: {monster.MonsterAtributes.PontosLivres.CurrentValuePoints}");
                        ctx.RespondAsync(embed.Build());
                        var quantityUp = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (quantityUp.Result.TimedOut)
                        {
                            ctx.RespondAsync("Cabou o tempo, começa de novo");
                            return;
                        }

                        embed = new DiscordEmbedBuilder();
                        embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                        embed.WithDescription(
                            $"Vitalidade -- Sorte\n" +
                            $"Agilidade -- Carisma\n" +
                            $"Forca -- Inteligencia\n" +
                            $"Sabedoria");

                        ctx.RespondAsync(embed.Build());

                        var waitAtributeToAsign = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                        if (waitAtributeToAsign.Result.TimedOut)
                        {
                            ctx.RespondAsync("Cabou o tempo de novo irmão, para de ser burro");
                            return;
                        }

                        switch (waitAtributeToAsign.Result.Result.Content.ToLower())
                        {
                            case "vitalidade":
                                monster.MonsterAtributes.Vitalidade.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");
                                
                                embed.WithDescription("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddLifeStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                var YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "sorte":
                                monster.MonsterAtributes.Sorte.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddLuckStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "agilidade":
                                monster.MonsterAtributes.Agilidade.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddEvasionStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }

                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "carisma":
                                monster.MonsterAtributes.Carisma.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddPersuationStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "força":
                            case "forca":
                                monster.MonsterAtributes.Forca.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddPhysicalAttackStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "inteligencia":
                                monster.MonsterAtributes.Inteligencia.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddMagicAttackStatus(monster);


                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            case "sabedoria":
                                monster.MonsterAtributes.Sabedoria.CurrentValuePoints += int.Parse(quantityUp.Result.Result.Content);
                                monster.MonsterAtributes.PontosLivres.CurrentValuePoints -= int.Parse(quantityUp.Result.Result.Content);

                                embed = new DiscordEmbedBuilder();
                                embed.WithTitle("Seus atributos agora");
                                embed.WithDescription($"Vitalidade = {monster.MonsterAtributes.Vitalidade.CurrentValuePoints} -- Sorte = {monster.MonsterAtributes.Sorte.CurrentValuePoints}\n" +
                                $"Agilidade = {monster.MonsterAtributes.Agilidade.CurrentValuePoints} -- Carisma = {monster.MonsterAtributes.Carisma.CurrentValuePoints}\n" +
                                $"Força = {monster.MonsterAtributes.Forca.CurrentValuePoints} -- Inteligencia = {monster.MonsterAtributes.Inteligencia.CurrentValuePoints}\n" +
                                $"Sabedoria = {monster.MonsterAtributes.Sabedoria.CurrentValuePoints}");

                                embed.WithFooter("Deseja continuar adicionando os atributos do monstro ? Responda sim ou não");
                                ctx.RespondAsync(embed.Build());

                                statusRepository.AddManaStatus(monster);

                                if (monster.MonsterAtributes.PontosLivres.CurrentValuePoints <= 0)
                                {
                                    _context.CollectionMonsters.InsertOne(monster);
                                    ctx.RespondAsync("Pontos de atributos acabaram, Monstro completo 👾");
                                    return;
                                }


                                YesOrNot = interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);
                                if (YesOrNot.Result.TimedOut)
                                {
                                    ctx.RespondAsync("Cabou o tempo burro");
                                    return;
                                }

                                upando = YesOrNot.Result.Result.Content.ToLower() == "sim" ? true : false;

                                break;

                            default:
                                embed = new DiscordEmbedBuilder();
                                embed.WithDescription("Escolha invalida, tente novamente");
                                ctx.RespondAsync(embed.Build());

                                upando = true;

                                //embed = new DiscordEmbedBuilder();
                                //embed.WithTitle("Qual dos atributos abaixo você deseja upar ?");
                                //embed.WithDescription(
                                //    $"Vitalidade -- Sorte\n" +
                                //    $"Agilidade -- Carisma\n" +
                                //    $"Forca -- Inteligencia\n" +
                                //    $"Sabedoria");
                                // ctx.RespondAsync(embed.Build());

                                //waitAtributeToAsign =  interactivity.WaitForMessageAsync(x => x.Author.Id == ctx.User.Id && x.ChannelId == ctx.Channel.Id);

                                //if (waitAtributeToAsign.TimedOut)  ctx.RespondAsync("Cabou o tempo de novo irmão, para de ser burro");

                                break;
                        }
                    }
                    #endregion
                    if ((upando == false) && monster.MonsterAtributes.PontosLivres.CurrentValuePoints > 0)
                    {
                        _context.CollectionMonsters.InsertOne(monster);
                        ctx.RespondAsync("Monstro completo 👾");
                    }
                }
                else
                {
                    ctx.RespondAsync("Não existe essa resposta, começa de novo");
                    return;
                }
            });
        }
    }
}
