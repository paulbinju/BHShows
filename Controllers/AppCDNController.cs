using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Globalization;
using BHShows.Models;
 

namespace BHShows.Controllers
{
    public class AppCDNController : Controller
    {
        public ActionResult Movies()
        {

            using (CinemazEntities db = new CinemazEntities())
            {


                List<Moviesx> movies_T = db.Database.SqlQuery<Moviesx>(@"select m.MovieID,l.Language,m.Name,m.Rated,m.Type,m.Stars,m.Directors,m.Writers,m.Details,m.ReleasedOn,m.YoutubeURL from Movies_T m
                join Language_T l on m.LanguageID=l.LanguageID
                where m.MovieID in (select distinct movieid from ShowTime_T s)").ToList();

                db.Dispose();
                SqlConnection.ClearAllPools();

                return new JsonpResult
                {
                    Data = movies_T,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

    }

    public class JsonpResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;
            string jsoncallback = (context.RouteData.Values["jsoncallback"] as string) ?? request["jsoncallback"];
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(base.ContentType))
                {
                    base.ContentType = "application/x-javascript";
                }
                response.Write(string.Format("{0}((", jsoncallback));
            }
            base.ExecuteResult(context);
            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write("))");
            }
        }
    }
}