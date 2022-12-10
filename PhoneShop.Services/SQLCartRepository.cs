using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhoneShop.Services
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly PhoneDbContext contextPhone;
        private readonly CartAppDbContext contextCart;
        public SQLCartRepository(PhoneDbContext contextPhone, CartAppDbContext contextCart)
        {
            this.contextPhone = contextPhone;
            this.contextCart = contextCart;
        }

        public CartItem AddItemToCart(string username, int id)
        {
            Phone phone = contextPhone.Phone.Find(id);
            CartItem cartItem = new CartItem { ItemId = phone.Id.ToString(), UserName = username };
            contextCart.CartItems.Add(cartItem);
            contextCart.SaveChanges();
            return cartItem;
        }

        public IEnumerable<CartItem> GetAllItemsFromCart(string username)
        {
            return contextCart.CartItems.Where(x => x.UserName == username).ToList();
        }

        public CartItem DeleteItemFromCart(string username, int id)
        {
            CartItem cartItem = contextCart.CartItems.FirstOrDefault(x => x.Id == id);
            try
            {   
                contextCart.CartItems.Remove(cartItem);
                contextCart.SaveChanges();
                return cartItem;
            }
            catch
            {
                return cartItem;

            }
        }
    }
}
