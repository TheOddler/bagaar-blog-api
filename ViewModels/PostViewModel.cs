using System;
using BagaarBlogApi.Models;

namespace BagaarBlogApi.ViewModels
{
    public class PostViewModel
    {
        protected Post _post;

        public int Id => _post.Id;
        public string Title => _post.Title;
        public string Content => _post.Content;
        public DateTime Created => _post.Created;

        public PostViewModel(Post post)
        {
            _post = post;
        }
    }
}