using OOPPizzaOrder.Api.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Repository.Concrete
{
    public class ToppingRepository : IToppingRepository
    {
        public List<string> GetAllToppings()
        {
            List<string> toppings = new List<string>
            {
               "Olive Rings Raw",
               "Olive Rings Roasted",
               "Cripsy Onion",
               "Baccon",
               "Tomato Grills",
               "Sauce",
               "Cheese",
               "Peri Peri",
            };

            return toppings;
        }
    }
}
