using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using FirstBotDiscord.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Commands
{
    public class AtributesCommands : BaseCommandModule
    {
        private readonly AtributesRepository _repository;

        public AtributesCommands(AtributesRepository repository) =>
            _repository = repository;

        [Command("Status")]
        [Aliases("s")]
        [Description("Visualiza os atributos do personagem")]
        public async Task CharacterStatus(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await _repository.CharacterAtributes(ctx, ctx.User);
        }
    }
}
