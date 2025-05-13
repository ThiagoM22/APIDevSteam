using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class CupomCarrinho
    {
        // Define o campo CupomCarrinhoId como chave primária
        [Key]
        public Guid CupomCarrinhoId { get; set; }

        // O CarrinhoId é obrigatório para associar o cupom a um carrinho específico
        [Required(ErrorMessage = "O ID do carrinho é obrigatório.")]
        public Guid? CarrinhoId { get; set; }

        // Relacionamento com o carrinho
        public Carrinho? Carrinho { get; set; }

        // A data de validade do cupom no carrinho é obrigatória
        [Required(ErrorMessage = "A data de validade do cupom é obrigatória.")]
        public DateTime Validade { get; set; }

        // O CupomId é obrigatório para associar ao cupom aplicado
        [Required(ErrorMessage = "O ID do cupom é obrigatório.")]
        public Guid CupomId { get; set; }

        // Relacionamento com o cupom
        public Cupom? Cupom { get; set; }
    }
}
