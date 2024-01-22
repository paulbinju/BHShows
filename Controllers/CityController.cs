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
    public class CityController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: City
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            var city_T = db.City_T.Include(c => c.Country_T);

            ViewBag.menuno = 5;
            ViewBag.submenuno = 2;


            return View(city_T.ToList());
        }

        // GET: City/Details/5
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
            City_T city_T = db.City_T.Find(id);
            if (city_T == null)
            {
                return HttpNotFound();
            }
            return View(city_T);
        }

        // GET: City/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country");
            return View();
        }

        // POST: City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityID,CountryID,City")] City_T city_T)
        {

            if (ModelState.IsValid)
            {
                db.City_T.Add(city_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", city_T.CountryID);
            return View(city_T);
        }

        // GET: City/Edit/5
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
            City_T city_T = db.City_T.Find(id);
            if (city_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", city_T.CountryID);
            return View(city_T);
        }

        // POST: City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityID,CountryID,City")] City_T city_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(city_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Country_T, "CountryID", "Country", city_T.CountryID);
            return View(city_T);
        }

        // GET: City/Delete/5
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
            City_T city_T = db.City_T.Find(id);
            if (city_T == null)
            {
                return HttpNotFound();
            }
            return View(city_T);
        }

        // POST: City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            City_T city_T = db.City_T.Find(id);
            db.City_T.Remove(city_T);
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
