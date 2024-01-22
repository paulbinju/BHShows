using BHShows.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
 

namespace BHShows.Controllers
{
    public class CinemaController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: Cinema
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            var cinema_T = db.Cinema_T.Include(c => c.City_T).Include(c => c.Country_T);

            ViewBag.menuno = 3;
            return View(cinema_T.ToList());
        }

        // GET: Cinema/Details/5
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
            Cinema_T cinema_T = db.Cinema_T.Find(id);
            if (cinema_T == null)
            {
                return HttpNotFound();
            }
            return View(cinema_T);
        }

        // GET: Cinema/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            ViewBag.CityID = new SelectList(db.City_T, "CityID", "City");
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country");
            return View();
        }

        // POST: Cinema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CinemaID,Name,CityID,CountryID,Location,LatLong")] Cinema_T cinema_T)
        {
            if (ModelState.IsValid)
            {
                db.Cinema_T.Add(cinema_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.City_T, "CityID", "City", cinema_T.CityID);
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", cinema_T.CountryID);
            return View(cinema_T);
        }

        // GET: Cinema/Edit/5
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
            Cinema_T cinema_T = db.Cinema_T.Find(id);
            if (cinema_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.City_T, "CityID", "City", cinema_T.CityID);
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", cinema_T.CountryID);
            return View(cinema_T);
        }

        // POST: Cinema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CinemaID,Name,CityID,CountryID,Location,LatLong")] Cinema_T cinema_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cinema_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.City_T, "CityID", "City", cinema_T.CityID);
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", cinema_T.CountryID);
            return View(cinema_T);
        }

        // GET: Cinema/Delete/5
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
            Cinema_T cinema_T = db.Cinema_T.Find(id);
            if (cinema_T == null)
            {
                return HttpNotFound();
            }
            return View(cinema_T);
        }

        // POST: Cinema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cinema_T cinema_T = db.Cinema_T.Find(id);
            db.Cinema_T.Remove(cinema_T);
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
