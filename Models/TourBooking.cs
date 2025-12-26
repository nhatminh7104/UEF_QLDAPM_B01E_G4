namespace VillaManagementWeb.Models
{
    public class TourBooking
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public DateTime TourDate { get; set; }
        public int NumberOfPeople { get; set; }
        public decimal TotalPrice { get; set; }
        public string ContactInfo { get; set; }
    }
}
