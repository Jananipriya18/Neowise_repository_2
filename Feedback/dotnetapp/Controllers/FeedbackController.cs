// // FeedbackController.cs
// using System;
// using System.Linq;
// using Microsoft.AspNetCore.Mvc;
// using dotnetapp.Models;

// namespace dotnetapp.Controllers
// {
//     public class FeedbackController : Controller
//     {
//         private readonly ApplicationDbContext _context;

//         public FeedbackController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public IActionResult Index()
//         {
//             var feedbackList = _context.Feedbacks.ToList();
//             return View(feedbackList);
//         }

//         [HttpGet]
//         public IActionResult Create()
//         {
//             return View();
//         }

//         [HttpPost]
//         public IActionResult Create(Feedback feedback)
//         {
//             if (ModelState.IsValid)
//             {
//                 // Save the feedback to the database
//                 _context.Feedbacks.Add(feedback);
//                 _context.SaveChanges();

//                 // Redirect to the feedback list or another action
//                 return RedirectToAction("Index");
//             }

//             // If the model state is not valid, return to the create view with validation errors
//             return View(feedback);
//         }

//         public IActionResult Edit(int id)
//         {
//             var feedback = _context.Feedbacks.Find(id);

//             if (feedback == null)
//             {
//                 return NotFound();
//             }

//             return View(feedback);
//         }

//         [HttpPost]
//         public IActionResult Edit(Feedback feedback)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Feedbacks.Update(feedback);
//                 _context.SaveChanges();

//                 return RedirectToAction("Index");
//             }

//             return View(feedback);
//         }

//         public IActionResult Delete(int id)
//         {
//             Console.WriteLine("id"+id);
//             var feedback = _context.Feedbacks.Find(id);
            

//             if (feedback == null)
//             {
//                 return NotFound();
//             }

//             return View(feedback);
//         }

//         [HttpPost, ActionName("Delete")]
//         public IActionResult DeleteConfirmed(int id)
//         {
//             var feedback = _context.Feedbacks.Find(id);

//             if (feedback != null)
//             {
//                 _context.Feedbacks.Remove(feedback);
//                 _context.SaveChanges();
//             }

//             return RedirectToAction("Index");
//         }
//     }
// }

// FeedbackController.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedbackController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var feedbackList = _context.Feedbacks.ToList();
            return View(feedbackList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                // Save the feedback to the database
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();

                // Redirect to the feedback list or another action
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the create view with validation errors
            return View(feedback);
        }

        public IActionResult Edit(int id)
        {
            var feedback = _context.Feedbacks.Find(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost]
        public IActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                _context.Feedbacks.Update(feedback);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(feedback);
        }

        public IActionResult Delete(int id)
        {
            Console.WriteLine("id"+id);
            var feedback = _context.Feedbacks.Find(id);
            

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var feedback = _context.Feedbacks.Find(id);

            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
