using Ecommerce.Domain.Customers;
using Xunit;

namespace Ecommerce.Tests.Customers;

public sealed class CustomerTests
{
    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenFullNameIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Customer(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "",
                "customer@email.com",
                "12345678900"));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenEmailIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Customer(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Ana Silva",
                "",
                "12345678900"));
    }

    [Fact]
    public void Constructor_ShouldThrowInvalidOperationException_WhenDocumentIsEmpty()
    {
        Assert.Throws<InvalidOperationException>(() =>
            new Customer(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Ana Silva",
                "customer@email.com",
                ""));
    }

    [Fact]
    public void AddAddress_ShouldKeepOnlyOneDefaultAddress_WhenNewDefaultAddressIsAdded()
    {
        var customer = new Customer(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Ana Silva",
            "customer@email.com",
            "12345678900");

        var firstAddress = new Address(
            Guid.NewGuid(),
            "First Street",
            "100",
            "Center",
            "Sao Paulo",
            "SP",
            "01001000",
            isDefault: true);

        var secondAddress = new Address(
            Guid.NewGuid(),
            "Second Street",
            "200",
            "Center",
            "Sao Paulo",
            "SP",
            "02002000",
            isDefault: true);

        customer.AddAddress(firstAddress);
        customer.AddAddress(secondAddress);

        Assert.False(firstAddress.IsDefault);
        Assert.True(secondAddress.IsDefault);
    }
}