using MusicShop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Filters
{
    public class IsIdentified : ActionFilterAttribute
    {
        private DB db = new DB();
        // filtre à appliquer avant l'execution
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Debug.WriteLine("Action Executing");
            if (filterContext.HttpContext.Session["UserID"] != null && filterContext.HttpContext.Session["UserLastName"] != null )
            {
                string useridS = filterContext.HttpContext.Session["UserID"].ToString();
                string userlastname = filterContext.HttpContext.Session["UserLastName"].ToString();
                int userid;
                int.TryParse(useridS, out userid);
                // aller chercher dans la base si le user existe
                try
                {
                    User user;
                    user = db.Users.First((u) => u.UserId == userid && u.LastName == userlastname);
                }
                catch (Exception e)
                {
                    // mauvais id/nom, déconnexion
                    filterContext.HttpContext.Response.RedirectToRoute("Default", new { controller = "Account", action = "LogOut" });
                }
            }
            else
            {
                filterContext.HttpContext.Response.RedirectToRoute("Default", new { controller = "Account", action = "LogOut" });
            }
        }

        /*
        // filtre à appliquer pendant l'execution
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Debug.WriteLine("Action Executed");
            filterContext.HttpContext.Response.RedirectToRoute("Default", new { controller = "Client", action = "Index", id = "Mauricette" });
        }

        // filtre à appliquer après le calcul du résultat
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Debug.WriteLine("Result Executed");
        }

        // filtre à appliquer aavant le calcul du résultat
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Debug.WriteLine("Result Executing");
        }
        */
    }
}