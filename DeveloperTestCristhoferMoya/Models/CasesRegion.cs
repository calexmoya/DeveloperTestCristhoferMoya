using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DeveloperTestCristhoferMoya.Models
{
    public class CasesRegionH
    {
        public List<CasesRegion> data { get; set; }
    }
    public class CasesRegion
    {
        public int rank { get; set; }
        [XmlIgnore]
        public DateTime date { get; set; }
        public int confirmed { get; set; }
    
        public int deaths { get; set; }
        [XmlIgnore]
        public int recovered { get; set; }
        [XmlIgnore]
        public int confirmed_diff { get; set; }
        [XmlIgnore]
        public int deaths_diff { get; set; }
        [XmlIgnore]
        public int recovered_diff { get; set; }
        [XmlIgnore]
        public DateTime last_update { get; set; }
        [XmlIgnore]
        public int active { get; set; }
        [XmlIgnore]
        
        public int active_diff { get; set; }
        [XmlIgnore]
        public decimal fatality_rate { get; set; }
        [XmlIgnore]
        public Province region { get; set; }
        [XmlElement("province")]
        public string provinceName { get; set; }
        [XmlElement("region")]
        public string regionName { get; set; }
    }
}