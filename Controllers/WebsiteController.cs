using BHShows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHShows.Controllers
{
    public class WebsiteController : Controller
    {
        private CinemazEntities db = new CinemazEntities();
        // GET: Website
        public ActionResult Index()
        {
            ViewBag.Title = "Movie trailers, Cinema theaters and showtimes in Bahrain.";
            ViewBag.Description = "Watch your favourite movie trailer, find out cinema theater and showtimes in Bahrain.";
            ViewBag.Keywords = "Bahrain,Movie,Cinema,Theater,Malayalam,Hindi,English,Tamil";


            //List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select top 10 m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
            //join Language_T l on m.LanguageID=l.LanguageID order by m.movieid desc").ToList();

            List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select  top 10 m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
            join Language_T l on m.LanguageID=l.LanguageID
            where m.MovieID in (select distinct movieid from ShowTime_T s)  order by m.movieid desc").ToList();


            List<Moviesx> movies_T2 = new List<Moviesx>();
            Moviesx movies2;

            foreach (Moviesx moviest in movies_T)
            {
                movies2 = new Moviesx();
                movies2.Name = moviest.Name;
                movies2.cleanURL = cleanURL(moviest.Name);
                movies2.MovieID = moviest.MovieID;
                movies2.Language = moviest.Language;
                movies2.Rated = moviest.Rated;
                movies2.Type = moviest.Type;
                movies2.Stars = moviest.Stars;
                movies2.Directors = moviest.Directors;
                movies2.Writers = moviest.Writers;
                movies2.ReleasedOn = moviest.ReleasedOn;
                movies2.YoutubeURL = moviest.YoutubeURL;
                movies2.Details = moviest.Details;
                movies_T2.Add(movies2);
            }


            ViewBag.Movies = movies_T2.ToList();


            List<Media_T> media_T = db.Database.SqlQuery<Media_T>(@"select top 20 * from Media_T where mediaid in (select min(mediaid) from [dbo].[Media_T] group by MovieID) order by mediaid desc").ToList();
            ViewBag.Media = media_T.ToList();

            ViewBag.Cinemas = db.Cinema_T.Where(x => x.CountryID == 1).ToList();

           // List<Moviesx> allmovies = db.Database.SqlQuery<Moviesx>(@"select * from Movies_T m join Media_T i on m.MovieID= i.MovieID where i.MediaID in (select min(mediaid) from Media_T group by MovieID)").ToList();

          //  ViewBag.allmovies = allmovies.ToList();

            return View();
        }

        public ActionResult Movie(int id, string moviename)
        {
            List<Moviesx> moviex = db.Database.SqlQuery<Moviesx>(@"select m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
                                        join Language_T l on m.LanguageID=l.LanguageID where m.MovieID=" + id + " order by m.movieid desc").ToList();
            List<Media_T> media = db.Media_T.Where(x => x.MovieID == id).ToList();

            ViewBag.showtimes = (from st in db.ShowTime_T
                                 join c in db.Cinema_T on st.CinemaID equals c.CinemaID
                                 where st.MovieID == id
                                 select new MovieCinemaShowtime { ShowTime = st.ShowTime, CinemaID = c.CinemaID, Name = c.Name }
                            ).ToList();

            ViewBag.Movie = moviex;
            ViewBag.Media = media;


            List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select top 10 m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
            join Language_T l on m.LanguageID=l.LanguageID order by m.movieid desc").ToList();

            List<Moviesx> movies_T2 = new List<Moviesx>();
            Moviesx movies2;

            foreach (Moviesx moviest in movies_T)
            {
                movies2 = new Moviesx();
                movies2.Name = moviest.Name;
                movies2.cleanURL = cleanURL(moviest.Name);
                movies2.MovieID = moviest.MovieID;
                movies2.Language = moviest.Language;
                movies2.Rated = moviest.Rated;
                movies2.Type = moviest.Type;
                movies2.Stars = moviest.Stars;
                movies2.Directors = moviest.Directors;
                movies2.Writers = moviest.Writers;
                movies2.ReleasedOn = moviest.ReleasedOn;
                movies2.YoutubeURL = moviest.YoutubeURL;
                movies2.Details = moviest.Details;
                movies_T2.Add(movies2);
            }


            ViewBag.Movies = movies_T2.ToList();


            List<Media_T> media_T = db.Database.SqlQuery<Media_T>(@"select top 20 * from Media_T where mediaid in (select min(mediaid) from [dbo].[Media_T] group by MovieID) order by mediaid desc").ToList();
            ViewBag.Media2 = media_T.ToList();


            ViewBag.showtimes = (from st in db.ShowTime_T
                                 join c in db.Cinema_T on st.CinemaID equals c.CinemaID
                                 where st.MovieID == id
                                 select new MovieCinemaShowtime { ShowTime = st.ShowTime, CinemaID = c.CinemaID, Name = c.Name }
                                   ).ToList();




            return View();
        }

        public ActionResult MoviesThisWeek()
        {

            return View();
        }

        public string cleanURL(string rawurl)
        {
            string cleanedURL = rawurl.Replace(" ", "-").Replace("&", "").Replace(".", "").Replace("/", "").Replace("*", "").Replace(":", "");

            return cleanedURL;
        }
    }
}