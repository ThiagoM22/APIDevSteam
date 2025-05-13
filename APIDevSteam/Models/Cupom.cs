using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class Cupom
    {
        // Define o campo CupomId como chave primária
        [Key]
        public Guid CupomId { get; set; }

        // O nome do cupom é obrigatório e deve ter no máximo 100 caracteres
        [Required(ErrorMessage = "O nome do cupom é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do cupom deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        // O desconto deve estar entre 0 e 100
        [Required(ErrorMessage = "O desconto é obrigatório.")]
        [Range(0, 100, ErrorMessage = "O desconto deve estar entre 0% e 100%.")]
        public int Desconto { get; set; }

        // A data de validade é opcional, mas se for fornecida, deve ser maior que a data atual
        public DateTime? DataValidade { get; set; }

        // O cupom deve estar ativo ou inativo
        [Required(ErrorMessage = "O status de ativo é obrigatório.")]
        public bool Ativo { get; set; }

        // O limite de uso é opcional, mas se for fornecido, deve ser maior ou igual a 1
        [Range(1, int.MaxValue, ErrorMessage = "O limite de uso deve ser maior ou igual a 1.")]
        public int? LimiteUso { get; set; }

        // A data de criação é opcional, mas se for fornecida, deve ser uma data válida
        public DateTime? DataCriacao { get; set; }
    }
}
