using OOPPizzaOrder.Api.Models;
using OOPPizzaOrder.Api.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOPPizzaOrder.Api.Services.Concrete
{
    public class CartService : ICartService
    {
        public void AddTocart(Cart cart, AddPizzaToCart pizza)
        {
            CartItems cartItem = cart.CartItems.FirstOrDefault(cl => cl.Pizza.Id == pizza.Id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                return;
            }

            cart.CartItems.Add(new CartItems { Pizza = pizza , Quantity=pizza.NumberOfPizza});
        }

        public List<CartItems> GetCartItems(Cart cart)
        {
            return cart.CartItems;
        }

        public void RemoveFromCart(Cart cart,int pizzaId)
        {
            cart.CartItems.Remove(cart.CartItems.FirstOrDefault(cl => cl.Pizza.Id == pizzaId));
        }
    }
}
