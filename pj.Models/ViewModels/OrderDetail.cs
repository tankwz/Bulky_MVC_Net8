using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pj.Models.ViewModels
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderHeadId { get; set; }
        [ForeignKey(nameof(OrderDetail.OrderHeadId)),ValidateNever]
        public OrderHead OrderHead { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(OrderDetail.ProductId)),ValidateNever]
        public Product Product { get; set; }
        public int Count { get; set; }

        public double Price { get; set; }



    }
}
