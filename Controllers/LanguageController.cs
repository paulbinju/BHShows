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
    public class LanguageController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: Language
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }
            ViewBag.menuno = 5;
            ViewBag.submenuno = 3;

            return View(db.Language_T.ToList());
        }

        // GET: Language/Details/5
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
            Language_T language_T = db.Language_T.Find(id);
            if (language_T == null)
            {
                return HttpNotFound();
            }
            return View(language_T);
        }

        // GET: Language/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Language/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LanguageID,Language")] Language_T language_T)
        {
            if (ModelState.IsValid)
            {
                db.Language_T.Add(language_T);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(language_T);
        }

        // GET: Language/Edit/5
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
            Language_T language_T = db.Language_T.Find(id);
            if (language_T == null)
            {
                return HttpNotFound();
            }
            return View(language_T);
        }

        // POST: Language/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LanguageID,Language")] Language_T language_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(language_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(language_T);
        }

        // GET: Language/Delete/5
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
            Language_T language_T = db.Language_T.Find(id);
            if (language_T == null)
            {
                return HttpNotFound();
            }
            return View(language_T);
        }

        // POST: Language/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Language_T language_T = db.Language_T.Find(id);
            db.Language_T.Remove(language_T);
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
