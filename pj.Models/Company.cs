using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Stress Address")]
        public string? StressAddress { get; set; }

        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
