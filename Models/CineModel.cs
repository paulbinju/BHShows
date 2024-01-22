using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHShows.Models
{
    public class CineModel
    {
    }

    public class MovieCinemaShowtime
    {
        public int? MovieID { get; set; }
        public int? ShowtimeID { get; set; }
        public string ShowTime { get; set; }
        public int? CinemaID { get; set; }
        public string Name { get; set; }


    }
    public class Moviesx
    {
        public int MovieID { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string cleanURL { get; set; }
        public string Rated { get; set; }
        public string Type { get; set; }
        public string Stars { get; set; }
        public string Directors { get; set; }
        public string Writers { get; set; }
        public string Details { get; set; }
        public string YoutubeURL { get; set; }
        public DateTime ReleasedOn { get; set; }
        public string MediaName { get; set; }
    }
}