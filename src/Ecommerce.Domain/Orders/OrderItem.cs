namespace Ecommerce.Domain.Orders;

public sealed class OrderItem
{
    public OrderItem(
        Guid productId,
        string productName,
        int quantity,
        decimal unitPrice)
    {
        if (productId == Guid.Empty)
        {
            throw new InvalidOperationException("Product id is required.");
        }

        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new InvalidOperationException("Product name is required.");
        }

        if (quantity <= 0)
        {
            throw new InvalidOperationException("Order item quantity must be greater than zero.");
        }

        if (unitPrice <= 0)
        {
            throw new InvalidOperationException("Order item unit price must be greater than zero.");
        }

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid ProductId {get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice  { get; private set; }
    public decimal Subtotal => Quantity * UnitPrice;
}