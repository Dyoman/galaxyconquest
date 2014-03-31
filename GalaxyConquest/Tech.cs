using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace GalaxyConquest
{
    public class Tech
    {

        public List<string> TechList = new List<string>();
        public int tech_p;
        public List<int> tech_id = new List<int>();
        public List<int> TechLink = new List<int>();

        public void init()
        {

            StreamReader tech_str = new StreamReader("Tech.txt");
            int counter = 0;
            string line;

            while ((line = tech_str.ReadLine()) != null)
            {
                TechList.Add(line);
                counter++;
            }
            tech_str.Close();



            }
        }

    }

}
