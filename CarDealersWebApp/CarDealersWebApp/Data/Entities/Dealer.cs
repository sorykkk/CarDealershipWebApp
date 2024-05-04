namespace CarDealersWebApp.Data.Entities
{
    public class Dealer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public bool IsSelling { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
