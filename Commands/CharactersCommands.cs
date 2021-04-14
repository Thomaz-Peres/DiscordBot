using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using FirstBotDiscord.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class CharactersCommands : BaseCommandModule
    {
        private readonly CharactersRepository _repository;

        public CharactersCommands(CharactersRepository repository) =>
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
