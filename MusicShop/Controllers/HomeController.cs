using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicShop.Models;
using MusicShop.Filters;
using PagedList;

namespace MusicShop.Controllers
{
    public class HomeController : Controller
    {
        private DB db = new DB();

        // GET: Home
        public ActionResult Index(string sortOrder, string CurrentSort, int? idFiltreGenre, int? idFiltreArtist, int? page )
        {
            int pageSize = 6;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Date" : sortOrder;
            IPagedList<Album> mesAlbums = null;

            mesAlbums = db.Albums.Include(g => g.Genre).OrderByDescending
                                (m => m.Date).ToPagedList(pageIndex, pageSize);

            if (idFiltreArtist != null)
            {
                List<Album> listAlbums = new List<Album>();
                foreach (Album album in db.Albums.ToList())
                {
                    foreach (Artist artist in album.ListArtist)
                    {
                        // si l'album n'est pas déjà dans la liste d'album et que l'artiste correspond
                        if (artist.ArtistId == idFiltreArtist && listAlbums.Find(a => a.AlbumId == album.AlbumId) == null)
                        {
                            listAlbums.Add(album);
                        }
                    }
                }
                //db.Genres.ToList();
                //ViewBag.listGenre = db.Genres.OrderBy(g => g.Name).ToList();
                //ViewBag.listArtist = db.Artists.OrderBy(a => a.ArtistName).ToList();
                //List<Album> listAlbums = db.Albums.Include(g => g.Genre).Where(a => id between a.ListArtist).ToList();
                //return View("Index", listAlbums);
                mesAlbums = listAlbums.OrderByDescending
                                (m => m.Date).ToPagedList(pageIndex, pageSize);
            }

            if (idFiltreGenre != null)
            {
                List<Album> listAlbums = db.Albums.Include(g => g.Genre).Where(g => g.Genre.GenreId == idFiltreGenre).ToList();
                mesAlbums = listAlbums.OrderByDescending
                               (m => m.Date).ToPagedList(pageIndex, pageSize);
            }

            db.Genres.ToList();
            ViewBag.ListGenre = db.Genres.OrderBy(g => g.Name).ToList();
            ViewBag.ListArtist = db.Artists.OrderBy(a => a.ArtistName).ToList();
            // return View(db.Albums.Include(g => g.Genre).ToList());
            return View(mesAlbums);
        }

        public ActionResult Details(int? id)
        {
            Album album = db.Albums.Find(id);
            if (album != null)
            {

                return View(album);
            }
            return RedirectToAction("Index");
        }

        //public ActionResult filtrerGenre(int? id)
        //{
            
        //    return RedirectToAction("Index");
        //}

        //public ActionResult filtrerArtist(int? id)
        //{
           
            
        //    return RedirectToAction("Index");
        //}

    }
}