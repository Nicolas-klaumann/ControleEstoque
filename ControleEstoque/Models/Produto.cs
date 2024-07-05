using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Produto
    {
        [Key]   
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é Obrigatorio")]
        public required string Nome { get; set; }


        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Preço é Obrigatorio")]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Quantidade de Estoque é Obrigatorio")]
        [Display(Name = "Quantidade de Estoque")]
        public int QuantidadeEstoque{ get; set; }
    }
}
