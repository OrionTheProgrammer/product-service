namespace Product_Service.Exceptions;


public class ProductConflictException : Exception
{
    public ProductConflictException(string message) : base(message) { }
    public ProductConflictException(string message, Exception innerException) : base(message, innerException) { }
}
