using Ecommerce.Domain.Customers;
using Xunit;

namespace Ecommerce.Tests.Customers;

public sealed class AddressTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenStreetIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Address(
                Guid.NewGuid(),
                "",
                "100",
                "Center",
                "São Paulo",
                "SP",
                "01001000",
                isDefault: true));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenCityIsEmpty()
    {
       Assert.Throws<InvalidOperationException>(() =>
            new Address(
                Guid.NewGuid(),
                "First Street",
                "100",
                "Center",
                "",
                "SP",
                "01001000",
                isDefault: true));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenStateIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Address(
                Guid.NewGuid(),
                "First Street",
                "100",
                "Center",
                "",
                "",
                "01001000",
                isDefault: true));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenZipCodeIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Address(
                Guid.NewGuid(),
                "First Street",
                "100",
                "Center",
                "Sao Paulo",
                "SP",
                "",
                isDefault: true));
    }
}