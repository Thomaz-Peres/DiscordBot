using System.ComponentModel;

namespace FirstBotDiscord.Enums
{
    public enum ItemType : int
    {
        Nothing,
        [Description("Livro")]
        Book,
        [Description("Comida")]
        Food,
        [Description("Chave")]
        Key,
        [Description("Poção")]
        Potion,
        [Description("Arma")]
        Weapon,
        [Description("Livro de Fabricação")]
        CraftingBook,
        [Description("Armadura")]
        Armour
    }
}
