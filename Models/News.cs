namespace VillaManagementWeb.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Category { get; set; } // Ví dụ: Sự kiện âm nhạc, Tin tức...
        public string? ImageUrl { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}