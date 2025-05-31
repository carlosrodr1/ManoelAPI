using ManoelAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManoelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.OwnsOne(p => p.Dimensoes, dimensoes =>
                {
                    dimensoes.Property(d => d.Altura).HasPrecision(10, 2);
                    dimensoes.Property(d => d.Largura).HasPrecision(10, 2);
                    dimensoes.Property(d => d.Comprimento).HasPrecision(10, 2);
                });
            });
        }
    }
}
