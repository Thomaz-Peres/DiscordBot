using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using FirstBotDiscord.Repository;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class AssignAtributtesCharacter : BaseCommandModule
    {
        private readonly CharactersRepository _repository;

        public AssignAtributtesCharacter(CharactersRepository repository) =>
            _repository = repository;

        [Command("AtribuirPontos")]
        [Aliases("ap")]
        [Description("Permite atribuir pontos de atributo")]
        public async Task AssignPointsCharacter(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await _repository.AssignPointsCharacter(ctx.User, ctx);
        }
    }
}
