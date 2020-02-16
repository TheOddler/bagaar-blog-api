using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BagaarBlogApi.Models;
using BagaarBlogApi.Services;
using BagaarBlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BagaarBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ICommentsService _commentsService;
        private readonly CommentsController _commentsController;

        public PostsController(IPostsService postsService, ICommentsService commentsService, CommentsController commentsController)
        {
            _postsService = postsService;
            _commentsService = commentsService;
            _commentsController = commentsController;
        }

        // GET api/posts
        // GET api/posts?title=optional
        [HttpGet]
        public ActionResult<IEnumerable<PostViewModel>> GetAll([FromQuery] string title)
        {
            var posts = _postsService
                .GetAll(title)
                .Select(p => new PostViewModel(p))
                .ToList();
            if (posts.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return posts;
            }
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public ActionResult<PostViewModel> Get(int id)
        {
            Post post = _postsService.Get(id);

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
                _postsService.Create(post);
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
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Inconsistent ids"
                });
            }

            try
            {
                _postsService.Update(post);
                return NoContent();
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = $"{e.Message}: {e.InnerException?.Message}"
                });
            }
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public ActionResult<PostViewModel> Delete(int id)
        {
            Post deleted = _postsService.Delete(id);

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
            return _commentsController.GetAll(postId);
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
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Inconsistent ids"
                });
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
