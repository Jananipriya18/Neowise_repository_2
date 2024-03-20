// AppointmentController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class AppointmentController : Controller
    {
        private static List<Appointment> _appointments;

        public AppointmentController()
        {
            _appointments = new List<Appointment>
            {
                new Appointment { 
                    AppointmentID = 1, 
                    PatientFirstName = "John", 
                    PatientLastName = "Doe", 
                    PatientPhoneNumber = "1234567890", 
                    PatientEmail = "john.doe@example.com", 
                    DoctorFirstName = "Dr. Jane", 
                    DoctorLastName = "Smith", 
                    DoctorSpecialty = "Cardiology", 
                    AppointmentDate = DateTime.Now.AddDays(1), 
                    Reason = "Regular checkup" 
                },
                new Appointment { 
                    AppointmentID = 2, 
                    PatientFirstName = "Alice", 
                    PatientLastName = "Johnson", 
                    PatientPhoneNumber = "9876543210", 
                    PatientEmail = "alice.johnson@example.com", 
                    DoctorFirstName = "Dr. Michael", 
                    DoctorLastName = "Brown", 
                    DoctorSpecialty = "Dermatology", 
                    AppointmentDate = DateTime.Now.AddDays(2), 
                    Reason = "Skin condition" 
                }
                
            };
            Console.WriteLine("jan");
        }

        public IActionResult Index()
        {
            return View(_appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
            // Console.WriteLine("sree");
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            Console.WriteLine(appointment);
            if (ModelState.IsValid)
            {
                appointment.AppointmentID = _appointments.Count + 1; // Assign a simple incremental ID
                _appointments.Add(appointment);
                return RedirectToAction("Index");
            }
            Console.WriteLine("sree");
            return View(appointment);
        }

        public IActionResult Edit(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.AppointmentID == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment updatedAppointment)
        {
            var existingAppointment = _appointments.FirstOrDefault(a => a.AppointmentID == updatedAppointment.AppointmentID);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                existingAppointment.PatientFirstName = updatedAppointment.PatientFirstName;
                existingAppointment.PatientLastName = updatedAppointment.PatientLastName;
                existingAppointment.PatientPhoneNumber = updatedAppointment.PatientPhoneNumber;
                existingAppointment.PatientEmail = updatedAppointment.PatientEmail;
                existingAppointment.DoctorFirstName = updatedAppointment.DoctorFirstName;
                existingAppointment.DoctorLastName = updatedAppointment.DoctorLastName;
                existingAppointment.DoctorSpecialty = updatedAppointment.DoctorSpecialty;
                existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;
                existingAppointment.Reason = updatedAppointment.Reason;
                
                return RedirectToAction("Index");
            }
            return View(updatedAppointment);
        }

        public IActionResult Delete(int id)
        {
            var appointment = _appointments.FirstOrDefault(a => a.AppointmentID == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int appointmentID)
        {
            var appointment = _appointments.FirstOrDefault(a => a.AppointmentID == appointmentID);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointments.Remove(appointment);
            return RedirectToAction("Index");
        }

    }
}
