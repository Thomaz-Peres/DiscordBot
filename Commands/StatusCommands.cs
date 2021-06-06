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
        private readonly AtributesRepository _repository;

        public StatusCommands(AtributesRepository repository) =>
            _repository = repository;

        [Command("status")]
        [Aliases("s")]
        [Description("Visualiza algumas informações do personagem")]
        public async Task PlayerStatus(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await _repository.PlayerStatus(ctx, ctx.User);
        }

        [Command("statusAtributo")]
        [Aliases("sa")]
        [Description("Visualiza os status e atributos do personagem")]
        public async Task CharacterStatus(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await _repository.CharacterStatus(ctx, ctx.User);
        }
    }
}
