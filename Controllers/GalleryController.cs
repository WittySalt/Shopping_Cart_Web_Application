using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_Web_Application_V1._0.Repositories;
using Shopping_Cart_Web_Application_V1._0.Models;
using Shopping_Cart_Web_Application_V1._0.Models.DTOs;
using Shopping_Cart_Web_Application_V1._0.Data;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository _galleryRepository;
        private readonly ILogger<GalleryController> _logger;
		public GalleryController(ILogger<GalleryController> logger, IGalleryRepository galleryRepository) 
        {
            _galleryRepository = galleryRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string sterm = "")
        {
            string? storedUsername = HttpContext.Session.GetString("username");
            IEnumerable<Product> products = await _galleryRepository.GetProducts(sterm);
            ProductViewModel productModel = new ProductViewModel()
            {
                Products = products,
                STerm = sterm
            };
            if (!string.IsNullOrEmpty(storedUsername))
            {
                ViewData["StoredUsername"] = storedUsername;
            }
            return View(productModel);
        }
        [HttpGet]
        public IActionResult IsLoggedIn()
        {
            bool isLoggedIn = HttpContext.Session.GetString("username") != null;
            return Json(new { IsLoggedIn = isLoggedIn });
        }
    }
}
