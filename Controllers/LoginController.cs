using Microsoft.AspNetCore.Mvc;
using Shopping_Cart_Web_Application_V1._0.Data;
using Shopping_Cart_Web_Application_V1._0.Models;
using System.Diagnostics;



namespace Shopping_Cart_Web_Application_V1._0.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext db;

        public LoginController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(string username, string password)
        {
            if(HttpContext.Session.GetString("username") != null)
            {
                //redirect to product gallery
                return RedirectToAction("Index", "Gallery");
            }
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewData["nullUsernameOrPassword"] = true;
                return View();
            }

            //Check username & password input
            if(username !=null && password != null)
            {
                User checkUser = db.User.FirstOrDefault(x => x.UserName == username);
                User checkPw = db.User.FirstOrDefault(x => x.Password == password);

                if (checkUser != null && checkPw != null)
                {
                    HttpContext.Session.SetString("username", username);

                    //redirect to product gallery
                    return RedirectToAction("Index", "Gallery");
                }
                else
                {
                    ViewData["wrongUsernameOrPassword"] = true;
                    return View();
                }
            }
            
            
            // redirect page to Gallery
            return RedirectToAction("Index", "Gellery");
        }

 
    }
}
