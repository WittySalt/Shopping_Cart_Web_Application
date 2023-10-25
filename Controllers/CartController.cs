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
		public async Task<IActionResult> AddItem(int productId, int quantity = 1, int redirect = 0)
		{
			var cartCount = await _cartRepository.AddItem(productId, quantity);
			if (redirect == 0)
			{
				return Ok(cartCount);
			}
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> RemoveItem(int productId)
		{
			var cartCount = await _cartRepository.RemoveItem(productId);
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Index()
		{
			if (await _cartRepository.GetUserId() == null)
			{
				return RedirectToAction("Index", "Login");
			}
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
            var orderDate = await _cartRepository.DoCheckout();
            if (!orderDate.HasValue)
                throw new Exception("Something happen in server side");
            TempData["OrderDate"] = orderDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            return RedirectToAction("Checked", "Cart");
        }
		public async Task<IActionResult> Checked()
		{
			if (await _cartRepository.GetUserId() is null)
			{
				return RedirectToAction("Index");
			}
            if (TempData["OrderDate"] is string orderDate)
            {
                ViewBag.OrderDate = orderDate; // 使用 ViewBag 将订单日期传递到视图
            }
            return View();
		}
        public async Task<IActionResult> UpdateQuantity(int productId, int quantity)
        {
            var cartDetail = await _cartRepository.UpdateQuantityAsync(productId, quantity);

            if (cartDetail == null)
            {
                return Json(new { success = false, message = "Cart detail not found." });
            }

            if (quantity == 0)
            {
                // Since the item has been removed, fetch the new cart total
                var cart = await _cartRepository.GetUserCart();
                var newCartTotal = cart.CartDetail.Sum(item => item.Quantity * item.UnitPrice);

                // Check if the cart is now empty
                if (!cart.CartDetail.Any())
                {
                    return Json(new { success = true, newTotalQuantity = 0 });
                }

                return Json(new { success = true, cartTotal = newCartTotal });
            }

            var newTotal = cartDetail.Quantity * cartDetail.UnitPrice;

            // Also calculate the new cart total after quantity update
            var updatedCart = await _cartRepository.GetUserCart();
            var updatedCartTotal = updatedCart.CartDetail.Sum(item => item.Quantity * item.UnitPrice);

            return Json(new { success = true, newTotal = newTotal, cartTotal = updatedCartTotal });
        }
    }
}
