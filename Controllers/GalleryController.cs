using Microsoft.AspNetCore.Mvc;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            //for checking, can be overriden later
            string storedUsername = HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(storedUsername))
            {
                ViewData["StoredUsername"] = storedUsername;
            }
            return View();
        }
    }
}
