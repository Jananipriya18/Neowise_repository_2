using System;
namespace dotnetapp.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }

        // Patient details
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmail { get; set; }

        // Doctor details
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorSpecialty { get; set; }

        // Appointment details
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
    }
}