using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using neveinNadeen.Models;
using neveinNadeen.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace neveinNadeen.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        IMDb_Entities db = new IMDb_Entities();

        public ActionResult Index()
        {
            return View();
        }

        //##########################SignUp#########################
        [HttpGet]
        public ActionResult NewUser()
        {
            Auth signup = new Auth();

            return View(signup);
        }
        
        [HttpPost]
        public ActionResult NewUser(Auth signup, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                    file.SaveAs(path);
                    signup.IMDb_User.Image = pic;
                    
                }
                db.IMDb_User.Add(signup.IMDb_User);
                db.SaveChanges();


                return RedirectToAction("Login", "User");
            }


            return View("NewUser", signup.IMDb_User); //TRY:  return View("NewUser");

        }
        /*
        [HttpGet]

        public ActionResult Register()
        {
            Auth model = new Auth();
            return View(model);
        }
        [HttpPost]

        public ActionResult Register(Auth model)
        {
            IMDb_User iMDb_User = null;

            if (!ModelState.IsValid)
            {
                return View("Register", iMDb_User);
            }
                iMDb_User = new IMDb_User
                {
                    Fname = model.IMDb_User.Fname,
                    Lname = model.IMDb_User.Lname,
                    Age = model.IMDb_User.Age,
                    Password = model.IMDb_User.Password,
                    Email = model.IMDb_User.Email
                };
                db.IMDb_User.Add(iMDb_User);
                db.SaveChanges();

            
            return RedirectToAction("Index", "Movie");
        }
        */
        // If we got this far, something failed, redisplay form

        //##########################Log in#########################

        [HttpGet]
        public ActionResult Login()
        {
            Auth login = new Auth();

            return View(login);
        }
        [HttpPost]
        public ActionResult Login(Auth login)
        {
            if (!ModelState.IsValid)
            {
                return View("NewUser", "User", login.IMDb_User);
            }
            else
            {
                var data = db.IMDb_User.Where(a => a.Email.Equals(login.IMDb_User.Email) && a.Password.Equals(login.IMDb_User.Password)).FirstOrDefault();

                if (data != null)
                {
                    if (data.Role == 2)
                    {
                        Session["UserId"] = data.Id;
                        Session["Email"] = data.Email.ToString();

                        return RedirectToAction("Index", "Movie"); 

                    }
                    else if (data.Role == 1)
                    {


                    }
                }
                else
                {
                }
            }
            return RedirectToAction("Login", login);
        }

        public ActionResult Details()        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login","User");
            }            int userId = (int)Session["UserID"];

            //var user1 = db.IMDb_User.FirstOrDefault(u => u.Id == userId);            var user = db.IMDb_User.Include("Movie").SingleOrDefault(u => u.Id == userId);            return View(user);        }
        public ActionResult l()        {
            return RedirectToAction("Login", "User");
        }


        //edit user profile with lists

        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            var user = db.IMDb_User.Single(u => u.Id == id);
            var favMovie = db.Movies.ToList();
            var favActor = db.Actors.ToList();
            var favDirector = db.Directors.ToList();

            ProfileViewModel model = new ProfileViewModel
            {
                IMDb_User = user,
                Movie = favMovie,
                Actor = favActor,
                Director = favDirector,
                
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult UserProfile(ProfileViewModel model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                var favMovie = db.Movies.ToList();
                var favActor = db.Actors.ToList();
                var favDirector = db.Directors.ToList();
                model.Movie = favMovie;
                model.Actor = favActor;
                model.Director = favDirector;
                return View("UserProfile", "User", model);
            }
            var userDb = db.IMDb_User.Single(u => u.Id == model.IMDb_User.Id);
            if(file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/profiles/"), pic);

                file.SaveAs(path);
                model.IMDb_User.Image = pic;
            }


            userDb.Fname = model.IMDb_User.Fname;
            userDb.Lname = model.IMDb_User.Lname;
            userDb.Age = model.IMDb_User.Age;
            userDb.Email = model.IMDb_User.Email;
            userDb.Password = model.IMDb_User.Password;
            userDb.FavMovie = model.IMDb_User.FavMovie;
            userDb.FavActor = model.IMDb_User.FavActor;
            userDb.FavDirector = model.IMDb_User.FavDirector;
            userDb.Image = model.IMDb_User.Image;
            db.SaveChanges();
            return RedirectToAction("Index", "Movie");
        }
        //end of edit profile



        public PartialViewResult GetComments(int movieId)
        {
            IEnumerable<CommentsVM> comments = db.Comments.Where(c => c.Movie.Id == movieId)
                                     .Select(c => new CommentsVM
                                     {
                                         Id = c.Id,
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

        //#########################################Adding favfourit###############################################
        [HttpGet]
        public ActionResult AddFav()
        {

            return View();
        }



    }
}