using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models
{
    public class OrderHead
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey(nameof(OrderHead.AppUserId)), ValidateNever]
        public AppUser AppUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }


        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }


        public DateTime PaymenteDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }


        public string? PaymentIntentId { get; set; }

        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Name { get; set; }

    }
}
