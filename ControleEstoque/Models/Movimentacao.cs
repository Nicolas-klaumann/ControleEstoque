using System.ComponentModel.DataAnnotations;

namespace ControleEstoque.Models
{
    public class Movimentacao
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ProdutoId { get; set; }
        public Guid FornecedorId { get; set; }
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "Tipo da Movimentação é obrigatorio")]
        public required Boolean TipoMovimentacao { get; set; }

        [Required(ErrorMessage = "Quantidade da Movimentação é obrigatorio")]
        public required string Quantidade{ get; set; }


        private DateTime _data = DateTime.Today;

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }

        // Relacionamentos com outras entidades
        public Produto Produto { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Cliente Cliente { get; set; }
    }
}
