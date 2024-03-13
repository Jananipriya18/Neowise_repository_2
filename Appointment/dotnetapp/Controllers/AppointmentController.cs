// AppointmentController.cs
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointmentList = _context.Appointments.ToList();
            return View(appointmentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                // Save the appointment to the database
                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                // Redirect to the appointment list or another action
                return RedirectToAction("Index");
            }

            // If the model state is not valid, return to the create view with validation errors
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // [HttpPost, ActionName("DeleteConfirmed")]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     var appointment = _context.Appointments.Find(id);

        //     if (appointment == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Appointments.Remove(appointment);
        //     _context.SaveChanges();

        //     return RedirectToAction("Index");
        // }
        [HttpPost, ActionName("DeleteConfirmed")]
public IActionResult DeleteConfirmed(int AppointmentID)
{
    // Add a breakpoint here to inspect the value of 'id'
        Console.WriteLine("the id is" ,AppointmentID);

    var appointment = _context.Appointments.Find(AppointmentID);


    if (appointment == null)
    {
        return NotFound();
    }

    _context.Appointments.Remove(appointment);
    _context.SaveChanges();

    return RedirectToAction("Index");
}


        // public IActionResult Delete(int id)
        // {
        //     Console.WriteLine("id"+id);
        //     var appointment = _context.Appointments.Find(id);
            

        //     if (appointment == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(appointment);
        // }

        // [HttpPost, ActionName("DeleteConfirmed")]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     var appointment = _context.Appointments.Find(id);

        //     if (appointment != null)
        //     {
        //         _context.Appointments.Remove(appointment);
        //         _context.SaveChanges();
        //     }

        //     return RedirectToAction("Index");
        // }
        // [HttpPost, ActionName("DeleteConfirmed")]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     var appointment = _context.Appointments.Find(id);

        //     if (appointment != null)
        //     {
        //         _context.Appointments.RemoveRange(appointment);
        //         _context.SaveChanges();
        //         return RedirectToAction("Index");
        //     }

        //     return NotFound();
        // }


        
    }
}
