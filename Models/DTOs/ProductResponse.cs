using Product_Service.Models.Domain;

namespace Product_Service.Models.DTOs;


public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Category { get; set; } = null!;
    public int Price { get; set; }
    public ProductSizes Sizes { get; set; } = null!;

}
