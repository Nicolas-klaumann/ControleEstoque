using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleEstoque.Models
{
    public class Movimentacao
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Produto))]
        public required Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [ForeignKey(nameof(Fornecedor))]
        public Guid? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        [ForeignKey(nameof(Cliente))]
        public Guid? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Tipo da Movimentação é obrigatorio")]
        public required string TipoMovimentacao { get; set; }

        [Required(ErrorMessage = "Quantidade da Movimentação é obrigatorio")]
        public required string Quantidade { get; set; }

        private DateTime _data = DateTime.Today;

        [Required(ErrorMessage = "Data é obrigatória")]
        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }
        }
    }
}
