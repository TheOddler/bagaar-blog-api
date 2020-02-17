using System;
using System.ComponentModel.DataAnnotations;

namespace BagaarBlogApi.Models
{
    public class Comment : ITimestamped
    {
        // Entity framework knows Id and will make this the primary key
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}