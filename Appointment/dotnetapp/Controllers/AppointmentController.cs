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

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var appointment = _context.Appointments.Find(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Test]
public void EditAppointment_UpdatesAppointmentInDatabase()
{
    // Arrange
    var originalAppointment = new Appointment
    {
        AppointmentID = 1,
        PatientFirstName = "OriginalPatientFirstName",
        PatientLastName = "OriginalPatientLastName",
        DoctorFirstName = "OriginalDoctorFirstName",
        DoctorLastName = "OriginalDoctorLastName",
        DoctorSpecialty = "OriginalSpecialty",
        PatientEmail = "original@email.com",
        PatientPhoneNumber = "1234567890",
        AppointmentDate = DateTime.Now,
        Reason = "OriginalReason"
    };

    _context.Add(originalAppointment);
    _context.SaveChanges();

    var updatedAppointment = new Appointment
    {
        AppointmentID = 1,
        PatientFirstName = "UpdatedPatientFirstName",
        PatientLastName = "UpdatedPatientLastName",
        DoctorFirstName = "UpdatedDoctorFirstName",
        DoctorLastName = "UpdatedDoctorLastName",
        DoctorSpecialty = "UpdatedSpecialty",
        PatientEmail = "updated@email.com",
        PatientPhoneNumber = "9876543210",
        AppointmentDate = DateTime.Now.AddDays(1),
        Reason = "UpdatedReason"
    };

    // Act
    var controller = new AppointmentController(_context);
    var editActionResult = controller.Edit(updatedAppointment) as RedirectToActionResult;

    // Assert
    Assert.IsNotNull(editActionResult);
    Assert.AreEqual("Index", editActionResult.ActionName);

    var editedAppointment = _context.Appointments.Find(1);
    Assert.IsNotNull(editedAppointment);
    Assert.AreEqual("UpdatedPatientFirstName", editedAppointment.PatientFirstName);
    Assert.AreEqual("UpdatedPatientLastName", editedAppointment.PatientLastName);
    Assert.AreEqual("UpdatedDoctorFirstName", editedAppointment.DoctorFirstName);
    Assert.AreEqual("UpdatedDoctorLastName", editedAppointment.DoctorLastName);
    Assert.AreEqual("UpdatedSpecialty", editedAppointment.DoctorSpecialty);
    Assert.AreEqual("updated@email.com", editedAppointment.PatientEmail);
    Assert.AreEqual("9876543210", editedAppointment.PatientPhoneNumber);
    Assert.AreEqual("UpdatedReason", editedAppointment.Reason);
}

    }
}
