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

        public override bool Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            _context.Entry(comment).Property(c => c.Created).IsModified = false;
            return SaveChanges();
        }
    }
}