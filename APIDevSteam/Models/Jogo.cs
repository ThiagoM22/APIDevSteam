using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class Jogo
    {
        // Define o campo JogoId como chave primária
        [Key]
        public Guid? JogoId { get; set; }

        // O título do jogo é obrigatório e deve ter no máximo 100 caracteres
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; }

        // O preço do jogo é obrigatório e deve ser maior que zero
        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        // O desconto deve estar entre 0% e 100%
        [Range(0, 100, ErrorMessage = "O desconto deve estar entre 0% e 100%.")]
        public int Desconto { get; set; }

        // A URL da imagem é obrigatória e deve ser uma URL válida
        [Required(ErrorMessage = "A URL da imagem é obrigatória.")]
        [Url(ErrorMessage = "A URL da imagem não é válida.")]
        public string UrlImagem { get; set; }

        // O banner é obrigatório e deve ser uma URL válida
        [Required(ErrorMessage = "O banner é obrigatório.")]
        [Url(ErrorMessage = "A URL do banner não é válida.")]
        public string Banner { get; set; }

        // A descrição do jogo é obrigatória e deve ter no máximo 500 caracteres
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; }

        // O preço original é opcional, mas se for fornecido, deve ser maior que zero
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço original deve ser maior que zero.")]
        public decimal? PrecoOriginal { get; set; }
    }
}
