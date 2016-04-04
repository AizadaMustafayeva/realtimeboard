using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class BoardItemVM
    {
        public int id { get; set; }

        public string title { get; set; }

        public double positionX { get; set; }

        public double positionY { get; set; }

        public string value { get; set; }

        public int zIndex { get; set; }

        public PersonVM owner { get; set; }

        public string date { get; set; }

        public int typeId { get; set; }

    }
}