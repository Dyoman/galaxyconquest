using System;
using System.Collections.Generic;
using System.IO;
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

    public static class Tech
    {
        public static int learning_tech_time = 4;

        public static Teches teches = new Teches();
        public static void Inint()
        {
            Deserialize(teches.tiers, "teches.xml");
            //LoadTeches.SerializeObject(teches.tiers, ".xml");
        }

        /// <summary>
        /// Тут можно проверить, добавлять ли здание/броню, e.t.c.
        /// Form1.SelfRef.tt.tierClicked - доступ к только что выученному поколению технологий (столбец)
        /// Form1.SelfRef.tt.techLineClicked - доступ к только что выученной ветке технологий (строка)
        /// Form1.SelfRef.tt.subtechClicked - доступ к только что выученной подтехнологии (один и вариантов в ветке)
        /// teches.tiers[tierClicked][techLineClicked][subtechClicked]
        /// </summary>
        public static void CheckTechInnovaions()
        {
            
        }

            public static void SerializeObject(this List<List<List<Pair>>> list, string fileName)
            {
                var serializer = new XmlSerializer(typeof(List<List<List<Pair>>>));
                using (var stream = File.OpenWrite(fileName))
                {
                    serializer.Serialize(stream, list);
                }
            }

            public static void Deserialize(this List<List<List<Pair>>> list, string fileName)
            {
                var serializer = new XmlSerializer(typeof(List<List<List<Pair>>>));
                using (var stream = File.OpenRead(fileName))
                {
                    var other = (List<List<List<Pair>>>)(serializer.Deserialize(stream));
                    list.Clear();
                    list.AddRange(other);
                }
            }
    }
}
