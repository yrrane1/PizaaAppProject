using OOPPizzaOrder.Api.Models;
using OOPPizzaOrder.Api.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Repository.Concrete
{
    public class PizzaTypeRepository : IPizzaTypeRepository
    {
        public List<PizzaType> GetAllPizzas()
        {
            PizzaType cheese = new PizzaType
            {
                Id=1,
                Price = 300,
                Name = "Cheese",
                Category = "Veg"
            };

            PizzaType periperi = new PizzaType
            {
                Id = 2,
                Price = 250,
                Name="periperi",
                Category = "Veg"
            };


            PizzaType cheesyChicken = new PizzaType
            {
                Id = 3,
                Name = "Cheesy Chicken",
                Price = 400,
                Category = "NonVeg"
            };


            PizzaType paneer = new PizzaType
            {
                Id = 4,
                Name = "Paneer",
                Price = 200,
                Category = "Veg"
            };

            PizzaType italian = new PizzaType
            {
                Id=5,
                Name = "Italian Pizza",
                Price = 550,
                Category = "Veg"

            };

            PizzaType vegHandMade = new PizzaType
            {
                Id = 6,
                Name = "Try Hand made Veg Pizza",
                Price = 300,
                Category = "Veg"

            };
            PizzaType nonVegHandMade = new PizzaType
            {
                Id = 7,
                Name = "Try Hand made Non Veg Pizza",
                Price = 550,
                Category = "NonVeg"

            };

            List<PizzaType> pizzaTypes = new List<PizzaType>
            {
                cheese,
                periperi,
                cheesyChicken,
                paneer,
                italian,
                vegHandMade,
                nonVegHandMade
            };
            
            return pizzaTypes;
        }

        public string GetPizzaTypeName(int pizzaTypeId)
        {
            string name = GetAllPizzas()
                 .Where(pt => pt.Id == pizzaTypeId)
                 .Select(pt => pt.Name)
                 .SingleOrDefault();

            return name;
        }

        public decimal GetPizzaTypePrice(int pizzaTypeId)
        {
            decimal price = GetAllPizzas()
                 .Where(pt => pt.Id == pizzaTypeId)
                 .Select(pt => pt.Price)
                 .SingleOrDefault();

            return price;
        }
    }
}
