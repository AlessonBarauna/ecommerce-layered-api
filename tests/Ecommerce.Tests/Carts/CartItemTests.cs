using Ecommerce.Domain.Carts;
using Xunit;

namespace Ecommerce.Tests.Carts;

public sealed class CartItemTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenProductIdIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new CartItem(
                Guid.Empty,
                "Notebook",
                1,
                3500));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenProductNameIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new CartItem(
                Guid.NewGuid(),
                "",
                1,
                3500));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenQuantityIsZero()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new CartItem(
                Guid.NewGuid(),
                "Notebook",
                0,
                3500));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenUnitPriceIsZero()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new CartItem(
                Guid.NewGuid(),
                "Notebook",
                1,
                0));
    }

    [Fact]
    public void Subtotal_ShouldReturnQuantityMultipliedByUnitPrice()
    {
        var item = new CartItem(
            Guid.NewGuid(),
            "Notebook",
            2,
            3500);

        Assert.Equal(7000, item.Subtotal);
    }

    [Fact]
    public void IncreaseQuantity_ShouldIncreaseQuantity_WhenQuantityIsValid()
    {
        var item = new CartItem(
            Guid.NewGuid(),
            "Notebook",
            2,
            3500);

        item.IncreaseQuantity(3);

        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void ChangeQuantity_ShouldChangeQuantity_WhenQuantityIsValid()
    {
        var item = new CartItem(
            Guid.NewGuid(),
            "Notebook",
            2,
            3500);

        item.ChangeQuantity(10);

        Assert.Equal(10, item.Quantity);
    }
}