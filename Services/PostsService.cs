using System.Collections.Generic;
using System.Linq;
using BagaarBlogApi.Models;
using BagaarBlogApi.Repositories;

namespace BagaarBlogApi.Services
{
    public class PostsService : IPostsService
    {
        private readonly IRepository<Post> _postsRepo;
        private readonly IRepository<Comment> _commentsRepo;

        public PostsService(IRepository<Post> postsRepo, IRepository<Comment> commentsRepo)
        {
            _postsRepo = postsRepo;
            _commentsRepo = commentsRepo;
        }

        public IEnumerable<Post> GetAll(string partialTitle)
        {
            IQueryable<Post> posts = _postsRepo.GetAll();

            if (partialTitle != null)
            {
                posts = posts.Where(post => post.Title.Contains(partialTitle));
            }

            return posts;
        }

        public Post Get(int id)
        {
            return _postsRepo.Get(id);
        }

        public bool Create(Post post)
        {
            return _postsRepo.Create(post);
        }

        public bool Update(Post post)
        {
            return _postsRepo.Update(post);
        }

        public Post Delete(int id)
        {
            return _postsRepo.Delete(id);
        }
    }

}