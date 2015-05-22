using GalaxyConquest.Game;
using GalaxyConquest.PathFinding;
using GalaxyConquest.SpaceObjects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Gwen.Control;

namespace GalaxyConquest.Drawing
{
    /// <summary>
    /// Цвета для планет
    /// </summary>
    public struct PlanetBrushes
    {
        public static SolidBrush BlueBrush = new SolidBrush(Color.FromArgb(255, 123, 104, 238));
        public static SolidBrush LightBlueBrush = new SolidBrush(Color.FromArgb(180, 135, 206, 235));
        public static SolidBrush WhiteBrush = new SolidBrush(Color.FromArgb(255, 225, 250, 240));
        public static SolidBrush LightYellowBrush = new SolidBrush(Color.FromArgb(180, 255, 255, 0));
        public static SolidBrush YellowBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
        public static SolidBrush OrangeBrush = new SolidBrush(Color.FromArgb(255, 255, 140, 0));
        public static SolidBrush RedBrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
        public static SolidBrush SuperWhiteBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public static SolidBrush GoldBrush = new SolidBrush(Color.Gold);
    }
    /// <summary>
    /// Цвета флотов
    /// </summary>
    public struct FleetBrushes
    {
        public static Brush ActiveFleet = Brushes.GreenYellow;
        public static Brush PassiveFleet = Brushes.MediumSeaGreen;
        public static Brush NeutralFleet = Brushes.Silver;
        public static Brush EnemyFleet = Brushes.OrangeRed;
    }
    /// <summary>
    /// Цвета флотов
    /// </summary>
    public struct FleetColors
    {
        public static Color ActiveFleet = Color.GreenYellow;
        public static Color PassiveFleet = Color.MediumSeaGreen;
        public static Color NeutralFleet = Color.Silver;
        public static Color EnemyFleet = Color.OrangeRed;
    }
    /// <summary>
    /// Цвета имён звезд.
    /// </summary>
    public struct StarBrushes
    {
        public static Brush DefaultStarBrush = Brushes.White;
        public static Brush PlayerStarBrush = Brushes.GreenYellow;
    }
    /// <summary>
    /// Цвета линий.
    /// </summary>
    public struct Lines
    {
        public static Color LineAvailableColor = Color.Lime;
        public static Color LineUnavailableColor = Color.Red;
        public static Color WarpLineColor = Color.White;
        public static Color TransitionLineColor = Color.Gray;
        public static Color StarSelectionColor = Color.Gold;
        public static Brush LineAvailableBrush = Brushes.Lime;
        public static Brush LineUnvailableBrush = Brushes.Red;
    }

