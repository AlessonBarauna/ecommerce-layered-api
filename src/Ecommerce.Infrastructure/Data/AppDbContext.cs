using Ecommerce.Domain.Categories;
using Ecommerce.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Carts;
using Ecommerce.Domain.Customers;
using Ecommerce.Domain.Orders;
using Ecommerce.Domain.Users;

namespace Ecommerce.Infrastructure.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();

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

        modelBuilder.Entity<Customer>(builder =>
        {
            builder.ToTable("customers");

            builder.HasKey(customer => customer.Id);

            builder.Property(customer => customer.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(customer => customer.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(customer => customer.Document)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(customer => customer.IsActive)
                .IsRequired();

            builder.Property(customer => customer.CreatedAt)
                .IsRequired();

            builder.HasIndex(customer => customer.Email)
                .IsUnique();

            builder.OwnsMany(customer => customer.Addresses, addressBuilder =>
            {
                addressBuilder.ToTable("customer_addresses");

                addressBuilder.WithOwner()
                    .HasForeignKey("CustomerId");

                addressBuilder.HasKey("Id");

                addressBuilder.Property(address => address.Id)
                    .ValueGeneratedNever();

                addressBuilder.Property(address => address.Street)
                    .IsRequired()
                    .HasMaxLength(200);

                addressBuilder.Property(address => address.Number)
                    .IsRequired()
                    .HasMaxLength(30);

                addressBuilder.Property(address => address.Neighborhood)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(address => address.City)
                    .IsRequired()
                    .HasMaxLength(100);

                addressBuilder.Property(address => address.State)
                    .IsRequired()
                    .HasMaxLength(2);

                addressBuilder.Property(address => address.ZipCode)
                    .IsRequired()
                    .HasMaxLength(20);

                addressBuilder.Property(address => address.IsDefault)
                    .IsRequired();

                addressBuilder.Property(address => address.CreatedAt)
                    .IsRequired();
            });
        });

        modelBuilder.Entity<Cart>(builder =>
        {
            builder.ToTable("carts");

            builder.HasKey(cart => cart.Id);

            builder.Property(cart => cart.CustomerId)
                .IsRequired();

            builder.Property(cart => cart.CreatedAt)
                .IsRequired();

            builder.Property(cart => cart.UpdatedAt)
                .IsRequired();

            builder.HasIndex(cart => cart.CustomerId)
                .IsUnique();

            builder.OwnsMany(cart => cart.Items, itemBuilder =>
            {
                itemBuilder.ToTable("cart_items");

                itemBuilder.WithOwner()
                    .HasForeignKey("CartId");

                itemBuilder.Property<Guid>("Id");

                itemBuilder.HasKey("Id");

                itemBuilder.Property(item => item.ProductId)
                    .IsRequired();

                itemBuilder.Property(item => item.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                itemBuilder.Property(item => item.Quantity)
                    .IsRequired();

                itemBuilder.Property(item => item.UnitPrice)
                    .IsRequired()
                    .HasPrecision(18, 2);
            });
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.ToTable("orders");

            builder.HasKey(order => order.Id);

            builder.Property(order => order.CustomerId)
                .IsRequired();

            builder.Property(order => order.ShippingAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(order => order.DiscountAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(order => order.Status)
                .IsRequired();

            builder.Property(order => order.CreatedAt)
                .IsRequired();

            builder.Property(order => order.UpdatedAt)
                .IsRequired();

            builder.HasIndex(order => order.CustomerId);

            builder.OwnsMany(order => order.Items, itemBuilder =>
            {
                itemBuilder.ToTable("order_items");

                itemBuilder.WithOwner()
                    .HasForeignKey("OrderId");

                itemBuilder.Property<Guid>("Id");

                itemBuilder.HasKey("Id");

                itemBuilder.Property(item => item.ProductId)
                    .IsRequired();

                itemBuilder.Property(item => item.ProductName)
                    .IsRequired()
                    .HasMaxLength(200);

                itemBuilder.Property(item => item.Quantity)
                    .IsRequired();

                itemBuilder.Property(item => item.UnitPrice)
                    .IsRequired()
                    .HasPrecision(18, 2);
            });
        });
        
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(user => user.Role)
                .IsRequired();

            builder.Property(user => user.IsActive)
                .IsRequired();

            builder.Property(user => user.CreatedAt)
                .IsRequired();

            builder.HasIndex(user => user.Email)
                .IsUnique();
        });    
    }
}