using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperTestCristhoferMoya.Models
{
    public class City
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public int fips { get; set; }
        public decimal lat { get; set; }
        [JsonProperty("long")]
        public decimal longitud { get; set; }
        public int confirmed { get; set; }
        public int  deaths { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public DateTime last_update { get; set; }
    }
}