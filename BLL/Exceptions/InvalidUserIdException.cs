namespace BLL.Exceptions;

public class InvalidUserIdException : ArgumentException
{
    public InvalidUserIdException() : base() {}
    
    public InvalidUserIdException(string message) : base(message){}
    
    public InvalidUserIdException(string message, Exception innerException) : base(message, innerException){}
    
}