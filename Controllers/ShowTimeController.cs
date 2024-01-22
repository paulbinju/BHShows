using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BHShows.Models;

namespace BHShows.Controllers
{
    public class ShowTimeController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: ShowTime
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            var showTime_T = db.ShowTime_T.Include(s => s.Cinema_T).Include(s => s.Movies_T);

            ViewBag.menuno = 1;
            return View(showTime_T.ToList());
        }

        // GET: ShowTime/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime_T showTime_T = db.ShowTime_T.Find(id);
            if (showTime_T == null)
            {
                return HttpNotFound();
            }
            return View(showTime_T);
        }

        // GET: ShowTime/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            ViewBag.CinemaID = new SelectList(db.Cinema_T, "CinemaID", "Name");
            ViewBag.MovieID = new SelectList(db.Movies_T, "MovieID", "Name");
            return View();
        }

        // POST: ShowTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ShowtimeID,ShowTime,CinemaID,MovieID")] ShowTime_T showTime_T)
        {
            if (ModelState.IsValid)
            {
                db.ShowTime_T.Add(showTime_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CinemaID = new SelectList(db.Cinema_T, "CinemaID", "Name", showTime_T.CinemaID);
            ViewBag.MovieID = new SelectList(db.Movies_T, "MovieID", "Name", showTime_T.MovieID);
            return View(showTime_T);
        }

        // GET: ShowTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime_T showTime_T = db.ShowTime_T.Find(id);
            if (showTime_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinemaID = new SelectList(db.Cinema_T, "CinemaID", "Name", showTime_T.CinemaID);
            ViewBag.MovieID = new SelectList(db.Movies_T, "MovieID", "Name", showTime_T.MovieID);
            return View(showTime_T);
        }

        // POST: ShowTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShowtimeID,ShowTime,CinemaID,MovieID")] ShowTime_T showTime_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showTime_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinemaID = new SelectList(db.Cinema_T, "CinemaID", "Name", showTime_T.CinemaID);
            ViewBag.MovieID = new SelectList(db.Movies_T, "MovieID", "Name", showTime_T.MovieID);
            return View(showTime_T);
        }

        // GET: ShowTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowTime_T showTime_T = db.ShowTime_T.Find(id);
            if (showTime_T == null)
            {
                return HttpNotFound();
            }
            return View(showTime_T);
        }

        // POST: ShowTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowTime_T showTime_T = db.ShowTime_T.Find(id);
            db.ShowTime_T.Remove(showTime_T);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteAll()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }

            db.Database.ExecuteSqlCommand("delete from ShowTime_T");
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
