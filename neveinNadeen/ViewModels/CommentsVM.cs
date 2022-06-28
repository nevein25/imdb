using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace neveinNadeen.ViewModels
{

    public class MoviesVM
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        public DateTime? MovieDate { get; set; }
    }

    public class CommentsVM
    {
        public int Id{ get; set; }
        public string Conntent { get; set; }
        public DateTime? Date { get; set; }
        public MoviesVM Movies { get; set; }
        public UserVM Users { get; set; }
    }

    public class UserVM
    {
        public int UserID { get; set; }
        public string Fname { get; set; }
        public string Image{ get; set; }
    }

}