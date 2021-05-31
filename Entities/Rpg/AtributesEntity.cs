using FirstBotDiscord.Entities.Rpg.UsefulnessOfPoints;

namespace FirstBotDiscord.Entities.Rpg
{
    public class AtributesEntity
    {

        // Aumenta o ataque fisico minimo e maximo do jogador
        public StatusOfAttributePoints Forca { get; set; }

        // Aumenta o ataque magico minimo e maximo do jogador
        public StatusOfAttributePoints Inteligencia { get; set; }

        // Aumenta a vida maxima do jogador, e regeneração de vida
        public StatusOfAttributePoints Vitalidade { get; set; }
        
        // Aumenta a chance de desvio contra um ataque
        public StatusOfAttributePoints Agilidade { get; set; }

        // Aumenta a persuasão do personagem
        public StatusOfAttributePoints Carisma { get; set; }

        // Aumento de regeneração de mana
        public StatusOfAttributePoints Sabedoria { get; set; }

        // Aumenta a chance de acertar o ataque (caso o numero do dado for muito bom, pode dar critico, ou aumentar a chance de dar um critico)
        public StatusOfAttributePoints Sorte { get; set; }

        // Pontos de atributo livre (não usados)
        public StatusOfAttributePoints PontosLivres { get; set; }
    }
}
