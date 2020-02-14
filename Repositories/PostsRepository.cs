using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public class PostsRepository : AbstractRepository<Post>
    {
        protected override DbSet<Post> DbSet => _context.Posts;

        public PostsRepository(BlogContext context) : base(context)
        {
        }
    }
}