using Ecommerce.Domain.Orders;
using Xunit;

namespace Ecommerce.Tests.Orders;

public sealed class OrderTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenCustomerIdIsEmpty()
    {
        var items = CreateItems();

        Assert.Throws<InvalidOperationException>(() =>
            new Order(
                Guid.NewGuid(),
                Guid.Empty,
                items));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenItemsIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Order(
                Guid.NewGuid(),
                Guid.NewGuid(),
                []));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenShippingAmountIsNegative()
    {
        var items = CreateItems();

        Assert.Throws<InvalidOperationException>(() =>
            new Order(
                Guid.NewGuid(),
                Guid.NewGuid(),
                items,
                shippingAmount: -1));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenDiscountAmountIsNegative()
    {
        var items = CreateItems();

        Assert.Throws<InvalidOperationException>(() =>
            new Order(
                Guid.NewGuid(),
                Guid.NewGuid(),
                items,
                discountAmount: -1));
    }

    [Fact]
    public void Constructor_ShouldCreateOrderWithPendingPaymentStatus()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        Assert.Equal(OrderStatus.PendingPayment, order.Status);
    }

    [Fact]
    public void Subtotal_ShouldReturnSumOfItemSubtotals()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        Assert.Equal(7300, order.Subtotal);
    }

    [Fact]
    public void Total_ShouldReturnSubtotalPlusShippingMinusDiscount()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems(),
            shippingAmount: 50,
            discountAmount: 100);

        Assert.Equal(7250, order.Total);
    }

    [Fact]
    public void MarkAsPaid_ShouldChangeStatusToPaid_WhenOrderIsPendingPayment()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        order.MarkAsPaid();

        Assert.Equal(OrderStatus.Paid, order.Status);
    }

    [Fact]
    public void MarkAsShipped_ShouldChangeStatusToShipped_WhenOrderIsPaid()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        order.MarkAsPaid();
        order.MarkAsShipped();

        Assert.Equal(OrderStatus.Shipped, order.Status);
    }

    [Fact]
    public void MarkAsDelivered_ShouldChangeStatusToDelivered_WhenOrderIsShipped()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        order.MarkAsPaid();
        order.MarkAsShipped();
        order.MarkAsDelivered();

        Assert.Equal(OrderStatus.Delivered, order.Status);
    }

    [Fact]
    public void Cancel_ShouldChangeStatusToCancelled_WhenOrderIsPendingPayment()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        order.Cancel();

        Assert.Equal(OrderStatus.Cancelled, order.Status);
    }

    [Fact]
    public void Cancel_ShouldThrowInvalidOperationException_WhenOrderIsShipped()
    {
        var order = new Order(
            Guid.NewGuid(),
            Guid.NewGuid(),
            CreateItems());

        order.MarkAsPaid();
        order.MarkAsShipped();

        Assert.Throws<InvalidOperationException>(() => order.Cancel());
    }

    private static IReadOnlyCollection<OrderItem> CreateItems()
    {
        return
        [
            new OrderItem(
                Guid.NewGuid(),
                "Notebook",
                2,
                3500),

            new OrderItem(
                Guid.NewGuid(),
                "Mouse",
                3,
                100)
        ];
    }
}