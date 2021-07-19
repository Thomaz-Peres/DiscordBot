using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using FirstBotDiscord.Repository;

namespace FirstBotDiscord.Commands
{
    public class StartCommands : BaseCommandModule
    {
        private readonly StartsRepository _repository;

        public StartCommands(StartsRepository repository)   =>
            _repository = repository;

        [Command("createPlayer")]
        [Aliases("cp")]
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