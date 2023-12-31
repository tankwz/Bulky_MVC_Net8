﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            selected = false;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId"), ValidateNever]
        public Product Product { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [ValidateNever]
        public AppUser AppUser { get; set; }
        [Range(1,1000, ErrorMessage ="Quantity must be between 1-1000")]
        public int count { get; set; }
        [NotMapped] public double price { get; set; }
        [NotMapped] public double currentprice { get; set; }
        [NotMapped] public bool selected { get; set; }
    }
}
