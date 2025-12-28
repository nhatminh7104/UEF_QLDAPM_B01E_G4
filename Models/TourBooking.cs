namespace VillaManagementWeb.Models
{
    public class TourBooking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; } = null!;

        public DateTime TourDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public string ContactInfo { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
