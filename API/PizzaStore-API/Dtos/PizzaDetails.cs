using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Dtos
{
    public class PizzaDetails
    {
        public int pizzaTypeId { get; set; }

        public int SizeId { get; set; }

        public List<string> Toppings { get; set; }

        public int EdgeTypeId { get; set; }

        public int NumberOfPizza { get; set; }
    }
}
