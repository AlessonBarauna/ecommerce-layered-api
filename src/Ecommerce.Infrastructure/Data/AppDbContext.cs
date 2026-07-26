using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(builder =>
        {
            builder.ToTable("categories");

            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(category => category.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(category => category.IsActive)
                .IsRequired();

            builder.Property(category => category.CreatedAt)
                .IsRequired();
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.ToTable("products");

            builder.HasKey(product => product.Id);

            builder.Property(product => product.CategoryId)
                .IsRequired();

            builder.Property(product => product.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(product => product.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(product => product.Price)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(product => product.StockQuantity)
                .IsRequired();

            builder.Property(product => product.IsActive)
                .IsRequired();

            builder.Property(product => product.CreatedAt)
                .IsRequired();

            builder.HasIndex(product => product.CategoryId);
        });
    }
}