using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public class CommentsRepository : IRepository<Comment>
    {
        private readonly BlogContext _context;

        public CommentsRepository(BlogContext context)
        {
            _context = context;
        }

        public bool Create(Comment comment)
        {
            Post post = _context.Posts.Find(comment.PostId);

            if (post == null)
            {
                return false;
            }

            comment.Created = DateTime.Now;
            post.Comments.Add(comment);
            return SaveChanges();
        }

        public Comment Delete(int id)
        {
            Comment comment = Get(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                SaveChanges();
            }

            return comment;
        }

        public Comment Get(int id)
        {
            return _context.Comments.Find(id);
        }

        public IQueryable<Comment> GetAll()
        {
            return _context.Comments.OrderByDescending(c => c.Created);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(Comment item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return SaveChanges();
        }
    }
}