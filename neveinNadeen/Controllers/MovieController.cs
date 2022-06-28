using neveinNadeen.Models;
using neveinNadeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace neveinNadeen.Controllers
{
    public class MovieController : Controller
    {
        
        public IMDb_Entities db = new IMDb_Entities();

        public ActionResult Index(string searching)
        {
            return View(db.Movies.Where(x => x.Name.Contains(searching)).ToList());
        }
        public ActionResult Details(int id)
        {
            ActorMovieDirector a = new ActorMovieDirector
            {
                Movie = db.Movies.SqlQuery("select * from Movie where Id = " + id).ToList(),
                Actor = db.Actors.SqlQuery("select * from Actor,ActorMovie where ActorMovie.M_Id =" + id
            + "and Actor.Id=ActorMovie.A_Id ").ToList(),
                Director = db.Directors.SqlQuery($"Select * from Director,Movie where Movie.D_Id = Director.Id and Movie.Id ={id}").ToList(),
            };
            return View(a);
        }
        int entered = 0;

        [HttpGet]
        public ActionResult MovieLikes(int id)
        {
            if (entered != 1)
            {

                var movie = db.Movies.Single(c => c.Id == id);

                /* var rate = db.Rates.SqlQuery("select * from Rate, Movie,user where movie.id = " + id
                 + "and rate.m_id=Movie.id and rate.u_id = user.id ").ToList();
                 */
                entered = 1;
                movie.likes++;

                db.SaveChanges();

                return Json(new { result = movie.likes }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public ActionResult MovieDislikes(int id)
        {
            if (entered != 1)
            {

                var movie = db.Movies.Single(c => c.Id == id);
                entered = 1;

                movie.Dislikes++;
                db.SaveChanges();
                return Json(new { result = movie.Dislikes }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);

            }

        }

        /*
                 int entered = 0;

                [HttpGet]
                public ActionResult MovieLikes(int movieId)
                {
                    var movie = db.Movies.FirstOrDefault(m => m.Id == movieId);
                    LikeVM likeVM = new LikeVM
                    {
                        movie=movie,

                    };
                    return Json(new { result = movie.likes }, JsonRequestBehavior.AllowGet);
                    //if (entered != 1)
                    //{

                    //    var movie = db.Movies.Single(c => c.Id == id);

                    //    /* var rate = db.Rates.SqlQuery("select * from Rate, Movie,user where movie.id = " + id
                    //     + "and rate.m_id=Movie.id and rate.u_id = user.id ").ToList();
                    //     */
        //    entered = 1;
        //    movie.likes++;

        //    db.SaveChanges();

        //    return Json(new { result = movie.likes }, JsonRequestBehavior.AllowGet);
        //}
        //else
        //{
        //    return Json(new { result = "null" }, JsonRequestBehavior.AllowGet);

        //}*/
        /*   
       }


       [HttpGet]
       public ActionResult MovieDislikes(int id)
       {
           if (entered != 1)
           {

               var movie = db.Movies.Single(c => c.Id == id);
               entered = 1;

               movie.Dislikes++;
               db.SaveChanges();
               return Json(new { result = movie.Dislikes }, JsonRequestBehavior.AllowGet);
           }
           else
           {
               return Json(new { result = "null" }, JsonRequestBehavior.AllowGet);

           }

       }

       [HttpPost]
       public ActionResult MovieLikes(LikeVM likeVM, int movieId)
       {
           Rate rate = null;
           int userId = (int)Session["UserID"];
           var user = db.IMDb_User.FirstOrDefault(u => u.Id == userId);
           var movie = db.Movies.FirstOrDefault(m => m.Id == movieId);

           //if like
           rate = new Rate
           {
               M_Id = movieId,
               IMDb_User = user,
               Like = 1,
               DisLike = 0
           };
           likeVM = new LikeVM
           {
               user = user,
               movie = movie,


           };

                   movie.likes++;
                   db.Rates.Add(rate);
                   db.SaveChanges();
                   //result = true;  


           return RedirectToAction("Details", "MovieLikes", new { movieId = movieId });

       }
       */
    }
}
