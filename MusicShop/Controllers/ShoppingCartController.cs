using MusicShop.Filters;
using MusicShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private DB db = new DB();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [IsIdentified]
        // affichage de la vue de choix de paiement
        public ActionResult PaymentChoice()
        {
            return View();
        }

        [IsIdentified]
        [HttpPost]
        [ValidateAntiForgeryToken]
        // traitement du choix de paiement et validation de la commande
        public ActionResult PaymentChoice([Bind(Include = "Payment")] Order order)
        {
            // recuperer le user
            User user = db.Users.Find(Session["UserId"]);

            order.Date = DateTime.Now;
            order.User = user;

            // récupérer le panier
            int session = int.Parse(Session["CartId"].ToString());
            ShoppingCart myCart = db.ShoppingCarts.First(c => c.ShoppingCartId == session);
            db.Entry(myCart).Collection("ListItemCart").Load();

            //List<ItemCart> mesItems = db.ItemCarts.Select((i) => i.ShoppingCart.ShoppingCartId == myCart.ShoppingCartId);

            // recuperer la listItemCart
            order.ListItemCart = myCart.ListItemCart;
            order = db.Orders.Add(order);

            // pour chaque listItemCart affecter le numero Order
            foreach (ItemCart item in order.ListItemCart)
            {
                item.Order.OrderId = order.OrderId;
                //item.ShoppingCart.ShoppingCartId = null;
            }

            //supprime le shoppingCart
            db.ShoppingCarts.Remove(myCart);

            // sauvegarde en base
            db.SaveChanges();

            // supprime les variables de session relatives au panier
            Session["CartId"] = null;

            return View("Thanks");
        }


        [IsIdentified]
        // affichage du panier
        public ActionResult displayCart()
        {
            // récupérer toutes les infos dont on a besoin
            ShoppingCart myCart = db.ShoppingCarts.Find(Session["CartId"]);
            // charger les items cart et les albums
            db.Entry(myCart).Collection("ListItemCart").Load();
            db.Albums.ToList();

            // charger les choix de paiement
            //ViewBag.payment = Order.PaymentType

            //ViewBag.myCart = myCart;
            // .Join(Album, Album.AlbumId) .Include(a => a.album)
            //ViewBag.listItemCart = myCart.ListItemCart.Select(l => l.ShoppingCart.ShoppingCartId == myCart.ShoppingCartId).ToList();
            // db.ItemCarts.Include(sc=>sc.ShoppingCart).Include(a=>a.Album).ToList()
            return View(myCart);
        }

        [IsIdentified]
        // supprimer 1 article
        public ActionResult Delete(int id)
        {
            // récupérer toutes les infos dont on a besoin
            ShoppingCart myCart = db.ShoppingCarts.Find(Session["CartId"]);
            //ItemCart myItem = db.ItemCarts.Find(id);
            //myCart.ListItemCart.RemoveAt(myCart.ListItemCart.Find(i=>i.ItemCartId == myItem.ItemCartId).ItemCartId);
            //db.ItemCarts.Remove(myItem);
            //db.SaveChanges();
            return View(myCart);
        }

        [IsIdentified]
        // décrémenter la quantité
        public ActionResult Subtract(int id)
        {
            ShoppingCart myCart = db.ShoppingCarts.Find(Session["CartId"]);
            //ItemCart myItem = db.ItemCarts.Find(id);
            //myItem.Quantite = (myItem.Quantite > 1) ? myItem.Quantite - 1 : 1;
            //myItem.TotalPrice = myItem.Quantite * myItem.UnitPrice;
            //db.SaveChanges();
            return View(myCart);
        }

        [IsIdentified]
        // ajout au panier
        public ActionResult AddItem(int? id)
        {
            ShoppingCart myCart = new ShoppingCart();
            // recuperer l'album
            Album album = db.Albums.Find(id);
            // recuperer le user
            User user = db.Users.Find(Session["UserId"]);
            // regarder s'il y a déjà un shoppingCart
            if (Session["CartId"] == null)
            {
                // ici il n'y a pas encore de shoppingCart
                myCart.User = user;
                myCart.Date = DateTime.Now;
                db.ShoppingCarts.Add(myCart);
                db.SaveChanges();
                // on récupere l'objet enregistré en base 
                //myCart = db.ShoppingCarts.First(c => c.User.UserId == myCart.User.UserId && c.Date == myCart.Date);
                Session["CartId"] = myCart.ShoppingCartId;
            }
            else
            {
                // attention au cas où il y a n'importe quoi dans la var de session .... a faire
                int session = int.Parse(Session["CartId"].ToString());
                myCart = db.ShoppingCarts.First(c => c.ShoppingCartId == session);
            }
            // il y a maintenant un shoppingcart
            // on regarde s'il y a un item cart déjà existant
            ItemCart myItem;
            try
            {
                myItem = db.ItemCarts.First(ic => ic.Album.AlbumId == album.AlbumId && ic.ShoppingCart.ShoppingCartId == myCart.ShoppingCartId);
                // le panier existe
                // on met à jour la quantité
                myItem.Quantite += 1;
                myItem.UnitPrice = album.Price;
                myItem.TotalPrice = album.Price * myItem.Quantite;
            }
            catch (Exception e)
            {
                // le panier n'existe pas
                myItem = new ItemCart();
                myItem.Album = album;
                myItem.Quantite = 1;
                myItem.UnitPrice = album.Price;
                myItem.TotalPrice = album.Price * myItem.Quantite;
                myItem.ShoppingCart = myCart;
                db.ItemCarts.Add(myItem);
                db.SaveChanges();
                // on recupere l'élément
                //myItem = db.ItemCarts.First(ic => ic.Album.AlbumId == album.AlbumId && ic.ShoppingCart.ShoppingCartId == myCart.ShoppingCartId);
            }
            // on ajoute le ItemCart à la listItemCart du ShoppîngCart
            myCart.ListItemCart.Add(myItem);

            db.SaveChanges();
            //return RedirectToRoute(new { controller = "ShoppingCart", action = "displayCart" });
            return RedirectToRoute(new { controller = "Home" });
            //return View(db.Albums.Include(g => g.Genre).ToList());
        }
    }
}