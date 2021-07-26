using FirstBotDiscord.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FirstBotDiscord.Entities.Rpg.Items
{
    public class BaseItemsEntity
    {
        [BsonId]
        public Guid Id { get; set; }
        public int? ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = 1;
        public bool CanSell { get; set; } = true;
        public bool CanStack { get; set; } = true;
        public bool CanTrade { get; set; } = true;
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; } = 1;
        public string UrlImage { get; set; } = "";
        public string Description { get; set; } = "Sem descrição";

        public BaseItemsEntity(string itemName, decimal price, bool canSell, bool canStack, bool canTrade, int itemType)
        {
            this.Name = itemName;
            this.Price = price;
            this.CanSell = canSell;
            this.CanStack = canStack;
            this.CanTrade = canTrade;
            this.ItemType = (ItemType)itemType;
        }
    }
}
