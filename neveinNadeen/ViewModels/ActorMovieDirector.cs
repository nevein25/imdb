using neveinNadeen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace neveinNadeen.ViewModels
{
    public class ActorMovieDirector
    {
        public List<Movie> Movie { get; set; }
        public List<Actor> Actor { get; set; }
        public List<ActorMovie> ActorMovie { get; set; }
        public List<Director> Director { get; set; }
    }
}