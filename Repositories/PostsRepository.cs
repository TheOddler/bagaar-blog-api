using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public class PostsRepository : IRepository<Post>
    {
        private readonly BlogContext _context;

        public PostsRepository(BlogContext context)
        {
            _context = context;
        }

        public bool Create(Post item)
        {
            item.Created = DateTime.Now;
            _context.Posts.Add(item);
            return SaveChanges();
        }

        public Post Delete(int id)
        {
            Post post = Get(id);

            if (post != null)
            {
                _context.Posts.Remove(post);
                SaveChanges();
            }

            return post;
        }

        public Post Get(int id)
        {
            return _context.Posts.Find(id);
        }

        public IQueryable<Post> GetAll()
        {
            return _context.Posts.OrderByDescending(p => p.Created);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(Post item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return SaveChanges();
        }
    }
}