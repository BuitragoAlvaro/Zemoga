using System;
using System.Collections.Generic;
using System.Text;

namespace Zemoga.Domain.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public Post Post { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public string CommentText { get; set; }
    }
}
