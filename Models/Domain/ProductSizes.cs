namespace Product_Service.Models.Domain;


public class ProductSizes
{
    public Dictionary<Size, bool> Sizes { get; } = null!;

    public ProductSizes(Dictionary<Size, bool> sizes)
    {
        Sizes = sizes;
    }
}
