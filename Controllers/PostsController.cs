using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagaarBlogApi.Models;
using BagaarBlogApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BagaarBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _context;
        private readonly CommentsController _commentsController;

        public PostsController(BlogContext context, CommentsController commentsController)
        {
            _context = context;
            _commentsController = commentsController;
        }

        // GET api/posts
        // GET api/posts?title=optional
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get([FromQuery] string title)
        {
            IQueryable<Post> posts = _context.Posts;

            if (title != null)
            {
                posts = posts.Where(post => post.Title.Contains(title));
            }

            return posts.OrderByDescending(post => post.Created).ToList();
        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            Post post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }
            else
            {
                return post;
            }
        }

        // POST api/posts
        [HttpPost]
        public ActionResult<Post> Post([FromBody] Post post)
        {
            try
            {
                post.Created = DateTime.Now;

                _context.Posts.Add(post);
                _context.SaveChanges();

                return CreatedAtAction("Get", new Post { Id = post.Id }, post);
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest($"{e.Message}: {e.InnerException?.Message}");
            }
        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public ActionResult<Post> Put(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest("Inconsistent ids");
            }

            try
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();

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
        public ActionResult<Post> Delete(int id)
        {
            Post post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }
            else
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return post;
            }
        }

        // POST api/posts/5/comments
        [HttpPost("{postId}/comments")]
        public ActionResult<CommentViewModel> PostComment(int postId, [FromBody] Comment comment)
        {
            if (postId != comment.PostId)
            {
                return BadRequest("Inconsistent ids");
            }

            return _commentsController.Post(comment);
        }
    }
}
