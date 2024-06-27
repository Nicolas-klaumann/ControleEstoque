using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Produto
    {
        [Key]   
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é Obrigatorio")]
        public required string Nome { get; set; }

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Preço é Obrigatorio")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Quantidade de Estoque é Obrigatorio")]
        public int QuantidadeEstoque{ get; set; }
    }
}
