using Microsoft.EntityFrameworkCore;
using PedidoItem.Domain.Entity.Cliente;
using PedidoItem.Domain.Entity.Pedido;
using PedidoItem.Domain.Entity.Produto;
using PedidoItemNameSpace = PedidoItem.Domain.Entity.PedidoItem;

namespace Infrastructure
{
    public class BancoContext : DbContext
    {
        public BancoContext()
        {
            // Database.EnsureCreated();
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<PedidoItemNameSpace.PedidoItem> PedidoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nome).IsRequired().HasColumnType("VARCHAR(200)");
                entity.Property(c => c.Email).IsRequired().HasColumnType("VARCHAR(200)");
                entity.Property(c => c.Endereco).IsRequired().HasColumnType("VARCHAR(255)");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.ToTable("Produtos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nome).IsRequired().HasColumnType("VARCHAR(200)");
                entity.Property(p => p.Preco).IsRequired().HasColumnType("DECIMAL(10,2)");
                entity.Property(p => p.Descricao).HasColumnType("TEXT");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("Pedidos");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Data).IsRequired().HasColumnType("TIMESTAMP");
                
                entity.HasOne(p => p.Cliente)
                    .WithMany()
                    .HasForeignKey("ClienteId")
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            // Configuração da tabela PedidoItem
            modelBuilder.Entity<PedidoItemNameSpace.PedidoItem>(entity =>
            {
                entity.ToTable("PedidoItems");
                entity.HasKey(pi => pi.Id);

                entity.HasOne(pi => pi.Produto)
                    .WithMany()
                    .HasForeignKey("ProdutoId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey("PedidoId")
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=tdd;Username=postgres;Password=admin");
        }
    }
}