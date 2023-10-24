using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_Web_Application_V1._0.Repositories;
using Shopping_Cart_Web_Application_V1._0.Models;
using Shopping_Cart_Web_Application_V1._0.Models.DTOs;
using Microsoft.Extensions.Logging;
using System.IO;

namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class PurchaseHistoryController:Controller
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly ILogger<PurchaseHistoryController> _logger;
        public PurchaseHistoryController(IPurchaseHistoryRepository purchaseHistoryRepository, ILogger<PurchaseHistoryController> logger)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string sterm = "")
        {
            string storedUsername = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(storedUsername))
            {
                return RedirectToAction("Index", "Login");  // 假设Login方法在AccountController中
            }
            IEnumerable<Order> order = await _purchaseHistoryRepository.GetUserOrderHistory(sterm);
            PurchaseHistoryViewModel purchaseHistoryModel = new PurchaseHistoryViewModel()
            {
                Order = order,
                STerm = sterm
            };
            if (!string.IsNullOrEmpty(storedUsername))
            {
                ViewData["StoredUsername"] = storedUsername;
            }
            return View(purchaseHistoryModel);
        }
    }
}
