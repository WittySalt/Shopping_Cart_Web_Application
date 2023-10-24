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
                return View();
            }

            //Check username & password input
            if(username !=null && password != null)
            {
                User? checkUser = db.User.FirstOrDefault(x => x.UserName == username && x.Password == password);
                //string checkUser = "Pinata";
                //string checkPw = "test123";

                if (checkUser != null)
                {
                    HttpContext.Session.SetString("username", username);

                    //redirect to product gallery
                    return RedirectToAction("Index", "Gallery");
                }

                //if (checkUser == username && checkPw == password)
                //{
                //    HttpContext.Session.SetString("username", username);

                //    //redirect to product gallery
                //    return RedirectToAction("Index", "Gallery");
                //}
                else
                {
                    ViewBag.ErrorMessage = "Wrong username or password";
                    return View();
                }
            }

            // redirect page to Gallery
            return RedirectToAction("Index", "Gallery");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
