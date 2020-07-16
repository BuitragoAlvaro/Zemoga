using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;
using Zemoga.Data.Context;

namespace Zemoga.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {

        public BlogDbContext _context;

        public CommentRepository(BlogDbContext context)
        {
            this._context = context;
        }

        public void CreateComment(Comment comment)
        {
            comment.CreationDate = DateTime.Now;
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsbyPost(int PostId)
        {
            var comments = from comment in _context.Comments
                           where comment.Post.PostId == PostId
                           select comment;
            return comments;
        }

        public Post GetPostbyId(int postId)
        {
            var post = _context.Posts.Find(postId);
            return post;
        }
    }
}
