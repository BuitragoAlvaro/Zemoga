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
    public class CommentsController : Controller
    {
        private ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: Comments
        public IActionResult Index(int postId)
        {
            IEnumerable<Comment> comments = _commentService.GetCommentsbyPostId(postId);
            return View(comments);
        }

        // GET: Comments/Create
        public IActionResult Create(int id)
        {
            Post post = _commentService.GetPostbyId(id);
            Comment comment = new Comment();
            comment.Post = post;
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind(" Email, CommentText")] Comment comment)
        {
            Post post = _commentService.GetPostbyId(id);
            comment.Post = post;
            comment.CreationDate = DateTime.Now;
            _commentService.CreateComment(comment);
            return View(comment);
        }

    }
}
