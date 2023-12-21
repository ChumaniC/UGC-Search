using System.ComponentModel.DataAnnotations;

namespace SocialSearchTool.Models
{
    public class SocialMediaPost
    {
        [Key]
        public int PostId { get; set; }

        public string ImageUrl { get; set; }
        public string Caption { get; set; }
        public string Username { get; set; }
    }
}