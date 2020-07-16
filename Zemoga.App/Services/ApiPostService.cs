using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Zemoga.App.Interfaces;
using Zemoga.App.ViewModels;
using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;

namespace Zemoga.App.Services
{
    public class ApiPostService : IApiPostService
    {
        public IPostRepository _postRepository;

        public ApiPostService(IPostRepository postRepository)
        {
            this._postRepository = postRepository;
        }       

        public string ApprovePostbyId(ApiUpdatePostModel postUpdate)
        {
            string result = string.Empty;
            PostStatus newStatus = (PostStatus)postUpdate.PostStatus;
            Post post = _postRepository.GetPostbyId(postUpdate.PostId);
            if (post != null)
            {
                _postRepository.ChangePostStatus(post, newStatus);
                result = Constants.ResultOk;
            }
            else
            {
                result = Constants.InvalidPostMessage;
            }
            return result;
        }

        public IEnumerable<ApiQueryModel> GetPendingPost()
        {
            var Posts = _postRepository.GetPendingPosts();
            var result = Posts.Select(post => new ApiQueryModel()
            {
                PostId = post.PostId,
                AuthorName = post.AuthorName,
                SubmitDate = post.CreationDate
            });
            return result;
        }
    }
}
