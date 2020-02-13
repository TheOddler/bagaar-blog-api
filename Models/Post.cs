namespace BagaarBlogApi.Models
{
    public class Post
    {
        // Entity framework knows Id and will make this the primary key
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}