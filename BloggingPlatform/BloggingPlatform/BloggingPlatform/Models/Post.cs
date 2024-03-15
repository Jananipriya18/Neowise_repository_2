using System;

namespace BloggingPlatform.Models
{
    public class Post
    {
        public int Id { get; set; } // Add an Id property for database mapping
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
