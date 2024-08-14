using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FitnessHub.Models
{
    public class User : IdentityUser<int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
    {
        [Key]
        public int UserID { get; set; }
      
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin or User
        // Navigation properties
        public virtual ICollection<Booking> Bookings { get; set; }
    }

    public class UserDto
    {
        public int UserID { get; set; }
     
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        // Include bookings details if needed
        public ICollection<BookingDto> Bookings { get; set; }
    }
}