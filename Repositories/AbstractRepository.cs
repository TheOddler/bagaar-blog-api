using System;
using System.Linq;
using BagaarBlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T>
        where T : class
    {
        protected abstract DbSet<T> DbSet { get; }
        protected readonly BlogContext _context;

        public AbstractRepository(BlogContext context)
        {
            _context = context;
        }

        virtual public bool Create(T item)
        {
            DbSet.Add(item);
            return SaveChanges();
        }

        virtual public T Delete(int id)
        {
            T item = Get(id);

            if (item != null)
            {
                DbSet.Remove(item);
                SaveChanges();
            }

            return item;
        }

        virtual public T Get(int id)
        {
            return DbSet.Find(id);
        }

        virtual public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        virtual public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        virtual public bool Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return SaveChanges();
        }
    }
}