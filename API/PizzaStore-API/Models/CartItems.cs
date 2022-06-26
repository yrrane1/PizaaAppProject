using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Models
{
    public class CartItems
    {
        public AddPizzaToCart Pizza { get; set; }

        public int Quantity{ get; set; }
    }
}
