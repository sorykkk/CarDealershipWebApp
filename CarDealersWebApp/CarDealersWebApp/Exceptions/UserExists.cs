namespace CarDealersWebApp.Exceptions;

public class UserExists : CarDealerException
{
    public UserExists(string message) : base(message)
    {

    }

    public UserExists(string message, Exception innerException) : base(message, innerException) { }

}
