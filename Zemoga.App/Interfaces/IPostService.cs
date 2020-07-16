using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.App.ViewModels;
using Zemoga.Domain.Models;

namespace Zemoga.App.Interfaces
{
    public interface IPostService
    {
        void AddPost(Post post);

        void ApprovePost(Post post);

        IEnumerable<Post> GetApprovedPosts();

        IEnumerable<Post> GetPendingPosts();

        Post GetPostbyId(int postId);

        IEnumerable<Post> GetRejectedPostsbyAuthor(string authorEmail);

        void RejectPost(Post post);

        void UpdatePost(Post post);
        void DeletePost(int value);
    }
}
