using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.Domain.Models;


namespace Zemoga.Domain.Interfaces
{
    public interface IPostRepository
    {

        Post GetPostbyId(int postId);

        void CreatePost(Post post);

        void UpdatePost(Post post);

        void ChangePostStatus(Post post, PostStatus newStatus);

        IEnumerable<Post> GetApprovedPosts();

        IEnumerable<Post> GetPendingPosts();

        IEnumerable<Post> GetRejectedPostsbyAuthor(string authorEmail);
        void DeletePost(int postId);
    }
}
