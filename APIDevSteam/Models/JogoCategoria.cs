namespace APIDevSteam.Models
{
    public class JogoCategoria
    {
        public Guid JogoCategoriaId { get; set; }
        public Guid JogoId { get; set; }
        public Jogo? Jogo { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
