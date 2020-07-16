using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.App.ViewModels;
using Zemoga.Domain.Models;

namespace Zemoga.App.Interfaces
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetCommentsbyPostId(int postId);

        void CreateComment(Comment comment);

        Post GetPostbyId(int postId);
    }
}
