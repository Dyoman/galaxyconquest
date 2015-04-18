using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using GalaxyConquest.Tactics;
using System.Windows.Forms;

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
        public int RP { get; set; }
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

        /// <summary>
        /// Тут можно проверить, добавлять ли здание/броню, e.t.c.
        /// Form1.SelfRef.tt.tierClicked - доступ к только что выученному поколению технологий (столбец)
        /// Form1.SelfRef.tt.techLineClicked - доступ к только что выученной ветке технологий (строка)
        /// Form1.SelfRef.tt.subtechClicked - доступ к только что выученной подтехнологии (один и вариантов в ветке)
        /// teches.tiers[tierClicked][techLineClicked][subtechClicked]
        /// </summary>
        public static void CheckTechInnovaions()
        {
            MessageBox.Show("TODO");

            /*
            //----------------------------------Adds Armor-----------------------------------
            Armor armor = new ArmorNone();
            if (Form1.SelfRef.techTreeForm.techLineClicked == 1)
            {
                switch (Form1.SelfRef.techTreeForm.tierClicked)
                {
                    case 1:
                        if (Form1.SelfRef.techTreeForm.subtechClicked == 0)//Titan
                            armor = new ArmorTitan();
                        break;

                    case 2:
                        if (Form1.SelfRef.techTreeForm.subtechClicked == 0)//Molibden
                            armor = new ArmorMolibden();
                        break;
                    case 3:
                        if (Form1.SelfRef.techTreeForm.subtechClicked == 0)//Nanocom
                            armor = new ArmorNanocom();
                        break;
                    default:
                        MessageBox.Show("Error occured with tech data" +
                            " tier:" + Form1.SelfRef.techTreeForm.tierClicked +
                            " techLine:" + Form1.SelfRef.techTreeForm.techLineClicked +
                            " subtech:" + Form1.SelfRef.techTreeForm.subtechClicked);
                        break;
                }
            }

            for (int i = 0; i < Form1.Game.Player.fleets.Count; i++)
            {
                for (int j = 0; j < Form1.Game.Player.fleets[i].ships.Count; j++)
                {
                    Form1.Game.Player.fleets[i].ships[j].InstallArmor(armor);
                }
            }
            */

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
