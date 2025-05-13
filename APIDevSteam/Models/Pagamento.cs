namespace APIDevSteam.Models
{
    public class Pagamento
    {
        public Guid PagamentoId { get; set; }
        public Guid CarrinhoId { get; set; }
        public Carrinho? Carrinho { get; set; }
        public Guid CartaoId { get; set; }
        public Cartao? Cartao { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public string Status { get; set; } // Ex: Aprovado, Recusado, Pendente
        public string CodigoAutorizacao { get; set; }
    }
}
