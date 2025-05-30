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
                entity.Property(p => p.Altura).HasPrecision(10, 2);
                entity.Property(p => p.Largura).HasPrecision(10, 2);
                entity.Property(p => p.Comprimento).HasPrecision(10, 2);
            });
        }
    }
}
