using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BHShows.Models;

namespace BHShows.Controllers
{
    public class MoviesController : Controller
    {
        private CinemazEntities db = new CinemazEntities();

        // GET: Movies
        public ActionResult Index()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }

            //            List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
            //join Language_T l on m.LanguageID=l.LanguageID
            //where m.MovieID in (select distinct movieid from ShowTime_T s)").ToList();

            List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select top 70 m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
join Language_T l on m.LanguageID=l.LanguageID order by m.movieid desc").ToList();

            ViewBag.menuno = 2;
            return View(movies_T);
        }


        public ActionResult Search()
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }

            var movies_T = db.Movies_T.Where(x => x.MovieID == 0);
            return View(movies_T.OrderByDescending(x => x.MovieID).ToList());
        }

        [HttpPost]
        public ActionResult Search(FormCollection col)
        {
            List<Movies_T> moviesearch = db.Database.SqlQuery<Movies_T>("select * from movies_t where name like '%" + col["name"] + "%'").ToList();
            return View(moviesearch.OrderByDescending(x => x.MovieID).ToList());
        }

        // GET: Movies/Details/5
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
            Movies_T movies_T = db.Movies_T.Find(id);
            if (movies_T == null)
            {
                return HttpNotFound();
            }

            ViewBag.movieid = id;
            List<Media_T> mediat = db.Media_T.Where(x => x.MovieID == id).ToList();

            ViewBag.mediat = mediat;

            ViewBag.showtimes = (from st in db.ShowTime_T
                                 join c in db.Cinema_T on st.CinemaID equals c.CinemaID
                                 where st.MovieID == id
                                 select new MovieCinemaShowtime { ShowTime = st.ShowTime,   CinemaID = c.CinemaID,   Name = c.Name}
                                 ).ToList();



             ViewBag.cinemas = db.Cinema_T.ToList();

            //ViewBag.cinemas = db.Database.SqlQuery<MovieCinemaShowtime>("select * from [dbo].[Cinema_T] c left join [dbo].[ShowTime_T] s on c.CinemaID=s.CinemaID where movieid= or  movieid is null or movieid=''");
            ViewBag.cinemovie = db.Database.SqlQuery<MovieCinemaShowtime>("select * from [dbo].[ShowTime_T] s  right outer join[dbo].[Cinema_T] c on s.CinemaID = c.CinemaID where MovieID = " + id);

            return View(movies_T);
        }

        public ActionResult AddPic(HttpPostedFileBase pic, FormCollection col) {

            Media_T media = new Media_T();


            

            if (pic != null && pic.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(pic.FileName);
                var fileexten = Path.GetExtension(pic.FileName);

                media.MediaName = "-" + col["MovieID"] + fileexten;
                media.MovieID = Convert.ToInt32(col["MovieID"]);
                db.Media_T.Add(media);
                db.SaveChanges();
                var path = Path.Combine(Server.MapPath("~/moviemedia"), "poster-" + media.MediaID + media.MediaName);
                pic.SaveAs(path);
            }
             return RedirectToAction("../Movies/Details/" + media.MovieID);

            
        }

        public ActionResult addShowtimes(FormCollection col)
        {
            if (Convert.ToString(Session["Loggedin"]) != "YES")
            {
                return RedirectToAction("Login", "Default");
            }

            db.Database.ExecuteSqlCommand("delete from Showtime_T where movieid=" + col["MovieID"]);

            string[] xdd = Convert.ToString(col["xxx"]).Split(',');
            

            ShowTime_T showtime;
           
            foreach(var y in xdd)
            {
                if (Convert.ToString(col["showtime-" + y]) != "")
                {
                    showtime = new ShowTime_T();
                    showtime.MovieID = Convert.ToInt32(col["MovieID"]);
                    showtime.CinemaID = Convert.ToInt32(col["cinema-" + y]);
                    showtime.ShowTime = Convert.ToString(col["showtime-" + y]).Replace("+", ",");
                    db.ShowTime_T.Add(showtime);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("../Movies/Details/" + col["MovieID"]);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.LanguageID = new SelectList(db.Language_T, "LanguageID", "Language");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieID,Name,Rated,Type,ReleasedOn,Stars,Directors,Writers,Details,LanguageID,YoutubeURL")] Movies_T movies_T)
        {
            if (ModelState.IsValid)
            {
                db.Movies_T.Add(movies_T);
                db.SaveChanges();
                return RedirectToAction("../Movies/Details/" + movies_T.MovieID);
                //return RedirectToAction("Index");
            }

            ViewBag.LanguageID = new SelectList(db.Language_T, "LanguageID", "Language", movies_T.LanguageID);
            return View(movies_T);
        }

        // GET: Movies/Edit/5
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
            Movies_T movies_T = db.Movies_T.Find(id);
            if (movies_T == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageID = new SelectList(db.Language_T, "LanguageID", "Language", movies_T.LanguageID);
            return View(movies_T);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieID,Name,Rated,Type,ReleasedOn,Stars,Directors,Writers,Details,LanguageID,YoutubeURL")] Movies_T movies_T)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movies_T).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageID = new SelectList(db.Language_T, "LanguageID", "Language", movies_T.LanguageID);
            return View(movies_T);
        }

        // GET: Movies/Delete/5
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
            Movies_T movies_T = db.Movies_T.Find(id);
            if (movies_T == null)
            {
                return HttpNotFound();
            }
            return View(movies_T);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movies_T movies_T = db.Movies_T.Find(id);
            db.Movies_T.Remove(movies_T);
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
