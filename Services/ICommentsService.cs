using System.Collections.Generic;
using BagaarBlogApi.Models;

namespace BagaarBlogApi.Services
{
    public interface ICommentsService
    {
        IEnumerable<Comment> GetAll(int? postId = null);
        Comment Get(int id);
        bool Create(Comment comment);
    }
}