    /// <summary>
    /// Структура описывает вектор в двухмерном пространстве
    /// </summary>
    public struct Vector
    {
        /// <summary>
        /// Координаты вектора
        /// </summary>
        public double X, Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    /// <summary>
    /// Структура описывает вектор в трехмерном пространстве
    /// </summary>
    public struct Vector3
    {
        public double X, Y, Z;

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Возвращает скалярное произведение вектора на второй вектор
        /// </summary>
        public double ScalarWith(Vector3 v2)
        {
            return Normalized.X * v2.Normalized.X + Normalized.Y * v2.Normalized.Y + Normalized.Z * v2.Normalized.Z;
        }
        /// <summary>
        /// Длина вектора
        /// </summary>
        public double Length
        {
            get
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            }
        }
        /// <summary>
        /// Нормализованный вектор (указывает направление, но длина равна 1)
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                return new Vector3(X / Length, Y / Length, Z / Length);
            }
        }
    }

    /// <summary>
    /// Представляет класс, отвечающий за рисование
    /// </summary>
    public class DrawController
    {
        #region CONSTANTS
        int DISPERSION = 7; //Дисперсия/разброс. Радиус области вокруг объекта, при клике на которую объект будет выбран
        //Margin
        const int FLEET_ICON_MARGIN = -25;  // отступ рисования иконки флота (треугольника)
        const int FLEET_NAME_MARGIN_X = 0;  // отступ рисования имени флота
        const int FLEET_NAME_MARGIN_Y = -30;// отступ рисования имени флота
        const int STAR_NAME_MARGIN = 20;     // отступ рисования имени звезды
        const int STAR_SELECTION_MARGIN = 5;// дополнительный отступ при рисовании элипса вокруг выделенной звезды
        const int WARP_TEXT_MARGIN_X = 10;  // отступ рисования текста перемещения
        const int WARP_TEXT_MARGIN_Y = -10; // отступ рисования текста перемещения
        const int PLANET_NAME_MARGIN = 6;   // отступ рисования имени планеты
        //StarSize
        const float STAR_SIZE_SCALE = 4.5f; // множитель размера звезд
        //Font
        const string FONT_FAMILY_NAME = "Arial";                // стандартный шрифт
        const float STARNAME_FONT_SIZE = 10.0f;                  // размер шрифта имени звезды
        const float PLAYER_STARNAME_FONT_SIZE = 11.0f;           // размер шрифта имени звезды, если она принадлежит игроку
        const float FLEET_NAME_FONT_SIZE = 11.0f;                // размер шрифта имени флота
        const float PLAYER_FLEET_NAME_FONT_SIZE = 12.0f;         // размер шрифта имени флота, если он принадлежит игроку
        const float ACTIVE_PLAYER_FLEET_NAME_FONT_SIZE = 13.0f;  // размер шрифта имени флота, если он является активным флотом игрока
        const FontStyle STAR_FONTSTYLE = FontStyle.Regular;     // стандартный стиль шрифта для имён звёзд
        const FontStyle PLAYER_STAR_FONTSTYLE = FontStyle.Bold; // стиль шрифта для имён звезд игрока
        const float WARP_TEXT_FONT_SIZE = 11.0F;                 // размер шрифта текста, выводимого при отображении пути
        const float PLANET_NAME_FONT_SIZE = 12.0f;               // размер шрифта имени планеты
        //Scalling
        const double MAX_SCALE = 10.0;      // максимальный масштаб
        const double MIN_SCALE = 0.2;       // минимальный масштаб
        const double SCALE_DELTA = 0.2;     // величина, на которую изменяется масштаб за одну прокрутку ролика
        //Rotation
        const double ROTATION_SPEED = 0.01; // скорость вращения "камеры"
        //Drawing
        const float PATH_LINE_PEN_WIDTH = 4f;   // ширина линии, которая рисует путь
        //Fleet
        const int FLEET_ICON_UPLEFT_X = -3;     // 
        const int FLEET_ICON_UPLEFT_Y = -5;     //
        const int FLEET_ICON_BOTTOMLEFT_X = -3; // координаты вершин иконки флота относительно центра
        const int FLEET_ICON_BOTTOMLEFT_Y = 5;  //
        const int FLEET_ICON_RIGHT_X = 6;       //
        const int FLEET_ICON_RIGHT_Y = 0;       //
        #endregion

        /// <summary>
        /// Контрол, к которму привязано рисование
        /// </summary>
        public Image DrawTarget { get; private set; }
        /// <summary>
        /// Поворот по вертикали
        /// </summary>
        double spinX = 0.0;
        /// <summary>
        /// Поворот по горизонтали
        /// </summary>
        double spinY = Math.PI / 4;
        /// <summary>
        /// Масштаб
        /// </summary>
        float scaling = 1f;
        /// <summary>
        /// Сдвиг по горизонтали
        /// </summary>
        float horizontal = 0;
        /// <summary>
        /// Сдвиг по вертикали
        /// </summary>
        float vertical = 0;
        /// <summary>
        /// Координаты центра
        /// </summary>
        float centerX, centerY;
        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="target">Цель для рисования</param>
        public DrawController(Image target)
        {
            DrawTarget = target;
            //TODO: DrawTarget.SizeChanged += drawTarget_SizeChanged;

            UpdateCenters();
        }
        /// <summary>
        /// Рассчитывает экранные координаты объекта
        /// </summary>
        /// <param name="obj">Объект, чьи координаты будут рассчитаны</param>
        /// <returns>Экземпляр класса Vector</returns>
        public Vector getScreenCoordOf(SpaceObject obj)
        {
            double x = obj.x * Math.Cos(spinX) - obj.z * Math.Sin(spinX);
            double z = obj.x * Math.Sin(spinX) + obj.z * Math.Cos(spinX);
            double y = obj.y * Math.Cos(spinY) - z * Math.Sin(spinY);
            return new Vector(centerX + x, centerY + y);
        }
        /// <summary>
        /// Рисует галактику
        /// </summary>
        /// <param name="state">Галактика, которую нужно нарисовать</param>
        /// <param name="g">Полотно для рисования</param>
        public void Render(GameState state, Graphics g)
        {
            if (state.Galaxy == null || state.Player == null) return;

            g.ScaleTransform(scaling, scaling);

            //рисуем звездные системы
            for (int i = 0; i < state.Galaxy.stars.Count; i++)
            {
                StarSystem s = state.Galaxy.stars[i];

                Vector scr = getScreenCoordOf(s);

                float starSize = (s.type + 5) * STAR_SIZE_SCALE;
                float glowSize = (s.type + 12) * STAR_SIZE_SCALE;

                RectangleF GlowRectangle = new RectangleF((float)(scr.X - glowSize / 2), (float)(scr.Y - glowSize / 2), (float)glowSize, (float)glowSize);
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(GlowRectangle);

                PathGradientBrush pgb = new PathGradientBrush(gp);

                pgb.CenterPoint = new PointF((float)(scr.X - glowSize / 2) + (float)(glowSize / 2), (float)(scr.Y - glowSize / 2) + (float)(glowSize / 2));
                pgb.CenterColor = s.color.Color;
                pgb.SurroundColors = new Color[] { Color.FromArgb(0x00FF0000) };
                pgb.SetBlendTriangularShape(.5f, 1.0f);
                pgb.FocusScales = new PointF(0f, 0f);

                g.FillPath(pgb, gp);

                pgb.Dispose();
                gp.Dispose();

                g.FillEllipse(s.color, (float)scr.X - starSize / 2, (float)scr.Y - starSize / 2, starSize, starSize);
                //  Вокруг выбраной звезды рисуем круг
                if (s == state.Player.selectedStar)
                {
                    g.DrawEllipse(new Pen(Lines.StarSelectionColor), new RectangleF((float)scr.X - starSize / 2 - STAR_SELECTION_MARGIN, (float)scr.Y - starSize / 2 - STAR_SELECTION_MARGIN, starSize + STAR_SELECTION_MARGIN * 2, starSize + STAR_SELECTION_MARGIN * 2));
                }
                //  Вокруг звезды, принадлежащей игроку рисуем большой желтый круг и имя этой звезды пишем бОльшим шрифтом и другим цвета
                if (s.Discovered)
                    if (s.Owner == state.Player)
                    {
                        //g.DrawEllipse(Pens.GreenYellow, new RectangleF((float)scr.X - starSize / 2 - 3, (float)scr.Y - starSize / 2 - 3, starSize + 6, starSize + 6));
                        g.DrawString(s.name, new Font(FONT_FAMILY_NAME, PLAYER_STARNAME_FONT_SIZE, PLAYER_STAR_FONTSTYLE), StarBrushes.PlayerStarBrush, new PointF((float)scr.X + STAR_NAME_MARGIN, (float)scr.Y + STAR_NAME_MARGIN));
                    }
                    else
                        g.DrawString(s.name, new Font(FONT_FAMILY_NAME, STARNAME_FONT_SIZE, STAR_FONTSTYLE), StarBrushes.DefaultStarBrush, new PointF((float)scr.X + STAR_NAME_MARGIN, (float)scr.Y + STAR_NAME_MARGIN));
            }


            //----------------Neutral fleets------------------
            for (int k = 0; k < state.Galaxy.neutrals.Count; k++)
            {
                Fleet fleet = state.Galaxy.neutrals[k];
                if (!fleet.s1.Discovered)
                    continue;
                Vector scr = getScreenCoordOf(fleet);
                scr.X -= FLEET_ICON_MARGIN;
                scr.Y += FLEET_ICON_MARGIN;

                PointF[] compPointArrayShip = GetFleetIcon(scr);
                g.FillPolygon(FleetBrushes.NeutralFleet, compPointArrayShip);
                g.DrawString(fleet.name, new Font(FONT_FAMILY_NAME, FLEET_NAME_FONT_SIZE), FleetBrushes.NeutralFleet, new PointF((float)scr.X + FLEET_NAME_MARGIN_X, (float)scr.Y + FLEET_NAME_MARGIN_Y));
            }

            //----------------------Player Fleets----------------------
            for (int k = 0; k < state.Player.fleets.Count; k++)
            {
                Fleet fleet = state.Player.fleets[k];
                StarSystem flSys = fleet.s1;
                StarSystem targSys = fleet.s2;

                Vector scr = getScreenCoordOf(fleet);
                scr.X += FLEET_ICON_MARGIN;
                scr.Y += FLEET_ICON_MARGIN;

                PointF[] compPointArrayShip = GetFleetIcon(scr);

                if (k == state.Player.selectedFleet)
                {
                    g.FillPolygon(FleetBrushes.ActiveFleet, compPointArrayShip);
                    g.DrawString(fleet.name, new Font(FONT_FAMILY_NAME, ACTIVE_PLAYER_FLEET_NAME_FONT_SIZE, FontStyle.Bold), FleetBrushes.ActiveFleet, new PointF((float)scr.X + FLEET_NAME_MARGIN_X, (float)scr.Y + FLEET_NAME_MARGIN_Y));

                }
                else
                {
                    g.FillPolygon(FleetBrushes.PassiveFleet, compPointArrayShip);
                    g.DrawString(fleet.name, new Font(FONT_FAMILY_NAME, PLAYER_FLEET_NAME_FONT_SIZE), FleetBrushes.PassiveFleet, new PointF((float)scr.X + FLEET_NAME_MARGIN_X, (float)scr.Y + FLEET_NAME_MARGIN_Y));
                }

                /// Рисуется путь от текущего флота к звезде, на которую наведен курсор мыши
                if (state.Player.warpTarget != null && k == state.Player.selectedFleet && (!state.Player.fleets[state.Player.selectedFleet].onWay))
                {
                    StarPath path = new StarPath();
                    path.CalculateWay(state.Player.fleets[k].s1, state.Player.warpTarget);
                    double starDistance = path.Distance;
                    string dis = Math.Round(starDistance, 3).ToString() + " св. лет\n<Ходов: ~" + ((int)(starDistance * MovementsController.FIXED_TIME_DELTA) + 1).ToString() + ">";

                    Vector scrFrom = new Vector(), scrTo = new Vector();
                    Pen pen = null;
                    Brush brush = null;

                    if (starDistance < Fleet.MaxDistance)
                    {
                        pen = new Pen(Lines.LineAvailableColor);
                        brush = Lines.LineAvailableBrush;
                    }
                    else
                    {
                        pen = new Pen(Lines.LineUnavailableColor);
                        brush = Lines.LineUnvailableBrush;
                    }

                    g.DrawLine(pen,
                        new PointF((float)scrFrom.X, (float)scrFrom.Y),
                        new PointF((float)scrTo.X, (float)scrTo.Y));

                    for (int i = 1; i < path.Count; i++)
                    {
                        scrFrom = getScreenCoordOf(path[i - 1]);
                        scrTo = getScreenCoordOf(path[i]);
                        g.DrawLine(pen,
                            new PointF((float)scrFrom.X, (float)scrFrom.Y),
                            new PointF((float)scrTo.X, (float)scrTo.Y));
                    }
                    g.DrawString(dis, new Font(FONT_FAMILY_NAME, WARP_TEXT_FONT_SIZE), brush, new PointF((float)scrTo.X + WARP_TEXT_MARGIN_X, (float)scrTo.Y + WARP_TEXT_MARGIN_Y));
                }

                /// Если для текущего флота рассчитан путь, рисуем его, отбрасывая те рёбра, которые флот уже прошел
                if (!fleet.path.Empty && k == state.Player.selectedFleet)    //new 
                {
                    Pen pen = new Pen(Lines.WarpLineColor, PATH_LINE_PEN_WIDTH);
                    pen.DashStyle = DashStyle.Dash;

                    for (int i = Math.Max(fleet.path.Current - 1, 0); i < fleet.path.Count - 1; i++)
                    {
                        Vector scrFrom = getScreenCoordOf(fleet.path[i]);
                        Vector scrTo = getScreenCoordOf(fleet.path[i + 1]);

                        g.DrawLine(pen,
                                new PointF((float)scrFrom.X, (float)scrFrom.Y),
                                new PointF((float)scrTo.X, (float)scrTo.Y));
                    }
                }
            }

            //рисуем гиперпереходы
            for (int i = 0; i < state.Galaxy.lanes.Count; i++)
            {
                StarWarp w = state.Galaxy.lanes[i];

                g.DrawLine(new Pen(Lines.TransitionLineColor),
                    new PointF((float)w.system1.x, (float)w.system1.y),
                    new PointF((float)w.system2.x, (float)w.system2.y));
            }
        }
        /// <summary>
        /// Рисует звездную систему
        /// </summary>
        /// <param name="state">Система, которую нужно нарисовать</param>
        /// <param name="g">Полотно для рисования</param>
        public void Render(StarSystem system, Graphics g)
        {
            g.ScaleTransform(scaling, scaling);

            Vector scr = getScreenCoordOf(system.centralStar);
            g.FillEllipse(system.centralStar.color, (float)scr.X - system.centralStar.size / 2, (float)scr.Y - system.centralStar.size / 2, system.centralStar.size, system.centralStar.size);

            for (int i = 0; i < system.planets.Count; i++)
            {
                Planet p = system.planets[i];

                scr = getScreenCoordOf(p);
                float distX = p.distance;
                float distY = p.distance * (float)Math.Sin(spinY);
                g.DrawEllipse(Pens.White, centerX - distX, centerY - distY, distX * 2, distY * 2);
                g.FillEllipse(new SolidBrush(p.planetColor), (float)scr.X - p.SIZE / 2, (float)scr.Y - p.SIZE / 2, p.SIZE, p.SIZE);
                g.DrawString(p.name, new Font(FONT_FAMILY_NAME, PLANET_NAME_FONT_SIZE), new SolidBrush(Color.White), new PointF((float)scr.X + PLANET_NAME_MARGIN, (float)scr.Y + PLANET_NAME_MARGIN));
            }
        }
        /// <summary>
        /// Двигает изображение
        /// </summary>
        /// <param name="dx">По вертикали</param>
        /// <param name="dy">По горизонтали</param>
        public void Move(float dx, float dy)
        {
            horizontal += dx / scaling;
            vertical += dy / scaling;

            UpdateCenters();
        }
        /// <summary>
        /// Поворачивает изображение
        /// </summary>
        /// <param name="dx">По вертикали</param>
        /// <param name="dy">По горизонтали</param>
        public void Rotate(float dx, float dy)
        {
            spinX += dx * ROTATION_SPEED;
            spinY += dy * ROTATION_SPEED;
        }
        /// <summary>
        /// Изменяет масштаб
        /// </summary>
        /// <param name="dv">Величина изменения</param>
        public void ChangeScale(double dv)
        {
            if (dv > 0)
                scaling = (float)Math.Min(scaling + SCALE_DELTA, MAX_SCALE);
            else
                scaling = (float)Math.Max(scaling - SCALE_DELTA, MIN_SCALE);

            UpdateCenters();
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на флоте
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Флот для проверки</param>
        public bool CursorIsOnObject(ClickedEventArgs e, Fleet obj)
        {
            Vector scr = getScreenCoordOf(obj);
            scr.X += FLEET_ICON_MARGIN;
            scr.Y += FLEET_ICON_MARGIN;
            return (e.X + DISPERSION) / scaling > (scr.X - DISPERSION) &&
                   (e.X - DISPERSION) / scaling < (scr.X + DISPERSION) &&
                   (e.Y + DISPERSION) / scaling > (scr.Y - DISPERSION) &&
                   (e.Y - DISPERSION) / scaling < (scr.Y + DISPERSION);
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на звездной системе
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Система для проверки</param>
        public bool CursorIsOnObject(ClickedEventArgs e, StarSystem obj)
        {
            Vector scr = getScreenCoordOf(obj);
            double starSize = (obj.type + 1) * STAR_SIZE_SCALE;

            return (e.X + DISPERSION) / scaling > (scr.X - starSize / 2) &&
                   (e.X - DISPERSION) / scaling < (scr.X + starSize / 2) &&
                   (e.Y + DISPERSION) / scaling > (scr.Y - starSize / 2) &&
                   (e.Y - DISPERSION) / scaling < (scr.Y + starSize / 2);
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на планете
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Планета для проверки</param>
        public bool CursorIsOnObject(ClickedEventArgs e, Planet obj)
        {
            Vector scr = getScreenCoordOf(obj);

            return (e.X + DISPERSION) / scaling > (scr.X - obj.SIZE / 2) &&
                   (e.X - DISPERSION) / scaling < (scr.X + obj.SIZE / 2) &&
                   (e.Y + DISPERSION) / scaling > (scr.Y - obj.SIZE / 2) &&
                   (e.Y - DISPERSION) / scaling < (scr.Y + obj.SIZE / 2);
        }
        /// <summary>
        /// Рассчитывает дистанцию от одного объекта до другого
        /// </summary>
        /// <param name="from">Первый объект</param>
        /// <param name="to">Второй оъект</param>
        public static double Distance(SpaceObject from, SpaceObject to)
        {
            return Math.Sqrt(Math.Pow((to.x - from.x), 2) + Math.Pow((to.y - from.y), 2) + Math.Pow((to.z - from.z), 2));
        }

        /// <summary>
        /// Получает массив точек, представляющих координаты иконки флота на экране.
        /// </summary>
        /// <param name="screenCoords">Экранные координаты флота.</param>
        PointF[] GetFleetIcon(Vector screenCoords)
        {
            return new PointF[3] {new PointF((float)screenCoords.X + FLEET_ICON_UPLEFT_X, (float)screenCoords.Y + FLEET_ICON_UPLEFT_Y),
                                  new PointF((float)screenCoords.X + FLEET_ICON_BOTTOMLEFT_X, (float)screenCoords.Y + FLEET_ICON_BOTTOMLEFT_Y),
                                  new PointF((float)screenCoords.X + FLEET_ICON_RIGHT_X, (float)screenCoords.Y+FLEET_ICON_RIGHT_Y)};
        }
        /// <summary>
        /// Обновляет координаты центра
        /// </summary>
        void UpdateCenters()
        {
            centerX = DrawTarget.Width / 2 / scaling + horizontal;
            centerY = DrawTarget.Height / 2 / scaling + vertical;
        }
        /// <summary>
        /// Обновляем координаты центра, если размер объекта, на котором происходит рисование изменился
        /// </summary>
        void drawTarget_SizeChanged(object sender, EventArgs e)
        {
            UpdateCenters();
        }
        /// <summary>
        /// Обновляем координаты центра, центром становится звездная система
        /// </summary>
        public void UpdateCenters(StarSystem s)
        {
            horizontal -= (float)getScreenCoordOf(s).X / scaling - DrawTarget.Width / 2;
            vertical -= (float)getScreenCoordOf(s).Y / scaling - DrawTarget.Height / 2;
            UpdateCenters();
        }
    }
}
