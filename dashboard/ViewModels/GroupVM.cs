using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class GroupVM
    {
        public int id { get; set; }

        public string title { get; set; }

        public PersonVM Owner { get; set; }

        public List<PersonVM> Users { get; set; }

        // need add docs

        //group.Id
        //group.AspNetUsers
        //group.GroupDocs
        //group.GroupUsers
        //group.Title
    }
}