using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public class CommentsRepository : AbstractRepository<Comment>
    {
        protected override DbSet<Comment> DbSet => _context.Comments;

        public CommentsRepository(BlogContext context) : base(context)
        {
        }
    }
}