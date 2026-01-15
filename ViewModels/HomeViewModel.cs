using System.Collections.Generic;
using VillaManagementWeb.Models;

namespace VillaManagementWeb.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Room> Rooms { get; set; } = new List<Room>();
        public IEnumerable<RoomCategory> Categories { get; set; } = new List<RoomCategory>();
        public IEnumerable<Tour> Tours { get; set; } = new List<Tour>();
        public IEnumerable<Event> Events { get; set; } = new List<Event>();
        public IEnumerable<News> NewsItems { get; set; } = new List<News>();
    }
}