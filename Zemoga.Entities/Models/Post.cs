using System;
using System.Collections.Generic;
using System.Text;

namespace Zemoga.Domain.Models
{
    public enum PostStatus
    {
        Created,
        Approved,
        Rejected
    }
    public class Post
    {
        public int PostId { get; set; }

        public string AuthorName { get; set; }

        public string AuthorEmail { get; set; }

        public PostStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public string PostTitle { get; set; }

        public string PostText { get; set; }

        public List<Comment> Comments { get; set; }

    }
}
