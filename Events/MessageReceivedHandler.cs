using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Events
{
    public static class MessageReceivedHandler
    {
        public static Task Event(CommandsNextExtension cne, CommandExecutionEventArgs e)
        {
            cne.Client.Logger.LogInformation(new EventId(600, "Comando exec"), $"{e.Context.Guild.Name} - {e.Context.User.Id} executou '{e.Command.QualifiedName}'.", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
