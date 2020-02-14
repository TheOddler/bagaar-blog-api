using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Models
{
    public class BlogContext : DbContext
    {
        // This tells EF to make a table for posts called Posts
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {

        }
    }
}