using Microsoft.EntityFrameworkCore;
using SocialSearchTool.Models;

namespace SocialSearchTool.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<BookmarkPost> Bookmarks { get; set; }  
        public DbSet<SocialMediaPost> SocialMediaPosts { get; set; }
        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
