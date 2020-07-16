using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


using Zemoga.App.Interfaces;
using Zemoga.App.ViewModels;
using Zemoga.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Zemoga.UI.Controllers
{
    public class PostsController : Controller
    {
        private IPostService _postService;
       
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: Posts
        public ActionResult Index()
        {
            IEnumerable<Post> model = _postService.GetApprovedPosts();
            return View(model);
        }

        [Authorize(Roles = "Editor")]
        public IActionResult IndexEditor()
        {
            IEnumerable<Post> model = _postService.GetPendingPosts();
            return View(model);
        }

        [Authorize(Roles = "Writer")]
        public IActionResult IndexWriter()
        {
            IEnumerable<Post> model = _postService.GetRejectedPostsbyAuthor(User.Identity.Name);
            return View(model);
        }

        // GET: Posts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postService.GetPostbyId(id.Value);               
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize(Roles = "Writer")]
        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Writer")]
        public IActionResult Create([Bind("PostTitle,PostText")] Post post)
        {
            if (!string.IsNullOrEmpty(post.PostText)&& !string.IsNullOrEmpty(post.PostTitle))
            {
                post.Status = PostStatus.Created;
                post.AuthorEmail = User.Identity.Name;
                post.AuthorName = User.Identity.Name;
                post.CreationDate = DateTime.Now;
                _postService.AddPost(post);                
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            
            var post = _postService.GetPostbyId(id.Value);
            if (post == null || post.AuthorName != User.Identity.Name)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("PostId,AuthorName,AuthorEmail,Status,CreationDate,PostTitle,PostText")] Post post)
        {
            if (id != post.PostId || post.AuthorName != User.Identity.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    post.Status = PostStatus.Created;
                    _postService.UpdatePost(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postService.GetPostbyId(id.Value);                
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
             _postService.DeletePost(id);
            
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Approve(int postId)
        {
            Post post = _postService.GetPostbyId(postId);
            if (post == null)
            {
                return NotFound();
            }
            _postService.ApprovePost(post);

            return RedirectToAction(nameof(IndexEditor));
        }

        [Authorize(Roles = "Editor")]
        public IActionResult Reject(int postId)
        {
            Post post = _postService.GetPostbyId(postId);
            if (post == null)
            {
                return NotFound();
            }
            _postService.RejectPost(post);

            return RedirectToAction(nameof(IndexEditor));
        }

        private bool PostExists(int id)
        {
            return _postService.GetPostbyId(id) != null;
        }
    }
}
