namespace CarDealersWebApp.Exceptions
{
    public class LoginException : CarDealerException
    {

        public LoginException(string message) : base(message)
        {

        }

        public LoginException(string message, Exception innerException) : base(message, innerException) { }

    }
}
