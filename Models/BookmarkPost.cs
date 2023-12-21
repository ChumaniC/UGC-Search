using System.ComponentModel.DataAnnotations;

namespace SocialSearchTool.Models
{
    public class BookmarkPost
    {
        [Key]
        public int BookmarkId { get; set; }

        public int SearchId { get; set; }
        public int PostId { get; set; }
        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string Username { get; set; }
        public DateTime SearchDate { get; set; } = DateTime.Now;
    }
}