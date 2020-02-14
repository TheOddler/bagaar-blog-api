using System.Collections.Generic;
using System.Linq;
using BagaarBlogApi.Models;
using BagaarBlogApi.Repositories;

namespace BagaarBlogApi.Services
{
    public interface IPostsService
    {
        IEnumerable<Post> GetAll(string partialTitle);
        Post Get(int id);
        bool Create(Post post);
        bool Update(Post post);
        Post Delete(int id);
    }
}