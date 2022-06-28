using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using neveinNadeen.Models;
namespace neveinNadeen.ViewModels
{
    public class LikeVM
    {
        public Movie movie { get; set; }
        public IMDb_User user { get; set; }
        public Rate rate { get; set; }
    }
}