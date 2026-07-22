using Ecommerce.Domain.Products;
using Xunit;

namespace Ecommerce.Tests.Products;

public sealed class ProductTests
{
    [Fact]
    public void Construtor_ShowldThrowInvalidOperationException_WhenPriceIsZero()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Product(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Notebook",
                "Notebook gamer",
                0,
                10));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenStockQuantityIsNegative()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Product(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Notebook",
                "Notebook gamer",
                3500,
                -1));
    }
    [Fact]
    public void DecreaseStock_ShouldThrowInvalidOperationException_WhenQuantityIsGreaterThanStock()
    {
        var product = new Product(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Notebook",
            "Notebook gamer",
            3500,
            5);

        Assert.Throws<InvalidOperationException>(() => product.DecreaseStock(6));
    }

    [Fact]
    public void DecreaseStock_ShouldDecreaseStockQuantity_WhenQuantityIsAvailable()
    {
        var product = new Product(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Notebook",
            "Notebook gamer",
            3500,
            5);

        product.DecreaseStock(2);

        Assert.Equal(3, product.StockQuantity);
    }
}