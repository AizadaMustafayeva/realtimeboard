using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class DocCommentVM
    {
        public int id { get; set; }

        public PersonVM owner { get; set; }

        public string date { get; set; }

        public string time { get; set; }

        public string message { get; set; }

        public bool self { get; set; }

    }
}