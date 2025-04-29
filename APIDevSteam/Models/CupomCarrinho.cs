namespace APIDevSteam.Models
{
    public class CupomCarrinho
    {
        public Guid CupomCarrinhoId { get; set; }
        public Guid? CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public DateTime Validade { get; set; }
        public Guid CupomId { get; set; }
        public Cupom? Cupom { get; set; }

    }
}
