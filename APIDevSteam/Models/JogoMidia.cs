namespace APIDevSteam.Models
{
    public class JogoMidia
    {
        public Guid JogoMidiaId { get; set; }
        public Guid JogoId { get; set; }
        public Jogo? Jogo { get; set; }
        public string Tipo { get; set; } // Ex: "Trailer", "Gameplay", "Cenas do Jogo"
        public string Url { get; set; } // URL do vídeo ou imagem
    }
}
