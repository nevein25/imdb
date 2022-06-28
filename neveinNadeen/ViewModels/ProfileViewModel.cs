using neveinNadeen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace neveinNadeen.ViewModels
{
    public class ProfileViewModel
    {
        //##########

            public IMDb_User IMDb_User { get; set; }

            public IEnumerable<Movie> Movie { get; set; }
            public IEnumerable<Actor> Actor { get; set; }
            public IEnumerable<ActorMovie> ActorMovie { get; set; }
            public IEnumerable<Director> Director { get; set; }

        }
 }

