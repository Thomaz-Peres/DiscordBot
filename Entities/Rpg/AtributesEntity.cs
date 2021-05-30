namespace FirstBotDiscord.Entities.Rpg
{
    public class AtributesEntity
    {

        // Aumenta o ataque fisico minimo e maximo do jogador
        public StatePointsEntity Forca { get; set; }

        // Aumenta o ataque magico minimo e maximo do jogador
        public StatePointsEntity Inteligencia { get; set; }

        // Aumenta a vida maxima do jogador, e regeneração de vida
        public StatePointsEntity Vitalidade { get; set; }
        
        // Aumenta a chance de desvio contra um ataque
        public StatePointsEntity Agilidade { get; set; }

        // Aumenta a persuasão do personagem
        public StatePointsEntity Carisma { get; set; }

        // Aumento de regeneração de mana
        public StatePointsEntity Sabedoria { get; set; }

        // Aumenta a chance de acertar o ataque (caso o numero do dado for muito bom, pode dar critico, ou aumentar a chance de dar um critico)
        public StatePointsEntity Sorte { get; set; }

        // Pontos de atributo livre (não usados)
        public StatePointsEntity PontosLivres { get; set; }
    }
}
