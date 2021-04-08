using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Enums
{
    public enum MapsType : int
    {
        [Description("Cidade")]
        City,
        [Description("Floresta")]
        Florest,
        [Description("Vila")]
        Village
    }
}
