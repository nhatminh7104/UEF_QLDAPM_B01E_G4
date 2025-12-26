using System.Net.Sockets;

namespace VillaManagementWeb.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; } // Sân khấu ngoài trời, Trong nhà
        public int TotalTickets { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
