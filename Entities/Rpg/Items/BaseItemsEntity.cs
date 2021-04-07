using FirstBotDiscord.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstBotDiscord.Entities.Rpg.Items
{
    public class BaseItemsEntity
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = 1m;
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; } = "Sem descrição";
    }
}
