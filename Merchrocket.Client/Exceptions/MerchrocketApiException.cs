namespace Merchrocket.Client.Exceptions;

public class MerchrocketApiException: Exception
{
    public MerchrocketApiException(string message): base(message)
    {
    }
    
    public MerchrocketApiException(string message, Exception innerException) : base(message, innerException)
    {
    }
}