﻿using Microsoft.AspNetCore.Mvc;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}