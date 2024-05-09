namespace CarDealersWebApp.Data.Entities
{
    public enum UserType
    { 
        Customer, 
        Dealer,
        Admin
    };

    public class User
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }

    }
}
