using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatorio")]
        public required string Nome { get; set; }


        [Display(Name = "Endereço")]
        public string? Endereco { get; set; }

        [Required(ErrorMessage = "Telefone é obrigatorio")]
        public required string Telefone { get; set; }

        public string? Email { get; set; }
    }
}
