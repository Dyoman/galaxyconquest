using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace GalaxyConquest
{
    [Serializable()]
    public class Teches
    {
        [XmlElement("Tier")]
        public List<List<List<Pair>>> tiers = new List<List<List<Pair>>>();
    }

    public class Pair
    {
        public string subtech { get; set; }
        public string description { get; set; }
    }
}
