using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public abstract class TimestampedRepository<T> : Repository<T>
        where T : class, ITimestamped
    {
        public TimestampedRepository(BlogContext context) : base(context)
        {
        }

        public override bool Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.Entry(item).Property(i => i.Created).IsModified = false;
            return SaveChanges();
        }
    }
}