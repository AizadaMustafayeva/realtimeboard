using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dashboard.ViewModels
{
    public class PersonVM
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mail")]
        public string mail { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
}