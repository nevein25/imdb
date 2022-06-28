using neveinNadeen.Models;
using neveinNadeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace neveinNadeen.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment

        IMDb_Entities db = new IMDb_Entities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetComments(int movieId)
        {
            IEnumerable<CommentsVM> comments = db.Comments.Where(c => c.Movie.Id == movieId)
                                     .Select(c => new CommentsVM
                                     {
                                         Id= c.Id,
                                         Date = c.Date.Value,
                                         Conntent = c.Conntent,
                                         Users = new UserVM
                                         {
                                             UserID = c.IMDb_User.Id,
                                             Fname = c.IMDb_User.Fname,
                                             Image = c.IMDb_User.Image
                                         }
                                     }).ToList();

            return PartialView("~/Views/Shared/_MovieComment.cshtml", comments);
        }


        [HttpPost]
        public ActionResult AddComment(CommentsVM comment, int movieId)
        {
            //bool result = false;  
            Comment commentEntity = null;
            int userId = (int)Session["UserID"];

            var user = db.IMDb_User.FirstOrDefault(u => u.Id == userId);
            var movie = db.Movies.FirstOrDefault(m => m.Id == movieId);

            if (comment != null)
            {
                if (comment.Conntent != null)
                {
                    commentEntity = new Comment 
                    {
                        Conntent = comment.Conntent,
                        Date = comment.Date,
                    };


                }

                if (user != null && movie != null)
                {
                    movie.Comments.Add(commentEntity);
                    user.Comments.Add(commentEntity);

                    db.SaveChanges();
                    //result = true;  
                }
            }
            return RedirectToAction("GetComments", "Comment", new { movieId = movieId });
        }
    }
}