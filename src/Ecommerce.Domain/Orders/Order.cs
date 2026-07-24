namespace Ecommerce.Domain.Orders;

public sealed class Order
{
    private readonly List<OrderItem> _items = [];

    public Order(
        Guid id,
        Guid customerId,
        IReadOnlyCollection<OrderItem> items,
        decimal shippingAmount = 0,
        decimal discountAmount = 0)
    {
        if (customerId == Guid.Empty)
        {
            throw new InvalidOperationException("Customer id is required.");
        }
        
         if (items.Count == 0)
        {
            throw new InvalidOperationException("Order must have at least one item.");
        }

        if (shippingAmount < 0)
        {
            throw new InvalidOperationException("Shipping amount cannot be negative.");
        }

        if (discountAmount < 0)
        {
            throw new InvalidOperationException("Discount amount cannot be negative.");
        }

        Id = id;
        CustomerId = customerId;
        ShippingAmount = shippingAmount;
        DiscountAmount = discountAmount;
        Status = OrderStatus.PendingPayment;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        _items.AddRange(items);
    }

    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items;

    public decimal Subtotal => _items.Sum(item => item.Subtotal);

    public decimal ShippingAmount { get; private set; }

    public decimal DiscountAmount { get; private set; }

    public decimal Total => Subtotal + ShippingAmount - DiscountAmount;

    public OrderStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public void MarkAsPaid()
    {
        if (Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Only pending payment orders can be paid.");
        }

        Status = OrderStatus.Paid;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsShipped()
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Only paid orders can be shipped.");
        }

        Status = OrderStatus.Shipped;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsDelivered()
    {
        if (Status != OrderStatus.Shipped)
        {
            throw new InvalidOperationException("Only shipped orders can be delivered.");
        }

        Status = OrderStatus.Delivered;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status is OrderStatus.Shipped or OrderStatus.Delivered)
        {
            throw new InvalidOperationException("Shipped or delivered orders cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }
}