using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace APIDevSteam.Models
{
    public class Carrinho
    {
        // Define o campo CarrinhoId como chave primária
        [Key]
        public Guid CarrinhoId { get; set; }

        // O UsuarioId é obrigatório para associar o carrinho a um usuário
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public Guid? UsuarioId { get; set; }

        // Relacionamento com o usuário
        public Usuario? Usuario { get; set; }

        // A data de criação do carrinho deve ser fornecida (obrigatória)
        [Required(ErrorMessage = "A data de criação é obrigatória.")]
        public DateTime DataCriacao { get; set; }

        // A data de finalização é opcional (caso o carrinho tenha sido finalizado)
        public DateTime? DataFinalizacao { get; set; }

        // Indica se o carrinho foi finalizado ou não (opcional)
        public bool? Finalizado { get; set; }

        // O valor total do carrinho deve ser maior que zero
        [Required(ErrorMessage = "O valor total do carrinho é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor total deve ser maior que zero.")]
        public decimal ValorTotal { get; set; }

        // Relacionamento com os itens do carrinho
        public ICollection<ItemCarrinho> ItensCarrinhos { get; set; } = new List<ItemCarrinho>();
    }
}
