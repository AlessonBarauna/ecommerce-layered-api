using Ecommerce.Domain.Orders;
using Xunit;

namespace Ecommerce.Tests.Orders;

public sealed class OrderItemTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenProductIdIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new OrderItem(
                Guid.Empty,
                "Notebook",
                1,
                3500));
    }
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenProductNameIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new OrderItem(
                Guid.NewGuid(),
                "",
                1,
                3500));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenQuantityIsZero()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new OrderItem(
                Guid.NewGuid(),
                "Notebook",
                0,
                3500));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenUnitPriceIsZero()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new OrderItem(
                Guid.NewGuid(),
                "Notebook",
                1,
                0));
    }

    [Fact]
    public void Subtotal_ShouldReturnQuantityMultipliedByUnitPrice()
    {
        var item = new OrderItem(
            Guid.NewGuid(),
            "Notebook",
            2,
            3500);

        Assert.Equal(7000, item.Subtotal);
    }
}