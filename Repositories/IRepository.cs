using System.Linq;

namespace BagaarBlogApi.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        bool Create(T item);
        bool Update(T item);
        T Delete(int id);
        T Get(int id);
        bool SaveChanges();
    }

}