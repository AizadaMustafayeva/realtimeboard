//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dashboard.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class DocComment
    {
        public int Id { get; set; }
        public string CommentOwner { get; set; }
        public System.DateTime Date { get; set; }
        public string Message { get; set; }
        public int DocId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Doc Doc { get; set; }
    }
}
