using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Range(1, 100, ErrorMessage = "Please Enter Value between 1 and 100")]
        public int Count { get; set; }
    }
}
