//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace neveinNadeen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FavActor
    {
        public int Id { get; set; }
        public Nullable<int> U_ID { get; set; }
        public Nullable<int> A_Id { get; set; }
    
        public virtual Actor Actor { get; set; }
        public virtual IMDb_User IMDb_User { get; set; }
    }
}
