using System;
using System.ComponentModel.DataAnnotations;

namespace APIDevSteam.Models
{
    public class Categoria
    {
        // Define o campo CategoriaId como chave primária
        [Key]
        public Guid CategoriaId { get; set; }

        // O nome da categoria é obrigatório e deve ter no máximo 100 caracteres
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        public string CategoriaName { get; set; }
    }
}
