using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.Domain.Models;

namespace Zemoga.Domain.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsbyPost(int PostId);

        void CreateComment(Comment comment);

        Post GetPostbyId(int postId);
    }
}
