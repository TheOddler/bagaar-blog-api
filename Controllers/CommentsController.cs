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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GET api/comments
        // GET api/comments?postId=optional
        [HttpGet]
        public ActionResult<IEnumerable<CommentViewModel>> GetAll([FromQuery] int? postId)
        {
            var comments = _commentsService.GetAll(postId)
                .Select(comment => new CommentViewModel(comment))
                .ToList();
            if (comments.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return comments;
            }
        }

        // GET api/comments/5
        [HttpGet("{id}")]
        public ActionResult<CommentViewModel> Get(int id)
        {
            Comment comment = _commentsService.Get(id);

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
                bool success = _commentsService.Create(comment);

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
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    message = $"{e.Message}: {e.InnerException?.Message}"
                });
            }
        }
    }
}
