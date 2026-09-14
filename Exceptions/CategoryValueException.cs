namespace Product_Service.Exceptions;


public class CategoryValueException : Exception
{
    public CategoryValueException() { }

    public CategoryValueException(string message) : base(message) { }

    public CategoryValueException(string message, Exception innerException) : base(message, innerException) { }
}
