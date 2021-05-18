using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DeveloperTestCristhoferMoya.Models
{
    public class TopRegionWorld
    {
        public int rank { get; set; }
        public string name { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        [XmlIgnore]
        public DateTime last_update { get; set; }
       

        public TopRegionWorld() { }
    }
}