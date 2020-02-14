
using System.Collections.Generic;
using System.Linq;
using BagaarBlogApi.Models;
using BagaarBlogApi.Repositories;

namespace BagaarBlogApi.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> _commentsRepo;

        public CommentsService(IRepository<Comment> commentsRepo)
        {
            _commentsRepo = commentsRepo;
        }

        public IEnumerable<Comment> GetAll(int? postId)
        {
            var comments = _commentsRepo.GetAll();

            if (postId != null)
            {
                comments = comments.Where(comment => comment.PostId == postId);
            }

            return comments;
        }

        public Comment Get(int id)
        {
            return _commentsRepo.Get(id);
        }

        public bool Create(Comment comment)
        {
            return _commentsRepo.Create(comment);
        }
    }

}