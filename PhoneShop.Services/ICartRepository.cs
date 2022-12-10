using PhoneShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneShop.Services
{
    public interface ICartRepository
    {
        IEnumerable<CartItem> GetAllItemsFromCart(string username);
        CartItem AddItemToCart(string username, int id);
        CartItem DeleteItemFromCart(string username, int id);
    }
}
