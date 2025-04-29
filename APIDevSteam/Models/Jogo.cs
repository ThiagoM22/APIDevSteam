namespace APIDevSteam.Models
{
    public class Jogo
    {
        public Guid? JogoId { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public int Desconto { get; set; }
        public string UrlImagem { get; set; }
        public string Banner { get; set; }
        public string Descricao { get; set; }

    }
}
