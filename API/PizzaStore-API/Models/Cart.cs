using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItems>();
        }
        public List<CartItems> CartItems { get; set; }

        public decimal TotalPrice { get; set; }

        public int TotalQuantity { get; set; }


    }
}
