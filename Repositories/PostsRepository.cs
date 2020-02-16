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

        public override bool Update(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            _context.Entry(post).Property(p => p.Created).IsModified = false;
            return SaveChanges();
        }
    }
}