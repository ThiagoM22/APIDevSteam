using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class JogoCategoria
    {
        // Chave primária da relação entre Jogo e Categoria
        [Key]
        public Guid JogoCategoriaId { get; set; }

        // O ID do jogo é obrigatório para associar a categoria a um jogo específico
        [Required(ErrorMessage = "O ID do jogo é obrigatório.")]
        public Guid JogoId { get; set; }

        // Propriedade de navegação para o jogo
        public Jogo? Jogo { get; set; }

        // O ID da categoria é obrigatório para associar o jogo a uma categoria
        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        public Guid CategoriaId { get; set; }

        // Propriedade de navegação para a categoria
        public Categoria? Categoria { get; set; }
    }
}
