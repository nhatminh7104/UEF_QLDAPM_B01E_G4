namespace VillaManagementWeb.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string TourName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal PricePerPerson { get; set; }
        public int DurationHours { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<TourBooking> TourBookings { get; set; } = null!;
    }
}
