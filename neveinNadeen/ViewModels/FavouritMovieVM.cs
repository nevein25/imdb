using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using neveinNadeen.Models;
namespace neveinNadeen.ViewModels
{
    public class FavouritMovieVM
    {
        public int FM_Id;
        public int FM_U_Id;
        public int FM_M_Id;

    }
    public class UserFavVM
    {
        public int UserID { get; set; }
        public string Fname { get; set; }
        public string Image { get; set; }
    }
    public class MoviesFavVM
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        
    }

}