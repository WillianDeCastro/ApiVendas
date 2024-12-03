using Microsoft.EntityFrameworkCore;
using Vendas.Domain.Entities;

namespace Vendas.Data.Contexts
{
    public class VendasContext : DbContext
    {
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }

        public VendasContext(DbContextOptions<VendasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relacionamento de Venda e ItemVenda
            modelBuilder.Entity<Venda>()
                .HasMany(v => v.Itens)
                .WithOne(iv => iv.Venda)
                .HasForeignKey(iv => iv.VendaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar relacionamento de Cliente e Venda
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Vendas)
                .WithOne(v => v.Cliente)
                .HasForeignKey(v => v.ClienteId);

            // Configurar relacionamento de Filial e Venda
            modelBuilder.Entity<Filial>()
                .HasMany(f => f.Vendas)
                .WithOne(v => v.Filial)
                .HasForeignKey(v => v.FilialId);

            // Configurar relacionamento de Produto e ItemVenda
            modelBuilder.Entity<Produto>()
                .HasMany(p => p.ItensVenda)
                .WithOne(iv => iv.Produto)
                .HasForeignKey(iv => iv.ProdutoId);
        }
    }
}
