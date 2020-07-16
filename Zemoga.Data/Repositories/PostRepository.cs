using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;
using Zemoga.Data.Context;


namespace Zemoga.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        public BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            this._context = context;
        }

        public void ChangePostStatus(Post post, PostStatus newStatus)
        {
            PostStatus prevStatus = post.Status;
            try
            {
                post.Status = newStatus;
                _context.Posts.Update(post);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                post.Status = prevStatus;
                throw ex;
            }
        }

        public void CreatePost(Post post)
        {
            post.CreationDate = DateTime.Now;
            post.Status = PostStatus.Created;
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            Post post = _context.Posts.Find(postId);
            _context.Posts.Remove(post);
        }

        public IEnumerable<Post> GetApprovedPosts()
        {
            var posts = from post in _context.Posts
                        where post.Status == PostStatus.Approved
                        select post;
            return posts;
        }

        public IEnumerable<Post> GetPendingPosts()
        {
            var posts = from post in _context.Posts
                        where post.Status == PostStatus.Created
                        select post;
            return posts;
        }

        public Post GetPostbyId(int postId)
        {
            //var post = _context.Posts.Include
            var post = _context.Posts
                .Include(p => p.Comments)
                .Where(p => p.PostId == postId)
                .FirstOrDefault();
            return post;
        }

        public IEnumerable<Post> GetRejectedPostsbyAuthor(string authorEmail)
        {
            var posts = from post in _context.Posts
                        where post.Status == PostStatus.Rejected && post.AuthorEmail == authorEmail
                        select post;
            return posts;
        }

        public void UpdatePost(Post post)
        {
            try
            {
                _context.Posts.Update(post);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
