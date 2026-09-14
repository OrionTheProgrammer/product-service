namespace Product_Service.Exceptions;


public class SizeValueException : Exception
{
    public SizeValueException() { }

    public SizeValueException(string message) : base(message) { }

    public SizeValueException(string message, Exception innerException) : base(message, innerException) { }
}
