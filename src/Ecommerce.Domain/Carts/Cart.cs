namespace Ecommerce.Domain.Carts;

public sealed class Cart
{
    private readonly List<CartItem> _items = [];

    private Cart()
    {
        
    }

    public Cart(
        Guid id,
        Guid customerId)
    {
        if (customerId == Guid.Empty)
        {
            throw new InvalidOperationException("Customer id is required");
        }

        Id = id;
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items;

    public decimal Total => _items.Sum(item => item.Subtotal);

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void AddItem(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice)
    {
        var existingItem = _items.FirstOrDefault(item => item.ProductId == productId);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity);
            UpdatedAt = DateTime.UtcNow;
            return;
        }

        var item = new CartItem(
            productId,
            productName,
            quantity,
            unitPrice);

        _items.Add(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public void ChangeItemQuantity(
        Guid productId,
        int quantity)
    {
        var item = _items.FirstOrDefault(item => item.ProductId == productId);

        if (item is null)
        {
            throw new InvalidOperationException("Cart item was not found.");
        }

        item.ChangeQuantity(quantity);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(item => item.ProductId == productId);

        if (item is null)
        {
            return;
        }

        _items.Remove(item);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Clear()
    {
        _items.Clear();
        UpdatedAt = DateTime.UtcNow;
    }
}