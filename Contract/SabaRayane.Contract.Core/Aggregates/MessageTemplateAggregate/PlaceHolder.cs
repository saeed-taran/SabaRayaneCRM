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
    public string Name { get; private set; }
    
    [StringLength(50)]
    public string Path { get; private set; }

    private PlaceHolder(int id, string name, string path)
    {
        Id = id;
        Name = name;
        Path = path;
    }

    public static PlaceHolder CustomerName = new(1, "نام_مشتری", $"{nameof(Customer.FullName)}");
    public static PlaceHolder StoreName = new(2, "نام_فروشگاه", "StoreName");
    public static PlaceHolder ProductName = new(3, "نام_محصول", "Product.Name");
    public static PlaceHolder ProductPrice = new(4, "مبلغ_تمدید", "Product.Price");
    public static PlaceHolder ExpireDate = new(5, "تاریخ_انقضا", "Agreement.ExpireDate");
    public static PlaceHolder ExtraUsersCount = new(6, "تعداد_کاربران_اضافه", "Agreement.ExtraUsersCount");

    public static List<PlaceHolder> PlaceHolders = new() 
    {
        CustomerName,
        StoreName,
        ProductName,
        ProductPrice,
        ExpireDate,
        ExtraUsersCount,
    };
}