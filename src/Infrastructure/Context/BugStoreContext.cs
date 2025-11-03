using BugStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure.Context;

public class BugStoreContext(DbContextOptions<BugStoreContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Customer>().HasKey(x => x.Id);
        modelBuilder.Entity<Customer>().Property(x => x.Name).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
        modelBuilder.Entity<Customer>().Property(x => x.Email).IsRequired().HasMaxLength(180).HasColumnType("varchar(180)");
        modelBuilder.Entity<Customer>().Property(x => x.Phone).IsRequired(false).HasMaxLength(20).HasColumnType("varchar(20)");
        modelBuilder.Entity<Customer>().Property(x => x.BirthDate).IsRequired();

        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Product>().HasKey(x => x.Id);
        modelBuilder.Entity<Product>().Property(x => x.Title).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
        modelBuilder.Entity<Product>().Property(x => x.Description).IsRequired(false).HasMaxLength(1024).HasColumnType("varchar(1024)");
        modelBuilder.Entity<Product>().Property(x => x.Slug).IsRequired().HasMaxLength(120).HasColumnType("varchar(120)");
        modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired().HasColumnType("money");

        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<Order>().HasKey(x => x.Id);
        modelBuilder.Entity<Order>().Property(x => x.CreatedAt).IsRequired();
        modelBuilder.Entity<Order>().Property(x => x.UpdatedAt).IsRequired();
        modelBuilder.Entity<Order>().HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId);
        modelBuilder.Entity<Order>().HasMany(x => x.Lines).WithOne().HasForeignKey(x => x.OrderId);

        modelBuilder.Entity<OrderLine>().ToTable("OrderLine");
        modelBuilder.Entity<OrderLine>().HasKey(x => x.Id);
        modelBuilder.Entity<OrderLine>().Property(x => x.Quantity).IsRequired();
        modelBuilder.Entity<OrderLine>().Property(x => x.Total).IsRequired().HasColumnType("money");
        modelBuilder.Entity<OrderLine>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
    }
}