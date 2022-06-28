using neveinNadeen.Models;
using neveinNadeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace neveinNadeen.Controllers
{
    public class ActorController : Controller
    {
        // GET: Actor
        public IMDb_Entities db = new IMDb_Entities();
        public ActionResult Index(string searching)
        {
            return View(db.Actors.Where(x => x.Fname.Contains(searching)).ToList());
        }
        //public ActionResult Details(int id)
        //{
        //    var actors = db.Actors.SingleOrDefault(a => a.Id == id);
        //    return View(actors);
        //}
        public ActionResult Details(int id)        {            ActorMovieDirector a = new ActorMovieDirector            {                Actor = db.Actors.SqlQuery("select * from actor where id = " + id).ToList(),                Movie = db.Movies.SqlQuery($"select * from movie,ActorMovie WHERE movie.id = ActorMovie.m_id AND {id}=ActorMovie.a_id ").ToList(),            };            return View(a);        }

    }
}