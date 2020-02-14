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
    public class CommentsController : ControllerBase
    {
        private readonly BlogContext _context;

        public CommentsController(BlogContext context)
        {
            _context = context;
        }

        // GET api/comments
        // GET api/comments?postId=optional
        [HttpGet]
        public ActionResult<IEnumerable<CommentViewModel>> Get([FromQuery] int? postId)
        {
            IQueryable<Comment> comments = _context.Comments;

            if (postId != null)
            {
                comments = comments.Where(comment => comment.PostId == postId);
            }

            return comments
                .OrderByDescending(comment => comment.Created)
                .Select(comment => new CommentViewModel(comment))
                .ToList();
        }

        // GET api/comments/5
        [HttpGet("{id}")]
        public ActionResult<CommentViewModel> Get(int id)
        {
            Comment comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }
            else
            {
                return new CommentViewModel(comment);
            }
        }

        // POST api/comments/
        [HttpPost]
        public ActionResult<CommentViewModel> Post([FromBody] Comment comment)
        {
            try
            {
                Post post = _context.Posts.Find(comment.PostId);

                if (post == null)
                {
                    return NotFound();
                }

                comment.Created = DateTime.Now;
                post.Comments.Add(comment);
                _context.SaveChanges();

                return CreatedAtAction("Get", new Comment { Id = comment.Id }, new CommentViewModel(comment));
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest($"{e.Message}: {e.InnerException?.Message}");
            }
        }
    }
}
