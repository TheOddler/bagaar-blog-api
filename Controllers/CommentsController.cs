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
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentsRepo;

        public CommentsController(IRepository<Comment> commentsRepo)
        {
            _commentsRepo = commentsRepo;
        }

        // GET api/comments
        // GET api/comments?postId=optional
        [HttpGet]
        public ActionResult<IEnumerable<CommentViewModel>> Get([FromQuery] int? postId)
        {
            IQueryable<Comment> comments = _commentsRepo.GetAll();

            if (postId != null)
            {
                comments = comments.Where(comment => comment.PostId == postId);
            }

            return comments
                .Select(comment => new CommentViewModel(comment))
                .ToList();
        }

        // GET api/comments/5
        [HttpGet("{id}")]
        public ActionResult<CommentViewModel> Get(int id)
        {
            Comment comment = _commentsRepo.Get(id);

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
                bool success = _commentsRepo.Create(comment);

                if (success)
                {
                    return CreatedAtAction("Get", new Comment { Id = comment.Id }, new CommentViewModel(comment));
                }
                else
                {
                    return NotFound();
                }
            }
            // TODO: Better exception handling, for instance when there is an id conflict return a `Conflict`
            catch (Exception e)
            {
                return BadRequest($"{e.Message}: {e.InnerException?.Message}");
            }
        }
    }
}
