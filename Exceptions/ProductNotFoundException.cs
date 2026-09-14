namespace Product_Service.Exceptions;


public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int id) : base($"No existe el producto ligado al ID {id}") { }

    public ProductNotFoundException(string message) : base(message) { }
}
