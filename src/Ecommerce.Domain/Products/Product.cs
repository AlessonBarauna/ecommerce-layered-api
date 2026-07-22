using Ecommerce.Domain.Categories;

namespace Ecommerce.Domain.Products;

public sealed class Product
{
    public Product(
        Guid id,
        Guid categoryId,
        string name,
        string description,
        decimal price,
        int stockQuantity)
    {
        if (price <= 0)
        {
            throw new InvalidOperationException("Product price must be greater than zero. ");
        }
        if (stockQuantity < 0)
        {
            throw new InvalidOperationException("Product stock quantity cannot be negative.");
        }

        Id = id;
        CategoryId = categoryId;
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid CategoryId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void UpdateDetails(
        string name,
        string description,
        decimal price)
    {
        if (price <= 0)
        { 
            throw new InvalidOperationException("Product price must be greater than zero.");
        }

        Name = name;
        Description = description;
        Price = price;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than zero.");
        }

        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than zero.");
        }

        if (quantity > StockQuantity)
        {
            throw new InvalidOperationException("Insufficient stock.");
        }

        StockQuantity -= quantity;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}