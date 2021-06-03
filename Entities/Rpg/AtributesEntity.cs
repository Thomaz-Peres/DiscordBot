using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;

namespace FirstBotDiscord.Entities.Rpg
{
    public class AtributesEntity
    {

        // Aumenta o ataque fisico maximo do jogador
        public StatusOfAttributePoints Forca { get; set; } = new StatusOfAttributePoints();

        // Aumenta o ataque magico maximo do jogador, e um pouco do ataque magico e maximo
        public StatusOfAttributePoints Inteligencia { get; set; } = new StatusOfAttributePoints();

        // Aumenta a vida maxima do jogador, e regeneração de vida
        public StatusOfAttributePoints Vitalidade { get; set; } = new StatusOfAttributePoints();
        
        // Aumenta a chance de desvio contra um ataque
        public StatusOfAttributePoints Agilidade { get; set; } = new StatusOfAttributePoints();

        // Aumenta a persuasão do personagem
        public StatusOfAttributePoints Carisma { get; set; } = new StatusOfAttributePoints();

        // Aumento de regeneração de mana e Mana maxima do jogador, e um pouco do ataque magico maximo
        public StatusOfAttributePoints Sabedoria { get; set; } = new StatusOfAttributePoints();

        // Aumenta os atributos minimos na batalha, melhorando sua chance, entre os valores minimos e maximos
        public StatusOfAttributePoints Sorte { get; set; } = new StatusOfAttributePoints();

        // Pontos de atributo livre (não usados)
        public StatusOfAttributePoints PontosLivres { get; set; } = new StatusOfAttributePoints();
    }
}
