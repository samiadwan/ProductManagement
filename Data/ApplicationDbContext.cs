using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        //{
        //ApplicationDbContext for multiple database
        //}

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Name)
                      .IsRequired(); 

                entity.Property(u => u.Email)
                      .IsRequired(); 
            });

            // Address
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(a => a.Street)
                      .IsRequired(); 

                entity.Property(a => a.City)
                      .IsRequired();

                entity.Property(a => a.PostalCode)
                      .IsRequired(); 

                entity.HasOne(a => a.User)
                      .WithOne(u => u.Address)
                      .HasForeignKey<Address>(a => a.UserId); // One-to-One relationship
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                      .IsRequired(); 

                entity.Property(p => p.Price)
                      .IsRequired(); // Required field with decimal type
            });

            // Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.OrderDate)
                      .IsRequired(); 

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId); // One-to-Many relationship
            });

            // OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => new { oi.OrderId, oi.ProductId }); // Composite primary key

                entity.Property(oi => oi.Quantity)
                      .IsRequired(); 

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId);
            });
        }

    }
}
