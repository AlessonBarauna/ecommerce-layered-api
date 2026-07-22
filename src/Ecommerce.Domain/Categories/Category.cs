using System.Dynamic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Ecommerce.Domain.Categories;

public sealed class Category
{
    public Category(
        Guid id,
        string name,
        string description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void Update(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }

    public void Desactivate()
    {
        IsActive = false;
    }
    public void Activate()
    {
        IsActive = true;
    }
}