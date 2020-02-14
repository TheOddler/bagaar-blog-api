using System;
using BagaarBlogApi.Models;

namespace BagaarBlogApi.ViewModels
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }
        public int? PostId { get; set; }
    }
}