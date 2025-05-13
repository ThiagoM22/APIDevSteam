using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class ItemCarrinho
    {
        // Define o campo ItemCarrinhoId como chave primária
        [Key]
        public Guid ItemCarrinhoId { get; set; }

        // O CarrinhoId é obrigatório para associar o item ao carrinho
        [Required(ErrorMessage = "O ID do carrinho é obrigatório.")]
        public Guid? CarrinhoId { get; set; }

        // Relacionamento com o carrinho
        public Carrinho? Carrinho { get; set; }

        // O JogoId é obrigatório para associar o item ao jogo
        [Required(ErrorMessage = "O ID do jogo é obrigatório.")]
        public Guid? JogoId { get; set; }

        // Relacionamento com o jogo
        public Jogo? Jogo { get; set; }

        // A quantidade deve ser maior que zero
        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }

        // O valor unitário deve ser maior que zero
        [Required(ErrorMessage = "O valor unitário é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior que zero.")]
        public decimal ValorUnitario { get; set; }

        // O valor total deve ser calculado com base na quantidade e valor unitário
        [Required(ErrorMessage = "O valor total é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }
    }
}
