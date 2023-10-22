using Depuntzak_V2.Models;
using System.Collections.Generic;

namespace Depuntzak_V2.Services
{
    public class ShoppingCartService
    {
        private List<CartItem> _cartItems = new List<CartItem>();

        public void AddToCart(Product product)
{
    var existingItem = _cartItems.Find(item => item.ProductId == product.Id);
    if (existingItem != null)
    {
        existingItem.Quantity++;
    }
    else
    {
        _cartItems.Add(new CartItem
        {
            ProductId = product.Id,
            Quantity = 1,
            Product = product
        });
    }
   
}

        public List<CartItem> ViewCart()
        {
            return _cartItems;
        }
    }
}
