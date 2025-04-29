namespace APIDevSteam.Models
{
    public class ItemCarrinho
    {
        public Guid ItemCarrinhoId { get; set; }
        public Guid? CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public Guid? JogoId { get; set; }
        public Jogo? Jogo { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
