using Microsoft.AspNetCore.Mvc;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
