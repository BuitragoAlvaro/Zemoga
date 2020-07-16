using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.App.Interfaces;
using Zemoga.App.ViewModels;
using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;

namespace Zemoga.App.Services
{
    public class CommentService : ICommentService
    {
        public ICommentRepository _ICommentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this._ICommentRepository = commentRepository;
        }

        public void CreateComment(Comment comment)
        {
            this._ICommentRepository.CreateComment(comment);
        }

        public IEnumerable<Comment> GetCommentsbyPostId(int postId)
        {
            return  this._ICommentRepository.GetCommentsbyPost(postId);
        }

        public Post GetPostbyId(int postId)
        {
            return this._ICommentRepository.GetPostbyId(postId);
        }
    }
}
