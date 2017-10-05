using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicShop.Models;

namespace MusicShop.Controllers
{
    public class MusicsController : Controller
    {
        private DB db = new DB();

        // GET: Musics
        public ActionResult Index()
        {
            return View(db.Musics.ToList());
        }

        // GET: Musics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // GET: Musics/Create
        public ActionResult Create()
        {
            ViewBag.listArtist = db.Artists.ToList().Select((artist) => new SelectListItem { Text = artist.ArtistName, Value = artist.ArtistId.ToString() });
            return View();
        }

        // POST: Musics/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusicId,Title,Duration")] Music music, List<int> listArtist)
        {
            if (ModelState.IsValid)
            {
                foreach (int a in listArtist)
                {
                    music.ListArtist.Add(db.Artists.Find(a));
                }
                db.Musics.Add(music);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(music);
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            ViewBag.listArtist = db.Artists.ToList();
            ViewBag.selectedArtist = music.ListArtist.Select(a => a.ArtistId).ToList();
            ViewBag.multi = new MultiSelectList(ViewBag.listArtist, "ArtistId", "ArtistName",null, ViewBag.selectedArtist);
            return View(music);
        }

        // POST: Musics/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusicId,Title,Duration")] Music music, List<int> listArtist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(music).State = EntityState.Modified;
                db.Entry(music).Collection("ListArtist").Load();
                music.ListArtist.Clear();
                foreach (int i in listArtist)
                {
                    music.ListArtist.Add(db.Artists.Find(i));
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(music);
        }

        // GET: Musics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Music music = db.Musics.Find(id);
            if (music == null)
            {
                return HttpNotFound();
            }
            return View(music);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Music music = db.Musics.Find(id);
            db.Musics.Remove(music);
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
