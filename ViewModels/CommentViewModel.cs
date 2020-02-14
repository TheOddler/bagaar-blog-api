using System;
using BagaarBlogApi.Models;

namespace BagaarBlogApi.ViewModels
{
    public class CommentViewModel
    {
        private Comment _comment;

        public int Id => _comment.Id;
        public string Content => _comment.Content;
        public DateTime Created => _comment.Created;
        public int PostId => _comment.PostId;

        public CommentViewModel(Comment comment)
        {
            _comment = comment;
        }

        public static implicit operator CommentViewModel(Comment c) => new CommentViewModel(c);
    }
}