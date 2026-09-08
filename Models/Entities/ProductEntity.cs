using Product_Service.Models.Domain;

namespace Product_Service.Models.Entities;


public class ProductEntity
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductBrand { get; set; } = null!;
    public Category ProductCategory { get; set; } = null!;
    public int ProductPrice { get; set; }
    public ProductSizes ProductSizes { get; set; } = null!;

}
