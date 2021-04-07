using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg
{
    public class Ticks
    {
        public DateTime Time { get; set; }
        public double Bid { get; set; }
        public double Ask { get; set; }
        public Customers Customer { get; set; }
    }
}
