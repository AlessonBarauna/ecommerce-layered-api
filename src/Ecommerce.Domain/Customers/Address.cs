namespace Ecommerce.Domain.Customers;

public sealed class Address
{
    public Address(
        Guid id,
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string zipCode,
        bool isDefault)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            throw new InvalidOperationException("Address street is required.");
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new InvalidOperationException("Address city is required.");
        }

        if (string.IsNullOrWhiteSpace(zipCode))
        {
            throw new InvalidOperationException("Address zip code is required.");
        }

        Id = id;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        IsDefault  = isDefault;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }

    public string ZipCode { get; private set; }

    public bool IsDefault { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public void MarkAsDefault()
    {
        IsDefault = true;
    }

    public void RemoveDefault()
    {
        IsDefault = false;
    }
}