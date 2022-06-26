using OOPPizzaOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Services.Abstract
{
   public interface ICartService
    {
        void AddTocart(Cart cart, AddPizzaToCart pizza);

        void RemoveFromCart(Cart cart,int pizzaId);

        List<CartItems> GetCartItems(Cart cart);
    }
}
