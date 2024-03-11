using System;
namespace dotnetapp.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
