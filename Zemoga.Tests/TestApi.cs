using System;

using System.Collections;

using Zemoga.App.Interfaces;
using Zemoga.App.Services;
using Zemoga.App.ViewModels;
using System.Collections.Generic;

using Moq;
using Zemoga.Domain.Interfaces;
using Zemoga.Domain.Models;

using NUnit.Framework;

namespace Zemoga.Tests
{
    [TestFixture]
    public class TestApi
    {
        private Mock<IPostRepository> mockPostRepository = new Mock<IPostRepository>();
        ApiPostService postService;
        [SetUp]
        public void Setup()
        {
            mockPostRepository.Setup(it => it.GetApprovedPosts()).Returns(new List<Post>() { new Post() { PostId = 1, Status= PostStatus.Approved, PostTitle="Post 1"},
                    new Post(){ PostId = 2, Status= PostStatus.Approved, PostTitle="Post 2" } });
            mockPostRepository.Setup(it => it.GetPendingPosts()).Returns(new List<Post>() { new Post() { PostId = 3, Status= PostStatus.Created, PostTitle="Post 3"},
                    new Post(){ PostId = 4, Status= PostStatus.Created, PostTitle="Post 4" } });
            mockPostRepository.Setup(it => it.GetPostbyId(3)).Returns(new Post() { PostId = 3, PostTitle = "post 3"});
            mockPostRepository.Setup(it => it.GetPostbyId(5)).Returns((Post)null);
            postService = new ApiPostService(mockPostRepository.Object);
        }

            [Test]
        public void TypeList()
        {           
            var test1 = postService.GetPendingPost();
            CollectionAssert.AllItemsAreInstancesOfType(test1, new ApiQueryModel().GetType());           
        }

        [Test]
        public void NumElements()
        {
            int numElements;
            var test1 = postService.GetPendingPost();
            var enumTest = test1.GetEnumerator();
            numElements = 0;          
            while (enumTest.MoveNext())
            {
                numElements++;
            }
            Assert.AreEqual(numElements, 2);
        }

        [Test]
        public void testApproveOk()
        {
            ApiUpdatePostModel updatePost = new ApiUpdatePostModel() {  PostId = 3, PostStatus=2 };         
            var test = postService.ApprovePostbyId(updatePost);
            Assert.AreEqual(Constants.ResultOk, test);
        }

        [Test]
        public void testApproveNotOk()
        {
            ApiUpdatePostModel updatePost = new ApiUpdatePostModel() { PostId = 5, PostStatus = 2 };

            var test = postService.ApprovePostbyId(updatePost);

            Assert.AreEqual(Constants.InvalidPostMessage, test);
        }
    }
}
