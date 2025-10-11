using SabaRayane.Contract.Core.Aggregates.CustomerAggregate;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SabaRayane.Contract.Core.Aggregates.MessageTemplateAggregate;

public class PlaceHolder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; private set; }

    [StringLength(50)]
    public string Title { get; private set; }
    
    [StringLength(50)]
    public string Name { get; private set; }

    [NotMapped]
    public Func<Customer, string?> GetValue { get; init; }

    private PlaceHolder(int id, string title, string name)
    {
        Id = id;
        Title = title;
        Name = name;
    }

    private PlaceHolder(int id, string title, string name, Func<Customer, string?> retrieveFunction) : this(id, title, name)
    {
        GetValue = retrieveFunction;
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public static readonly PlaceHolder CustomerName = new(1, "Customer", $"@Customer", c => { return c.FullName; });
    public static readonly PlaceHolder StoreName = new(2, "Shop", $"@Shop", c => { return c.StoreName; });
    public static readonly PlaceHolder ProductName = new(3, "Product", $"@Product", c => { return c.Agreements.FirstOrDefault()?.Product.Name; });
    public static readonly PlaceHolder ProductPrice = new(4, "Price", $"@Price", c => { return c.Agreements.FirstOrDefault()?.Product.Price.ToString(); });
    public static readonly PlaceHolder ExtraUsersCount = new(6, "ExtraUserCount", "@ExtraUserCount", c => { return c.Agreements.FirstOrDefault()?.ExtraUsersCount.ToString(); });

    public static Dictionary<string, PlaceHolder> PlaceHolders = new() 
    {
        { CustomerName.Name, CustomerName },
        { StoreName.Name, StoreName },
        { ProductName.Name, ProductName },
        { ProductPrice.Name, ProductPrice },
        { ExtraUsersCount.Name, ExtraUsersCount },
    };
}