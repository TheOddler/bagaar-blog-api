using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BagaarBlogApi.Models
{
    public class Post : ITimestamped
    {
        // Entity framework knows Id and will make this the primary key
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        public List<Comment> Comments { get; } = new List<Comment>();
    }
}