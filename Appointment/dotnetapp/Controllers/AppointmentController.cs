using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly BookDbContext _context;

        public AppointmentController(BookDbContext context)
        {
            _context = context;
        }

        
    }
}
