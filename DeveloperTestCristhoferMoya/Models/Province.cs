using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperTestCristhoferMoya.Models
{
    public class Province
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public decimal lat { get; set; }

        [JsonProperty("long")]
        public decimal longitude { get; set; }
        public List<City> cities { get; set; }
    }
}