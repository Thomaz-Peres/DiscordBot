namespace FirstBotDiscord.Entities.Rpg
{
    public class AtributesEntity
    {
        public AtributesEntity() { }

        // Aumenta o ataque fisico minimo e maximo do jogador
        public int Forca { get; set; } = 0;

        // Aumenta o ataque magico minimo e maximo do jogador
        public int Inteligencia { get; set; } = 0;

        // Aumenta a vida maxima do jogador, e regeneração de vida
        public int Vitalidade { get; set; } = 0;

        // Aumenta a resistencia contra venenos e stuns
        public int Resistencia { get; set; } = 0;
        
        // Aumenta a chance de desvio contra um ataque
        public int Agilidade { get; set; } = 0;

        // Aumenta a chance de enganar um NPC
        public int Carisma { get; set; } = 0;

        public int Sabedoria { get; set; } = 0;

        // Aumenta a chance de acertar o ataque (caso o numero do dado for muito bom, pode dar critico, ou aumentar a chance de dar um critico)
        public int Sorte { get; set; } = 0;

        // Pontos de atributo livre (não usados)
        public int PontosLivres { get; set; } = 0;
    }
}
