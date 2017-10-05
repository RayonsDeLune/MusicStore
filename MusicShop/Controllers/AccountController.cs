using MusicShop.Filters;
using MusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Controllers
{
    public class AccountController : Controller
    {
        private DB db = new DB();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // permet de s'identifier
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn([Bind(Include = "Email,Password")] User user)
        {
            try
            {
                user = db.Users.First((u) => u.Email == user.Email && u.Password == user.Password);
                Session["UserID"] = user.UserId;
                Session["UserFirstName"] = user.FirstName;
                Session["UserLastName"] = user.LastName;
                Session["Admin"] = (user.Role == Models.User.RoleUser.admin) ? true : false;
                try
                {
                    Session["CartId"] = db.ShoppingCarts.First(l => l.User.UserId == user.UserId).ShoppingCartId;
                }
                catch (Exception e)
                {
                    Session["CartId"] = null;
                }
                return RedirectToRoute(new { controller = "Home" });
            }
            catch (Exception e)
            {
                // mauvais email/password
                ViewBag.ErrorMessage = "Couple email/password non reconnu. Veuillez réessayer ou créer un nouveau compte.";
            }

            return View();
        }

        // permet de s'identifier
        public ActionResult LogOut()
        {
            //Session["UserID"] = null;
            //Session["UserFirstName"] = null;
            //Session["UserLastName"] = null;
            //Session["Admin"] =  false;
            Session.Clear();
            return RedirectToRoute(new { controller = "Home" });
        }

        [IsIdentified]
        public String Get(int? id)
        {
            return "vous etes connecté";
        }
    }
}