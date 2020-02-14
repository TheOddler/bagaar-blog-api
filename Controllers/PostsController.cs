using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagaarBlogApi.Models;
using BagaarBlogApi.Repositories;
using BagaarBlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postsRepo;
        private readonly IRepository<Comment> _commentsRepo;
        private readonly CommentsController _commentsController;

        public PostsController(IRepository<Post> postsRepo, IRepository<Comment> commentsRepo, CommentsController commentsController)
        {
            _postsRepo = postsRepo;
            _commentsRepo = commentsRepo;
            _commentsController = commentsController;
        }

        // GET api/posts
        // GET api/posts?title=optional
        [HttpGet]
        public ActionResult<IEnumerable<PostViewModel>> Get([FromQuery] string title)
        {
            IQueryable<Post> posts = _postsRepo.GetAll();

            if (title != null)
            {
                posts = posts.Where(post => post.Title.Contains(title));
            }

            return posts.Select(p => new PostViewModel(p)).ToList();
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public ActionResult<PostViewModel> Get(int id)
        {
            Post post = _postsRepo.Get(id);

            if (post == null)
            {
                return NotFound();
            }
            else
            {
                return new PostViewModel(post);
            }
        }

        // POST api/posts
        [HttpPost]
        public ActionResult<PostViewModel> Post([FromBody] Post post)
        {
            try
            {
                _postsRepo.Create(post);
                return CreatedAtAction("Get", new Post { Id = post.Id }, new PostViewModel(post));
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest($"{e.Message}: {e.InnerException?.Message}");
            }
        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public ActionResult<PostViewModel> Put(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest("Inconsistent ids");
            }

            try
            {
                _postsRepo.Update(post);
                return NoContent();
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest($"{e.Message}: {e.InnerException?.Message}");
            }
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public ActionResult<PostViewModel> Delete(int id)
        {
            Post deleted = _postsRepo.Delete(id);

            if (deleted != null)
            {
                return new PostViewModel(deleted);
            }
            else
            {
                return NotFound();
            }
        }

        // Extra api endpoints for comments of specific posts

        // GET api/posts/5/comments
        [HttpGet("{postId}/comments")]
        public ActionResult<IEnumerable<CommentViewModel>> GetComments(int postId)
        {
            return _commentsRepo
                .GetAll()
                .Where(comment => comment.PostId == postId)
                .Select(comment => new CommentViewModel(comment))
                .ToList();
        }

        // POST api/posts/5/comments
        [HttpPost("{postId}/comments")]
        public ActionResult<CommentViewModel> PostComment(int postId, [FromBody] CreateCommentViewModel createComment)
        {
            if (createComment.PostId == null)
            {
                createComment.PostId = postId;
            }
            else if (postId != createComment.PostId)
            {
                return BadRequest("Inconsistent ids");
            }

            Comment comment = new Comment
            {
                Content = createComment.Content,
                PostId = createComment.PostId.Value
            };
            return _commentsController.Post(comment);
        }
    }
}
