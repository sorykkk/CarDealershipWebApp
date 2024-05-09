namespace CarDealersWebApp.Exceptions;

public class CarDealerException :Exception
{
    public CarDealerException(string message) : base(message)
    {
        
    }

    public CarDealerException(string message, Exception innerException) : base(message, innerException) { }

}


