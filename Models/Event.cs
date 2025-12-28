using System.Net.Sockets;

namespace VillaManagementWeb.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = null!; // Sân khấu ngoài trời, Trong nhà
        public int TotalTickets { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = null!;
    }
}
