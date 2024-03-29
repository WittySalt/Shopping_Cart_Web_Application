﻿using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Web_Application_V1._0.Data;
using Shopping_Cart_Web_Application_V1._0.Models;

namespace Shopping_Cart_Web_Application_V1._0.Repositories
{
	public class CartRepository:ICartRepository
	{
		private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor) 
		{
			_httpContextAccessor = httpContextAccessor;
			_db = db;
		}
		//So far we haven't decide any HttpContext should be added in.
		public async Task<string> GetUserId()
		{
			var username = _httpContextAccessor.HttpContext.Session.GetString("username");
            var user = await _db.User
                    .Where(u => u.UserName == username)
                    .FirstOrDefaultAsync();

            if (user != null)
            {
                return user.Id.ToString();
            }
            else
            {
                return null;
            }
        }
		//To get the UserId of User so that can find the unique Cart for him.
		public async Task<Cart> GetCart(string userId)
		{
			var cart =  await _db.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
			return cart;
		}
		//To get the unique Cart.
		public async Task<Cart> GetUserCart()
		{
			var userId = await GetUserId();
			if (userId == null)
			{
				throw new Exception("Invalid UserId");
			}
			var cart = await _db.Cart
							.Include(a => a.CartDetail)
							.ThenInclude(a => a.Product)
							.Where(a => a.UserId == userId)
							.FirstOrDefaultAsync();
			return cart;
		}
		public async Task<int> GetCartItemCount(string userId = "")
		{
			if(string.IsNullOrEmpty(userId))
			{
				userId = await GetUserId();
			}
			var data = await (from cart in _db.Cart
							  join cartDetail in _db.CartDetail
							  on cart.Id equals cartDetail.CartId
                              where cart.UserId == userId
                              select new {cartDetail.Id}).ToListAsync();
			return data.Count;
		}
		//To get the amount of added cartItem in Cart (the kind of products)
		public async Task<int> AddItem(int productId, int quantity)
		{
			string userId = await GetUserId();
			using var transaction = _db.Database.BeginTransaction();
			//Begin the database transaction
			try
			{
				if (string.IsNullOrEmpty(userId))
					throw new Exception("User Logged out");
				var cart = await GetCart(userId);
				if (cart is null)
				{
					cart = new Cart
					{
						UserId = userId
					};
					_db.Cart.Add(cart);
				}
				_db.SaveChanges();
				var cartItem = _db.CartDetail.FirstOrDefault(c => c.CartId == cart.Id && c.ProductId == productId);
				if (cartItem is not null)
				{
					cartItem.Quantity += quantity;
				}
				else
				{
					var product = _db.Product.Find(productId);
					cartItem = new CartDetail
					{
						ProductId = productId,
						CartId = cart.Id,
						Quantity = quantity,
						UnitPrice = product.Price
					};
					_db.CartDetail.Add(cartItem);
				}
				_db.SaveChanges();
				transaction.Commit();
				//commit the changes.
			}
			catch (Exception ex)
			{
				// Rollback the data if ex exists. Or just ignore this
				//transaction.Rollback();
			}
			var cartItemCount = await GetCartItemCount(userId);
			return cartItemCount;
		}
		//Add Item to the Cart
		public async Task<int> RemoveItem(int productId)
		{
			string userId = await GetUserId();
			//using var transaction = _db.Database.BeginTransaction();
			//As long as you can remove the Item, you must have added something already.
			try
			{
				//check the Cart
				if (string.IsNullOrEmpty(userId))
				{
					throw new Exception("User Logged out");
				}
				var cart = await GetCart(userId);
				if (cart is null)
				{
					throw new Exception("Invalid Cart");
				}
				//check the CartDetail
				var cartItem = _db.CartDetail.FirstOrDefault(c => c.CartId == cart.Id && c.ProductId == productId);
				if (cartItem is null)
				{
					throw new Exception("No Items in Cart");
				}
				else if (cartItem.Quantity == 1) 
				{
					_db.CartDetail.Remove(cartItem);
				}
				else
				{
					cartItem.Quantity--;
				}
				_db.SaveChanges();
			}
			catch (Exception ex)
			{

			}
			var cartItemCount = await GetCartItemCount(userId);
			return cartItemCount;
		}
        //Remove Item from the Cart.
        public async Task<CartDetail> UpdateQuantityAsync(int productId, int quantity)
        {
            string userId = await GetUserId();
            var userCartId = _db.Cart.FirstOrDefault(u => u.UserId == userId)?.Id;
            var cartDetail = await _db.CartDetail
                                               .FirstOrDefaultAsync(cd => cd.ProductId == productId && cd.CartId == userCartId);

            if (cartDetail != null)
            {
                if (quantity == 0)
                {
                    _db.CartDetail.Remove(cartDetail);
                }
                else
                {
                    cartDetail.Quantity = quantity;
                }

                await _db.SaveChangesAsync();
            }

            return cartDetail;
        }
        public async Task<DateTime?> DoCheckout()
		{
			using var transaction = _db.Database.BeginTransaction();
			try
			{
				var userId = await GetUserId();
				if (string.IsNullOrEmpty(userId))
				{
					throw new Exception("User Logged out");
				}
				var cart = await GetCart(userId);
				if (cart is null)
				{
					throw new Exception("Invalid cart");
				}
				var cartDetail = _db.CartDetail
									.Where(a => a.CartId == cart.Id).ToList();
				if(cartDetail.Count == 0)
				{
					throw new Exception("Cart is empty");
				}
				var order = new Order
				{
					UserId = userId,
					CreateDate = DateTime.Now,
				};
				_db.Order.Add(order);
				_db.SaveChanges();
				foreach (var item in cartDetail)
				{
					for (int i = 0; i < item.Quantity; i++)
					{
						var orderDetail = new OrderDetail
						{
							ProductId = item.ProductId,
							OrderId = order.Id,
							UnitPrice = item.UnitPrice,
							ActivationCode = Guid.NewGuid()
						};
						_db.OrderDetail.Add(orderDetail);
					}
				}
				_db.SaveChanges();

				// removing the cartdetails
				_db.CartDetail.RemoveRange(cartDetail);
				_db.SaveChanges();
				transaction.Commit();
                return order.CreateDate;
            }
			catch (Exception)
			{
				return null;
			}
		}
	}
}
