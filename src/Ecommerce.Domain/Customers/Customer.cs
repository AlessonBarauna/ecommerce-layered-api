namespace Ecommerce.Domain.Customers;

public sealed class Customer
{
    private readonly List<Address> _addresses = [];

    public Customer(
        Guid id,
        Guid userId,
        string fullName,
        string email,
        string document)
    {
        if (userId == Guid.Empty)
        {
            throw new InvalidOperationException("Customer user id is required.");
        }

        if (string.IsNullOrWhiteSpace(fullName))
        {
            throw new InvalidOperationException("Customer full name is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException("Customer email is required.");
        }

        if (string.IsNullOrWhiteSpace(document))
        {
            throw new InvalidOperationException("Customer document is required.");
        }

        Id = id;
        UserId = userId;
        FullName = fullName;
        Email = email;
        Document = document;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string FullName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Document { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses;
    public void AddAddress(Address address)
    {
        if (address.IsDefault)
        {
            foreach (var currentAddress in _addresses)
            {
                currentAddress.RemoveDefault();
            }
        }

        _addresses.Add(address);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}