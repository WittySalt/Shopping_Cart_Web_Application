﻿using Shopping_Cart_Web_Application_V1._0.Models;
using Shopping_Cart_Web_Application_V1._0.Data;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
	//Create Interface For Cart
	public interface ICartRepository
	{
		Task<int> AddItem(int productId, int quantity);
		Task<int> RemoveItem(int productId);
		Task<Cart> GetCart(string userId);
		Task<Cart> GetUserCart();
		Task<int> GetCartItemCount(string userId = "");
		Task<DateTime?> DoCheckout();
		Task<string> GetUserId();
		Task<CartDetail> UpdateQuantityAsync(int productId, int quantity);

    }
}
