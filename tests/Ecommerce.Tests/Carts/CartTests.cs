using Ecommerce.Domain.Carts;
using Xunit;

namespace Ecommerce.Tests.Carts;

public sealed class CartTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenCustomerIdIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Cart(
                Guid.NewGuid(),
                Guid.Empty));
    }

    [Fact]
    public void AddItem_ShouldAddNewItem_WhenProductIsNotInCart()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        var productId = Guid.NewGuid();

        cart.AddItem(
            productId,
            "Notebook",
            1,
            3500);

        Assert.Single(cart.Items);
    }

    [Fact]
    public void AddItem_ShouldIncreaseQuantity_WhenProductAlreadyExistsInCart()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        var productId = Guid.NewGuid();

        cart.AddItem(
            productId,
            "Notebook",
            1,
            3500);

        cart.AddItem(
            productId,
            "Notebook",
            2,
            3500);

        var item = cart.Items.Single();

        Assert.Equal(3, item.Quantity);
    }

    [Fact]
    public void Total_ShouldReturnSumOfItemSubtotals()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        cart.AddItem(
            Guid.NewGuid(),
            "Notebook",
            2,
            3500);

        cart.AddItem(
            Guid.NewGuid(),
            "Mouse",
            3,
            100);

        Assert.Equal(7300, cart.Total);
    }

    [Fact]
    public void ChangeItemQuantity_ShouldChangeQuantity_WhenItemExists()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        var productId = Guid.NewGuid();

        cart.AddItem(
            productId,
            "Notebook",
            1,
            3500);

        cart.ChangeItemQuantity(productId, 5);

        var item = cart.Items.Single();

        Assert.Equal(5, item.Quantity);
    }

    [Fact]
    public void ChangeItemQuantity_ShouldThrowInvalidOperationException_WhenItemDoesNotExist()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        Assert.Throws<InvalidOperationException>(() =>
            cart.ChangeItemQuantity(
                Guid.NewGuid(),
                5));
    }

    [Fact]
    public void RemoveItem_ShouldRemoveItem_WhenItemExists()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        var productId = Guid.NewGuid();

        cart.AddItem(
            productId,
            "Notebook",
            1,
            3500);

        cart.RemoveItem(productId);

        Assert.Empty(cart.Items);
    }

    [Fact]
    public void Clear_ShouldRemoveAllItems()
    {
        var cart = new Cart(
            Guid.NewGuid(),
            Guid.NewGuid());

        cart.AddItem(
            Guid.NewGuid(),
            "Notebook",
            1,
            3500);

        cart.AddItem(
            Guid.NewGuid(),
            "Mouse",
            2,
            100);

        cart.Clear();

        Assert.Empty(cart.Items);
    }
}