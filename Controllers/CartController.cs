using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_Web_Application_V1._0.Repositories;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartRepository _cartRepository;
		public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}
		public async Task<IActionResult> AddItem(int productId, int quantity = 1 /*int redirect = 0*/)
		{
			var cartCount = await _cartRepository.AddItem(productId, quantity);
			/*if (redirect == 0)
			{
				return Ok(cartCount);
			}*/
			return RedirectToAction("Cart");
		}
		public async Task<IActionResult> RemoveItem(int productId)
		{
			var cartCount = await _cartRepository.RemoveItem(productId);
			return RedirectToAction("Cart");
		}
		public async Task<IActionResult> GetUserCart()
		{
			var cart = await _cartRepository.GetUserCart();
			return View(cart);
		}
		public async Task<IActionResult> GetTotalItemInCart()
		{
			int cartItem = await _cartRepository.GetCartItemCount();
			return Ok(cartItem);
		}
		public async Task<IActionResult> Checkout()
		{
			bool isCheckedOut = await _cartRepository.DoCheckout();
			if (!isCheckedOut)
				throw new Exception("Something happen in server side");
			return RedirectToAction("Index", "PurchasedHistory");
		}
	}
}
