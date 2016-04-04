using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class BoardVM
    {
        public int id { get; set; }

        public string title { get; set; }

        public PersonVM owner { get; set; }

        public List<BoardItemVM> items { get; set; }

    }
}