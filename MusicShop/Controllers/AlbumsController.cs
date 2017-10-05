using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicShop.Models;
using System.IO;

namespace MusicShop.Controllers
{
    public class AlbumsController : Controller
    {
        private DB db = new DB();

        // GET: Albums
        public ActionResult Index()
        {

            return View(db.Albums.Include(g => g.Genre).ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Include(g => g.Genre).First((a) => a.AlbumId == id);
            if (album == null)
            {
                return HttpNotFound();
            }

            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.listGenre = db.Genres.ToList().Select((genre) => new SelectListItem { Text = genre.Name, Value = genre.GenreId.ToString() });
            ViewBag.listArtist = db.Artists.ToList().Select((artist) => new SelectListItem { Text = artist.ArtistName, Value = artist.ArtistId.ToString() });
            ViewBag.listMusic = db.Musics.ToList().Select((music) => new SelectListItem { Text = music.Title, Value = music.MusicId.ToString() });
            return View();
        }

        // POST: Albums/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,Title,Description,Date,Cover,Price")] Album album, List<int> listArtist, List<int> listMusic, int Genre, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                foreach (int a in listArtist)
                {
                    album.ListArtist.Add(db.Artists.Find(a));
                }
                foreach (int a in listMusic)
                {
                    album.ListMusic.Add(db.Musics.Find(a));
                }
                album.Genre = db.Genres.Find(Genre);

                db.Albums.Add(album);
                db.SaveChanges();
                
                // recuperer la jaquette
                try
                {
                    if (file.ContentLength > 0)
                    {
                        //string FileName = Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("/img"), "cover" + album.AlbumId+Path.GetExtension(file.FileName));
                        file.SaveAs(path);
                        album.Cover = "/img/cover" + album.AlbumId+ Path.GetExtension(file.FileName);
                    }
                    ViewBag.Message = "Ficher envoyé";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Message = "erreur de fichier";
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.listGenre = db.Genres.ToList().Select((genre) => new SelectListItem { Text = genre.Name, Value = genre.GenreId.ToString() });
            //ViewBag.listGenre = db.Genres.ToList().Select(g => g.GenreId).ToList();
            ViewBag.selectedGenre = album.Genre.GenreId;
            ViewBag.listArtist = db.Artists.ToList();
            ViewBag.selectedArtist = album.ListArtist.Select(a => a.ArtistId).ToList();
            ViewBag.listMusic = db.Musics.ToList();
            ViewBag.selectedMusic = album.ListMusic.Select(m => m.MusicId).ToList();
            return View(album);
        }

        // POST: Albums/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,Description,Date,Cover,Price")] Album album, List<int> listArtist, List<int> listMusic, int Genre)
        {
            if (ModelState.IsValid)
            {
                /* chargement du genre */
                album = db.Albums.Include(a => a.Genre).First(a => a.AlbumId == album.AlbumId);
                db.Entry(album).State = EntityState.Modified;
                /* chargement de la collection pour la supprimer et la recréer */
                db.Entry(album).Collection("ListArtist").Load();
                album.ListArtist.Clear();
                foreach (int i in listArtist)
                {
                    album.ListArtist.Add(db.Artists.Find(i));
                }
                /* chargement de la collection pour la supprimer et la recréer */
                db.Entry(album).Collection("ListMusic").Load();
                album.ListMusic.Clear();
                foreach (int i in listMusic)
                {
                    album.ListMusic.Add(db.Musics.Find(i));
                }
                album.Genre = db.Genres.Find(Genre);
                /* sauvegarde de l'entité en base */
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
