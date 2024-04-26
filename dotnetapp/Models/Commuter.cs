using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RideShare.Models

{
    public class Commuter
{
    public int CommuterID { get; set; }
        [Required]
    public string Name { get; set; }
        [Required]
    public string Email { get; set; }
        [Required]
    public string Phone { get; set; }
    public int RideID { get; set; }
    public Ride? Ride { get; set; }
}
}