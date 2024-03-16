using BloggingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloggingPlatform.Controllers
{
    public class PostController : Controller
    {
        private static List<Post> _posts = new List<Post>();

        public IRazorViewEngine ViewEngine { get; set; }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Id = _posts.Count + 1; // Assign a simple incremental ID
                _posts.Add(post);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public IActionResult Index()
        {
            return View(_posts);
        }

        public IActionResult Details(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(int id, Post updatedPost)
        {
            var existingPost = _posts.FirstOrDefault(p => p.Id == id);
            if (existingPost == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingPost.Title = updatedPost.Title;
                existingPost.Content = updatedPost.Content;
                existingPost.Author = updatedPost.Author;
                existingPost.PublishDate = updatedPost.PublishDate;
                return RedirectToAction("Index");
            }
            return View(updatedPost);
        }

        public IActionResult Delete(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _posts.Remove(post);
            return RedirectToAction("Index");
        }
    }
}