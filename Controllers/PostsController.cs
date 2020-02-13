using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BagaarBlogApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BagaarBlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        // POST api/posts
        [HttpPost]
        public void Post([FromBody] Post post)
        {

        }

        // GET api/posts/5
        [HttpGet("{id}")]
        public ActionResult<Post> Get(int id)
        {
            return new Post
            {
                Id = 1,
                Title = "This is a post",
                Content = "Hardcoded"
            };
        }

        // DELETE api/posts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }

        // PUT api/posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post post)
        {

        }

        // GET api/posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> Get()
        {
            // sorted by most recent
            return new Post[] {
                new Post
                {
                    Id = 1,
                    Title = "This is a post",
                    Content = "Hardcoded"
                },
                new Post
                {
                    Id = 2,
                    Title = "Second post",
                    Content = "Also hardcoded"
                },
            };
        }

        // Get posts based on title
    }
}
