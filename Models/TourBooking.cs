using System.ComponentModel.DataAnnotations.Schema;

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
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }
        public string? ContactInfo { get; set; }
        public string CustomerName { get; set; } = null!;
        public string? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";
    }
}