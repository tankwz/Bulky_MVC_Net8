using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public int Name { get; set; }
        public string? StressAddress { get; set; }
        public string? City { get; set; }

        public string? State { get; set; }
        public string? PostalCode { get; set; }



    }
}
