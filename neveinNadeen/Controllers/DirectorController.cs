using neveinNadeen.Models;
using neveinNadeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace neveinNadeen.Controllers
{
    public class DirectorController : Controller
    {
        // GET: Director
        public IMDb_Entities db = new IMDb_Entities();

        public ActionResult Index(string searching)
        {

            return View(db.Directors.Where(x => x.Fname.Contains(searching)).ToList());

        }
        //public ActionResult Details(int id)
        //{
        //    var directors = db.Directors.SingleOrDefault(a => a.Id == id);
        //    return View(directors);
        //}

        public ActionResult Details(int id)        {            ActorMovieDirector a = new ActorMovieDirector            {                Director = db.Directors.SqlQuery("select * from director where id = " + id).ToList(),                Movie = db.Movies.SqlQuery($"select * from movie WHERE movie.D_id={id} ").ToList(),            };            return View(a);        }

    }
}