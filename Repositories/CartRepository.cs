
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
		public string GetUserId()
		{
			string username = _httpContextAccessor.HttpContext.Session.GetString("username");
			var user = _db.User.FirstOrDefault(a => a.UserName == username);
			return user.Id.ToString();
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
			var userId = GetUserId();
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
			if(!string.IsNullOrEmpty(userId))
			{
				userId = GetUserId();
			}
			var data = await (from cart in _db.Cart
							  join cartDetail in _db.CartDetail
							  on cart.Id equals cartDetail.CartId
							  select new {cartDetail.Id}).ToListAsync();
			return data.Count;
		}
		//To get the amount of added cartItem in Cart (the kind of products)
		public async Task<int> AddItem(int productId, int quantity)
		{
			string userId = GetUserId();
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
			string userId = GetUserId();
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
				//transaction.Commit();
			}
			catch (Exception ex)
			{
				// Rollback the data if ex exists. Or just ignore this
				//transaction.Rollback();
			}
			var cartItemCount = await GetCartItemCount(userId);
			return cartItemCount;
		}
		//Remove Item from the Cart.
		public async Task<bool> DoCheckout()
		{
			using var transaction = _db.Database.BeginTransaction();
			try
			{
				var userId = GetUserId();
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
					CreateDate = DateTime.UtcNow,
					//OrderStatusId = 1//pending
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
							ActivationCode = new Guid()
						};
						_db.OrderDetail.Add(orderDetail);
					}
				}
				_db.SaveChanges();

				// removing the cartdetails
				_db.CartDetail.RemoveRange(cartDetail);
				_db.SaveChanges();
				transaction.Commit();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
