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

            await _repository.CreateUser(ctx.User, ctx);
        }

        [Command("playDice")]
        [Aliases("pd")]
        [Description("Roda o dado para o player ganhar pontos de atributos iniciais")]
        public async Task PlayDice(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            
            await _repository.PlayNewUserDice(ctx.User, ctx);
        }
    }
}