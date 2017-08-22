using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace iglid.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string PSN { get; set; }
        public int score { get; set; }
        public string Tname { get; set; }
        public List<Massage> massages { get; set; }
    }
}
