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
    /// <summary>
    /// Структура описывает параметры технологий.
    /// </summary>
    public struct TechData
    {
        /// <summary>
        /// Уровень технологии.
        /// </summary>
        public int Tier;
        /// <summary>
        /// Ветка технологии.
        /// </summary>
        public int Line;
        /// <summary>
        /// Номер технологии.
        /// </summary>
        public int Subtech;

        public TechData(int tier, int line, int subtech)
        {
            Tier = tier;
            Line = line;
            Subtech = subtech;
        }
        /// <summary>
        /// Возвращает true, если структура не содержит данных о технологии.
        /// </summary>
        public bool isNone()
        {
            return (Tier == 0) && (Line == 0) && (Subtech == 0);
        }
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
