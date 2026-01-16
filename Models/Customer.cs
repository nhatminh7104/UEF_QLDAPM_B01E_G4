using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VillaManagementWeb.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        public string? Address { get; set; }
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


        public ICollection<RoomBooking> Bookings { get; set; }


        public ICollection<Ticket> Tickets { get; set; }


        public ICollection<TourBooking> TourBookings { get; set; }

    }
}