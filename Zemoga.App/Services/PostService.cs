using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.App.Interfaces;
using Zemoga.App.ViewModels;
using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;

namespace Zemoga.App.Services
{
    public class PostService : IPostService
    {
        public IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }

        public Post GetPostbyId(int postId)
        {
            return this._postRepository.GetPostbyId(postId);
        }

        public void AddPost(Post post)
        {
            _postRepository.CreatePost(post);
        }

        public void UpdatePost(Post post)
        {
            _postRepository.UpdatePost(post);
        }

        public void ApprovePost(Post post)
        {
            _postRepository.ChangePostStatus(post, PostStatus.Approved);
        }

        public IEnumerable<Post> GetApprovedPosts()
        {
            return _postRepository.GetApprovedPosts();
        }

        public IEnumerable<Post> GetPendingPosts()
        {
            return _postRepository.GetPendingPosts();
        }

        public void RejectPost(Post post)
        {
            _postRepository.ChangePostStatus(post, PostStatus.Rejected);
        }

        public IEnumerable<Post> GetRejectedPostsbyAuthor(string authorEmail)
        {
            return  _postRepository.GetRejectedPostsbyAuthor(authorEmail);
        }

        public void DeletePost(int postId)
        {
            _postRepository.DeletePost(postId);
        }
    }
}