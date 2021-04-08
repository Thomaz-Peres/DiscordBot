using FirstBotDiscord.Enums;

namespace FirstBotDiscord.Entities.Rpg.Items
{
    public class BaseItemsEntity
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } = 1;
        public ItemType ItemType { get; set; }
        public int Quantity { get; set; }
        public string UrlImage { get; set; }
        public string Description { get; set; } = "Sem descrição";
    }
}
