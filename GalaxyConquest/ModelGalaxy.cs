using GalaxyConquest.StarSystems;
using GalaxyConquest.Tactics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace GalaxyConquest
{
    [Serializable]
    public enum GalaxyType
    {
        Spiral,
        Eliptical,
        Sphere,
        Irregular
    };

    [Serializable]
    public class ModelGalaxy : SpaceObject
    {
        public double Time;
        public List<StarSystem> stars; //звездные системы
        public List<StarWarp> lanes;   //гиперпереходы

        public List<Fleet> neutrals;
        GalaxyType galaxyType;

        public Player player;

        public ModelGalaxy()
        {
            stars = new List<StarSystem>();
            lanes = new List<StarWarp>();
            neutrals = new List<Fleet>();
        }

        /// <summary>
        /// Генерирует новую галактику
        /// </summary>
        /// <param name="galaxyname">Название галактики</param>
        /// <param name="playerName">Имя игрока</param>
        /// <param name="type">Тип галактики</param>
        /// <param name="size">Размер галактики</param>
        /// <param name="starCount">Количество звездных систем</param>
        /// <param name="generateRandomEvent">Возможность появления идеальной планеты</param>
        public void GenerateNew(string galaxyname, string playerName, GalaxyType type, int size, int starCount, bool generateRandomEvent)
        {
            Time = 3000.0;
            name = galaxyname;
            player.name = playerName;
            galaxyType = type;

            switch (type)
            {
                case GalaxyType.Spiral:
                    generate_spiral_galaxy(0.0, size, starCount);
                    generate_spiral_galaxy(Math.PI * 20, size, starCount);
                    break;
                case GalaxyType.Eliptical:
                    generate_elliptical_galaxy(size, starCount);
                    break;
                case GalaxyType.Irregular:
                    generate_irregular_galaxy(0, size, starCount);
                    generate_irregular_galaxy(Math.PI * 20, size, starCount);
                    break;
                case GalaxyType.Sphere:
                    generate_sphere_galaxy(size, starCount);
                    break;
            }

            Move(Time);

            Random rand = new Random((int)DateTime.Now.Ticks);

            if (generateRandomEvent)
            {
                generate_random_events();
            }

            //-----generate player fleets-------
            int fleetsCount = rand.Next(1, 3);
            for (int i = 0; i < fleetsCount; i++)
            {
                StarSystem s = stars[rand.Next(0, stars.Count - 1)];
                while (player.player_stars.Contains(s))
                {
                    s = stars[rand.Next(0, stars.Count - 1)];
                }
                player.player_stars.Add(s);

                for (int j = 0; j < s.PLN.Count; j++)
                    Player.player_planets.Add(s.PLN[j]);

                Fleet fl = generateFleet(rand.Next(2, 5), 1);
                fl.s1 = s;
                fl.name = "Флот " + (i + 1) + " <" + player.name + ">";
                fl.x = fl.s1.x;
                fl.y = fl.s1.y;
                fl.z = fl.s1.z;
                player.player_fleets.Add(fl);
            }

            //-----generate neutral fleets-------
            fleetsCount = rand.Next(1, 3);
            for (int i = 0; i < fleetsCount; i++)
            {
                StarSystem sr = stars[rand.Next(0, stars.Count - 1)];
                while (player.player_stars.Contains(sr))
                {
                    sr = stars[rand.Next(0, stars.Count - 1)];
                }
                Fleet flneutrals = generateFleet(rand.Next(2, 4), 2);
                flneutrals.s1 = sr;
                flneutrals.name = "Нейтральный флот";
                flneutrals.x = flneutrals.s1.x;
                flneutrals.y = flneutrals.s1.y;
                flneutrals.z = flneutrals.s1.z;
                neutrals.Add(flneutrals);
            }
        }

        Fleet generateFleet(int size, int player)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            Fleet fleet = new Fleet();

            for (int i = 0; i < size; i++)
            {
                int ship_type = (rand.Next(0, 100)) % 2;
                int weapon_type = rand.Next(0, 2);

                Weapon weapon = null;
                Ship ship = null;
                switch (weapon_type)
                {
                    case 0: weapon = new wpnLightLaser(); break;
                    //case 1: weapon = new WpnLightIon(); break;
                    case 1: weapon = new WpnHeavyLaser(); break;
                }

                switch (ship_type)
                {
                    case 0: ship = new ShipScout(player, weapon); break;
                    case 1: ship = new ShipAssaulter(player, weapon); break;
                }

                ship.player = player;
                fleet.ships.Add(ship);
            }

            return fleet;
        }

        void generate_random_events()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            StarSystem nova = stars[rand.Next(stars.Count)];

            nova.name = "Super nova";     //name for new star
            nova.type = 8;                //type for "super nova"
            nova.br = Form1.SuperWhiteBrush;    //brush for "super nova"

            generatePlanets(nova);
        }

        void generatePlanets(StarSystem s)
        {
            int sizemin = 10;
            int sizemax = 40;
            int popmin = 0;
            int popmax = 10;
            int mineralmin = 0;
            int mineralmax = 35;
            int colormin = 0;
            int colormax = 255;
            int dist = 50;
            float speed = 1f;

            if (s.PLN.Count > 0)
            {
                s.PLN.Clear();
                //throw new Exception("Планеты уже есть");
            }

            Random r = new Random(DateTime.Now.Millisecond);
            PLANET pln = new PLANET();

            int planets_count = s.type + r.Next(1, 2);

            pln.CENTER = new PointF(190f, 190f);
            pln.DISTANCE = 0;
            pln.SPEED = 0;
            pln.CLR = s.br.Color;
            pln.SIZE = 25;
            pln.NAME = s.name;
            pln.POPULATIONMAX = 0;
            pln.POPULATION = 0;
            pln.MINERALS = 0;

            pln.Move(Time);

            s.PLN.Add(pln);

            double p = 1;
            //p = p + (p / 2.75) - (p / (r.NextDouble() * 20));
            for (int i = 1; i <= planets_count; i++)
            {
                pln = new PLANET();

                pln.CENTER = new PointF(s.PLN[0].GetPoint().X, s.PLN[0].GetPoint().Y);
                pln.DISTANCE = dist;
                pln.SPEED = speed;
                pln.CLR = Color.FromArgb((r.Next(colormin, colormax)), (r.Next(colormin, colormax)), (r.Next(colormin, colormax)));
                pln.SIZE = r.Next(sizemin, sizemax);

                pln.NAME = s.name + " " + i.ToString();

                pln.POPULATION = pln.Inc(p, r.NextDouble());
                pln.POPULATIONMAX = r.Next(popmin, popmax);
                pln.MINERALS = r.Next(mineralmin, mineralmax);
                pln.PROFIT = pln.POPULATION * pln.MINERALS;

                pln.Move(Time);

                s.PLN.Add(pln);

                dist = dist + 25;
                speed = speed / 3 + 0.1f;
            }
        }

        void generate_spiral_galaxy(double offset, int galaxysize, int starscount)
        {
            Double r;           //radius
            Double t;           //rotate angle
            Random rand = new Random();

            r = 0;
            t = offset;
            for (int i = 0; i < starscount / 2; i++)
            {
                StarSystem s = new StarSystem();

                r += rand.Next(4) + 15 * (galaxysize + 1);
                t += 0.2 - Math.PI * 2;
                
                s.timeOffset = t;
                s.R = r;

                s.y = -5.0 + rand.NextDouble() * 10.0;

                s.type = rand.Next(7);  //type impact on size and color
                s.name = GenerateRandomStarName(); //s.name = (i + 1).ToString();

                switch (s.type)
                {
                    //O - Blue, t =30 000 — 60 000 K
                    case 0:
                        s.br = Form1.BlueBrush;
                        break;

                    //B - Light blue, t = 10 500 — 30 000 K
                    case 1:
                        s.br = Form1.LightBlueBrush;
                        break;

                    //A - White, t = 7500—10 000 K
                    case 2:
                        s.br = Form1.WhiteBrush;
                        break;

                    //F - Light Yellow, t = 6000—7200 K
                    case 3:
                        s.br = Form1.LightYellowBrush;
                        break;

                    //G - Yellow, t = 5500 — 6000 K
                    case 4:
                        s.br = Form1.YellowBrush;
                        break;

                    //K - Orange, t = 4000 — 5250 K
                    case 5:
                        s.br = Form1.OrangeBrush;
                        break;

                    //M - Red, t = 2600 — 3850 K
                    case 6:
                        s.br = Form1.RedBrush;
                        break;
                }

                generatePlanets(s);

                stars.Add(s);
            }

        }

        void generate_elliptical_galaxy(int galaxysize, int starscount)
        {
            galaxysize++;
            double inc = Math.PI * 48 / starscount;
            Double t = 0;
            Random rand = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < starscount; i++)
            {
                StarSystem s = new StarSystem();

                t += inc;

                s.timeOffset = t;

                s.R = 100 * galaxysize + rand.Next(200) - 100;

                s.type = rand.Next(7);  //type impact on size and color
                s.name = GenerateRandomStarName();

                switch (s.type)
                {
                    //O - Blue, t =30 000 — 60 000 K
                    case 0:
                        s.br = Form1.BlueBrush;
                        break;

                    //B - Light blue, t = 10 500 — 30 000 K
                    case 1:
                        s.br = Form1.LightBlueBrush;
                        break;

                    //A - White, t = 7500—10 000 K
                    case 2:
                        s.br = Form1.WhiteBrush;
                        break;

                    //F - Light Yellow, t = 6000—7200 K
                    case 3:
                        s.br = Form1.LightYellowBrush;
                        break;

                    //G - Yellow, t = 5500 — 6000 K
                    case 4:
                        s.br = Form1.YellowBrush;
                        break;

                    //K - Orange, t = 4000 — 5250 K
                    case 5:
                        s.br = Form1.OrangeBrush;
                        break;

                    //M - Red, t = 2600 — 3850 K
                    case 6:
                        s.br = Form1.RedBrush;
                        break;
                }

                generatePlanets(s);
                stars.Add(s);
            }

        }

        void generate_irregular_galaxy1(bool rotate, int galaxysize, int starscount)//fix
        {
            Double x;
            Double y;
            Double r;
            Double t;
            Double z = 0;
            Double curve = 0;
            Random rand = new Random();

            for (int j = 0; j < (starscount / 2); j++)
            {
                r = 0;
                t = 0;
                for (int i = 0; i < 2; i++)
                {
                    r += rand.Next(4) + 2 + galaxysize * 20;
                    curve = Math.Pow((r - 2), 2);
                    curve = curve / 150;

                    t += 0.2;
                    z = t + rand.NextDouble() * 20;
                    x = curve + rand.Next(30) - 15;
                    y = curve * Math.Sin(z) + rand.Next(100) - 15;

                    StarSystem s = new StarSystem();
                    s.x = x;
                    s.y = -10.0 + rand.NextDouble() * 20.0;
                    s.z = y;
                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = GenerateRandomStarName();
                    s.planets_count = s.type + 1;
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = Form1.BlueBrush;
                            break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = Form1.LightBlueBrush;
                            break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = Form1.WhiteBrush;
                            break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = Form1.LightYellowBrush;
                            break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = Form1.YellowBrush;
                            break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = Form1.OrangeBrush;
                            break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = Form1.RedBrush;
                            break;
                    }
                    generatePlanets(s);
                    stars.Add(s);
                }
            }
        }

        void generate_irregular_galaxy(double offset, int galaxysize, int starscount)
        {
            Double r;           //radius
            Double t;           //rotate angle
            Random rand = new Random();

            r = 0;
            t = offset;
            for (int i = 0; i < starscount / 2; i++)
            {
                StarSystem s = new StarSystem();

                r += rand.Next(4) + 15 * (galaxysize + 1);
                t += 0.2 - Math.PI * 2;

                s.timeOffset = t;
                s.R = r;

                s.y = -5.0 + rand.NextDouble() * 10.0;
                s.increment = rand.Next(-starscount * 100, starscount * 100);

                s.type = rand.Next(7);  //type impact on size and color
                s.name = GenerateRandomStarName(); //s.name = (i + 1).ToString();

                switch (s.type)
                {
                    //O - Blue, t =30 000 — 60 000 K
                    case 0:
                        s.br = Form1.BlueBrush;
                        break;

                    //B - Light blue, t = 10 500 — 30 000 K
                    case 1:
                        s.br = Form1.LightBlueBrush;
                        break;

                    //A - White, t = 7500—10 000 K
                    case 2:
                        s.br = Form1.WhiteBrush;
                        break;

                    //F - Light Yellow, t = 6000—7200 K
                    case 3:
                        s.br = Form1.LightYellowBrush;
                        break;

                    //G - Yellow, t = 5500 — 6000 K
                    case 4:
                        s.br = Form1.YellowBrush;
                        break;

                    //K - Orange, t = 4000 — 5250 K
                    case 5:
                        s.br = Form1.OrangeBrush;
                        break;

                    //M - Red, t = 2600 — 3850 K
                    case 6:
                        s.br = Form1.RedBrush;
                        break;
                }

                generatePlanets(s);

                stars.Add(s);
            }

        }

        void generate_sphere_galaxy(int galaxysize, int starscount)
        {
            Double r;
            Double t = 0; ;

            Random rand = new Random();

            for (int j = 0; j < starscount / 2; j++)
            {
                r = 0;
                t += Math.PI * starscount / 2;
                for (int i = 0; i < 2; i++)
                {
                    r = rand.Next(starscount);
                    StarSystem s = new StarSystem();

                    s.R = r;
                    s.increment = 100 * (galaxysize + 1);
                    s.timeOffset = t;

                    s.type = rand.Next(7);  //type impact on size and color
                    s.name = GenerateRandomStarName();
                    s.planets_count = s.type + 1;
                    switch (s.type)
                    {
                        //O - Blue, t =30 000 — 60 000 K
                        case 0:
                            s.br = Form1.BlueBrush;
                            break;

                        //B - Light blue, t = 10 500 — 30 000 K
                        case 1:
                            s.br = Form1.LightBlueBrush;
                            break;

                        //A - White, t = 7500—10 000 K
                        case 2:
                            s.br = Form1.WhiteBrush;
                            break;

                        //F - Light Yellow, t = 6000—7200 K
                        case 3:
                            s.br = Form1.LightYellowBrush;
                            break;

                        //G - Yellow, t = 5500 — 6000 K
                        case 4:
                            s.br = Form1.YellowBrush;
                            break;

                        //K - Orange, t = 4000 — 5250 K
                        case 5:
                            s.br = Form1.OrangeBrush;
                            break;

                        //M - Red, t = 2600 — 3850 K
                        case 6:
                            s.br = Form1.RedBrush;
                            break;
                    }
                    generatePlanets(s);
                    stars.Add(s);
                }
            }

        }

        //генерирует уникальное случайное имя для системы
        string GenerateRandomStarName()
        {
            Random r = new Random();
            TextReader tr = new StreamReader(@"Starnames.xml");
            XmlSerializer xmlser = new XmlSerializer(typeof(string[]));

            string[] names = (string[])xmlser.Deserialize(tr);
            string name;
            tr.Close();

            bool uniq;
            while (true)
            {
                uniq = true;
                name = names[r.Next(names.Length)];

                for (int i = 0; i < stars.Count; i++)
                    if (stars[i].name.Equals(name))
                    {
                        uniq = false;
                        break;//Если планета с таким именем уже есть, пробуем снова
                    }
                if (uniq)
                    return name;
            }
        }
        
        //Движение галактики
        public override void Move(double time)
        {
            switch (galaxyType)
            {
                case GalaxyType.Spiral:
                    for (int i = 0; i < stars.Count; i++)
                    {
                        stars[i].x = stars[i].R * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].z = stars[i].R * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].Move(time);
                    }
                    break;
                case GalaxyType.Eliptical:
                    for (int i = 0; i < stars.Count; i++)
                    {
                        stars[i].x = stars[i].R * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].z = stars[i].R * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].Move(time);
                    }
                    break;
                case GalaxyType.Sphere:
                    for (int i = 0; i < stars.Count; i++)
                    {
                        double x = Math.Cos(stars[i].R) * stars[i].increment;
                        double y = Math.Sin(stars[i].R) * stars[i].increment;

                        stars[i].x = x * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset)) + z * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].z = x * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset)) - z * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].y = y * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset)) + stars[i].z * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset));
                        stars[i].Move(time);
                    }
                    break;
                case GalaxyType.Irregular:
                    for (int i = 0; i < stars.Count; i++)
                    {
                        stars[i].x = stars[i].R * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset - stars[i].increment));
                        stars[i].z = stars[i].R * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset + stars[i].increment));
                        stars[i].y = y * Math.Cos(stars[i].angVel * (time + stars[i].timeOffset + stars[i].increment)) + stars[i].z * Math.Sin(stars[i].angVel * (time + stars[i].timeOffset - stars[i].increment));
                        stars[i].Move(time);
                    }
                    break;
            }
        }
    }
}
