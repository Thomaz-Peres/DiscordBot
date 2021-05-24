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
    public class StatusCommands : BaseCommandModule
    {
        private readonly StatusRepository _repository;

        public StatusCommands(StatusRepository repository) =>
            _repository = repository;

        [Command("Status")]
        [Description("Visualiza o status do personagem")]
        public async Task CharacterStatus(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await _repository.CharacterStatus(ctx, ctx.User);
        }
    }
}
