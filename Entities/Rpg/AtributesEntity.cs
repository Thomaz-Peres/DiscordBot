using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;

namespace FirstBotDiscord.Entities.Rpg
{
    public class AtributesEntity
    {

        // Aumenta o ataque fisico minimo e maximo do jogador
        public StatusOfAttributePoints Forca { get; set; } = new StatusOfAttributePoints();

        // Aumenta o ataque magico minimo e maximo do jogador
        public StatusOfAttributePoints Inteligencia { get; set; } = new StatusOfAttributePoints();

        // Aumenta a vida maxima do jogador, e regeneração de vida
        public StatusOfAttributePoints Vitalidade { get; set; } = new StatusOfAttributePoints();
        
        // Aumenta a chance de desvio contra um ataque
        public StatusOfAttributePoints Agilidade { get; set; } = new StatusOfAttributePoints();

        // Aumenta a persuasão do personagem
        public StatusOfAttributePoints Carisma { get; set; } = new StatusOfAttributePoints();

        // Aumento de regeneração de mana
        public StatusOfAttributePoints Sabedoria { get; set; } = new StatusOfAttributePoints();

        // Aumenta a chance de acertar o ataque (caso o numero do dado for muito bom, pode dar critico, ou aumentar a chance de dar um critico)
        public StatusOfAttributePoints Sorte { get; set; } = new StatusOfAttributePoints();

        // Pontos de atributo livre (não usados)
        public StatusOfAttributePoints PontosLivres { get; set; } = new StatusOfAttributePoints();
    }
}
