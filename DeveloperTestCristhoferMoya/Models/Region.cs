using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeveloperTestCristhoferMoya.Models
{
    public class RegionH
    {
        public List<Region> data { get; set; }
    }
    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }

        public Region()
        {
        }

        public Region(string iso, string name)
        {
            this.iso = iso;
            this.name = name;
        }
    }
}