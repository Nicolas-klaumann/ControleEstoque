using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Data
{
    public class ControleEstoqueContext : DbContext
    {
        public ControleEstoqueContext(DbContextOptions<ControleEstoqueContext> options ) : base (options)
        {                
        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Movimentacao> Movimentacao { get; set; }
        
    }
}
