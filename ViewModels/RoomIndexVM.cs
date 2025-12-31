namespace VillaManagementWeb.ViewModels
{
    public class RoomIndexVM
    {
        // All Room properties
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public string Type { get; set; } = null!;
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public int RatingStars { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public double? SquareFootage { get; set; }
        public bool HasWifi { get; set; }
        public bool HasBreakfast { get; set; }
        public bool HasPool { get; set; }
        public bool HasTowel { get; set; }

        // Rental status properties
        public string RentalStatus { get; set; } = null!; // "Available", "Occupied", "Reserved"
        public string StatusLabel { get; set; } = null!; // "Trống", "Đang cho thuê", "Đã đặt"
        public string StatusColor { get; set; } = null!; // Bootstrap badge class
    }
}

