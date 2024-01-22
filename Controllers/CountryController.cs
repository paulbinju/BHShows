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
    public class CountryController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: Country
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            ViewBag.menuno = 5;
            ViewBag.submenuno = 1;

            return View(db.Country_T.ToList());
        }

        // GET: Country/Details/5
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
            Country_T country_T = db.Country_T.Find(id);
            if (country_T == null)
            {
                return HttpNotFound();
            }
            return View(country_T);
        }

        // GET: Country/Create
        public ActionResult Create()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            return View();
        }

        // POST: Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,Country")] Country_T country_T)
        {
            if (ModelState.IsValid)
            {
                db.Country_T.Add(country_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(country_T);
        }

        // GET: Country/Edit/5
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
            Country_T country_T = db.Country_T.Find(id);
            if (country_T == null)
            {
                return HttpNotFound();
            }
            return View(country_T);
        }

        // POST: Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,Country")] Country_T country_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(country_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country_T);
        }

        // GET: Country/Delete/5
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
            Country_T country_T = db.Country_T.Find(id);
            if (country_T == null)
            {
                return HttpNotFound();
            }
            return View(country_T);
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Country_T country_T = db.Country_T.Find(id);
            db.Country_T.Remove(country_T);
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
