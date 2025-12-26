namespace VillaManagementWeb.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string TourName { get; set; }
        public string Description { get; set; }
        public decimal PricePerPerson { get; set; }
        public int DurationHours { get; set; }

        public ICollection<TourBooking> TourBookings { get; set; }
    }
}
