using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class DocVM
    {
        public int id { get; set; }

        public string title { get; set; }

        public string date { get; set; }

        public PersonVM owner { get; set; }

        public GroupVM group { get; set; }

        public string value { get; set; }

        public DocTypeVM type { get; set; }

    }
}