using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OOPPizzaOrder.Api.Dtos;
using OOPPizzaOrder.Api.Helpers;
using OOPPizzaOrder.Api.Models;
using OOPPizzaOrder.Api.Repository.Abstract;
using OOPPizzaOrder.Api.Services.Abstract;

namespace OOPPizzaOrder.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private  ICartSessionService _cartSessionService;
        private  ICartService _cartService;
        private  IPizzaPriceCalculater _pizzaPriceCalculater;
        private  IPizzaTypeRepository _pizzaTypeRepository;
        private ISizeRepository _sizeRepository;

        ObjectCache cache = MemoryCache.Default;
        const string CacheKey = "cart";

        public CartController(ICartSessionService cartSessionService,
            ICartService cartService,
            IPizzaPriceCalculater pizzaPriceCalculater,
            IPizzaTypeRepository pizzaTypeRepository,
            ISizeRepository sizeRepository
            )
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
           _pizzaPriceCalculater = pizzaPriceCalculater;
           _pizzaTypeRepository = pizzaTypeRepository;
            _sizeRepository = sizeRepository;
        }

        [HttpPost("addPizzaToCart/")]
        public ActionResult AddToCart( [FromBody] AppUpdatePizzaCart pizzaDto)
        {
            var cart = _cartSessionService.GetCart();

            decimal pizzaTypePrice = _pizzaTypeRepository.GetPizzaTypePrice(pizzaDto.pizzaTypeId);
            decimal sizeMultiplier = _sizeRepository.GetSizeMultiplier(pizzaDto.SizeId);
            decimal pricefromtheCart = 0;
            int quantityFromCart =  1;

            decimal price = _pizzaPriceCalculater.Calculate(sizeMultiplier, pizzaTypePrice, pizzaDto.EdgeTypeId, pizzaDto.NumberOfPizza, pizzaDto.Toppings == null ? 0 : pizzaDto.Toppings.Count);
            string pizzaName = _pizzaTypeRepository.GetPizzaTypeName(pizzaDto.pizzaTypeId);
            
            if (cache.Contains(CacheKey))
            {
                cart = (Cart)cache.Get(CacheKey);
                pricefromtheCart = cart.TotalPrice;
                quantityFromCart = cart.TotalQuantity;
            }
            AddPizzaToCart pizzaToAddCart = new AddPizzaToCart
            {
                Id = pizzaDto.pizzaTypeId,
                PizzaName = pizzaName,
                NumberOfPizza = pizzaDto.NumberOfPizza,
                Price = price,
                Toppings = pizzaDto.Toppings
            };
            _cartService.AddTocart(cart, pizzaToAddCart);

            _cartSessionService.SetCart(cart);

            var cartFromSession = _cartSessionService.GetCart();
            int totalPizzas = pizzaToAddCart.NumberOfPizza;

            Cart _cartItems = new Cart()
            {
                CartItems = new List<CartItems>()
                 {
                      new CartItems()
                      {
                          Pizza = pizzaToAddCart,
                      } 
                 },
            };
            _cartItems.TotalPrice = _cartItems.CartItems.Sum(c => c.Pizza.Price * quantityFromCart);
            _cartItems.TotalQuantity = _cartItems.CartItems.Sum(c => quantityFromCart);

            cache.Add("cart", _cartItems, null);
            return Ok(totalPizzas);
        }

        [HttpGet("pizzas")]
        public ActionResult GetPizzasFromCart()
        {
            Cart cart = null;
            if (cache.Contains(CacheKey))
              cart = (Cart)cache.Get(CacheKey);

            return Ok(cart);
        }

        [HttpGet("pizzas/totalQuantity")]
        public ActionResult GetTotalQuantity()
        {
            Cart cart = null;
            if (cache.Contains(CacheKey))
            {
                cart = (Cart)cache.Get(CacheKey);
                return Ok(cart.TotalQuantity);
            }

            else
            {
                return null;
            }

           
        }

        [HttpDelete("pizzas/{pizzaId}")]
        public ActionResult DeletePizzaFromCart(int pizzaId)
        {
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, pizzaId);

            _cartSessionService.SetCart(cart);
            var cartToReturn = _cartSessionService.GetCart();

            return Ok(cartToReturn);
        }
    }
}