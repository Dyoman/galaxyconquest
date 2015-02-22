﻿using GalaxyConquest.Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    /// Класс отвечает за рисование
    /// </summary>
    public class DrawController
    {
        /// <summary>
        /// Контрол, к которму привязано рисование
        /// </summary>
        public Control DrawTarget { get; private set; }
        /// <summary>
        /// Поворот по оси вертикали
        /// </summary>
        double spinX = 0.0;
        /// <summary>
        /// Поворот по оси горизонтали
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
        /// Дисперсия/разброс. Радиус области вокруг объекта, при клике на которую объект будет выбран
        /// </summary>
        int dispersion = 7;
        /// <summary>
        /// Создание экземпляра класса
        /// </summary>
        /// <param name="target">Цель для рисования</param>
        public DrawController(Control target)
        {
            DrawTarget = target;
            DrawTarget.SizeChanged += drawTarget_SizeChanged;

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

            int r = 6;
            Pen pen = new Pen(Color.Gold);
            double ugol = 2 * Math.PI / (3);
            Point[] points = new Point[3];

            g.ScaleTransform(scaling, scaling);

            //рисуем звездные системы
            for (int i = 0; i < state.Galaxy.stars.Count; i++)
            {
                float starSize = 0;

                StarSystem s = state.Galaxy.stars[i];

                Vector scr = getScreenCoordOf(s);

                starSize = (s.type + 1) * 1.5f;

                g.FillEllipse(s.br, (float)scr.X - starSize / 2, (float)scr.Y - starSize / 2, starSize, starSize);

                if (s == state.Player.selectedStar)
                {
                    g.DrawEllipse(pen, new RectangleF((float)scr.X - starSize / 2 - 5, (float)scr.Y - starSize / 2 - 5, starSize + 10, starSize + 10));
                }

                if (state.Player.stars.Contains(s))
                {
                    g.DrawEllipse(Pens.GreenYellow, new RectangleF((float)scr.X - starSize / 2 - 3, (float)scr.Y - starSize / 2 - 3, starSize + 6, starSize + 6));
                    g.DrawString(s.name, new Font("Arial", 5.5F, FontStyle.Bold), Brushes.GreenYellow, new PointF((float)scr.X + 6, (float)scr.Y + 6));
                }
                else if (s.Discovered)
                    g.DrawString(s.name, new Font("Arial", 5.0F), Brushes.White, new PointF((float)scr.X + 6, (float)scr.Y + 6));
            }


            //----------------Neutral fleets------------------
            for (int k = 0; k < state.Galaxy.neutrals.Count; k++)
            {
                Fleet fleet = state.Galaxy.neutrals[k];
                if (!fleet.s1.Discovered)
                    continue;
                Vector scr = getScreenCoordOf(fleet);
                scr.X -= 10;
                scr.Y -= 10;

                PointF[] compPointArrayShip = {  //точки для рисование корабля
                                    new PointF((float)scr.X + r * (float)Math.Cos(-1 * ugol), (float)scr.Y + r * (float)Math.Sin(-1 * ugol)),
                                    new PointF((float)scr.X + r * (float)Math.Cos(-2 * ugol), (float)scr.Y + r * (float)Math.Sin(-2 * ugol)),
                                    new PointF((float)scr.X + r * (float)Math.Cos(-3 * ugol), (float)scr.Y + r * (float)Math.Sin(-3 * ugol))};
                g.FillPolygon(FleetBrushes.NeutralFleet, compPointArrayShip);
                g.DrawString(fleet.name, new Font("Arial", 5.0F), FleetBrushes.NeutralFleet, new PointF((float)scr.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scr.Y + r * (float)Math.Sin(-3 * ugol) - 12));
            }

            //----------------------Player Fleets----------------------
            for (int k = 0; k < state.Player.fleets.Count; k++)
            {
                Fleet fleet = state.Player.fleets[k];
                StarSystem flSys = fleet.s1;
                StarSystem targSys = fleet.s2;

                Vector scr = getScreenCoordOf(fleet);
                scr.X -= 10;
                scr.Y -= 10;

                PointF[] compPointArrayShip = {  //точки для рисование корабля
                                    new PointF((float)scr.X + r * (float)Math.Cos(-1 * ugol), (float)scr.Y + r * (float)Math.Sin(-1 * ugol)),
                                    new PointF((float)scr.X + r * (float)Math.Cos(-2 * ugol), (float)scr.Y + r * (float)Math.Sin(-2 * ugol)),
                                    new PointF((float)scr.X + r * (float)Math.Cos(-3 * ugol), (float)scr.Y + r * (float)Math.Sin(-3 * ugol))};

                if (k == state.Player.selectedFleet)
                {
                    g.FillPolygon(FleetBrushes.ActiveFleet, compPointArrayShip);
                    g.DrawString(fleet.name, new Font("Arial", 9.0F, FontStyle.Bold), FleetBrushes.ActiveFleet, new PointF((float)scr.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scr.Y + r * (float)Math.Sin(-3 * ugol) - 12));

                }
                else
                {
                    g.FillPolygon(FleetBrushes.PassiveFleet, compPointArrayShip);
                    g.DrawString(fleet.name, new Font("Arial", 8.0F), FleetBrushes.PassiveFleet, new PointF((float)scr.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scr.Y + r * (float)Math.Sin(-3 * ugol) - 12));
                }

                if (state.Player.warpTarget != null && k == state.Player.selectedFleet && (!state.Player.fleets[state.Player.selectedFleet].onWay))
                {
                    Way way = new Way();
                    way.CalculateWay(state.Player.fleets[k].s1, state.Player.warpTarget);
                    //double starDistance = Distance(state.Player.fleets[state.Player.selectedFleet], state.Player.warpTarget);
                    double starDistance = way.Distance;
                    string dis = Math.Round(starDistance, 3).ToString() + " св. лет\n<Ходов: ~" + ((int)(starDistance * MovementsController.FIXED_TIME_DELTA) + 1).ToString() + ">";

                    Vector scrFrom = new Vector(), scrTo = new Vector();
                    /*
                    scrFrom = getScreenCoordOf(flSys);
                    scrTo = getScreenCoordOf(state.Player.warpTarget);
                    
                    if (starDistance < Fleet.MaxDistance)//пока тестим
                    {
                        pen.Color = Color.Lime;
                        g.DrawString(dis, new Font("Arial", 6.0F), Brushes.Lime,
                            new PointF((float)scrTo.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scrTo.Y + r * (float)Math.Sin(-3 * ugol) + 12));
                    }
                    else
                    {
                        pen.Color = Color.Red;
                        g.DrawString(dis, new Font("Arial", 6.0F), Brushes.Red,
                            new PointF((float)scrTo.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scrTo.Y + r * (float)Math.Sin(-3 * ugol) + 12));
                    }
                    */
                    for (int i = 1; i < way.Count; i++)
                    {
                        scrFrom = getScreenCoordOf(way[i - 1]);
                        scrTo = getScreenCoordOf(way[i]);
                        g.DrawLine(pen,
                            new PointF((float)scrFrom.X, (float)scrFrom.Y),
                            new PointF((float)scrTo.X, (float)scrTo.Y));
                    }
                    g.DrawString(dis, new Font("Arial", 6.0F), Brushes.Lime,
                        new PointF((float)scrTo.X + r * (float)Math.Cos(-3 * ugol) - 3, (float)scrTo.Y + r * (float)Math.Sin(-3 * ugol) + 12));
                }

                if (fleet.way.Count > 0 && k == state.Player.selectedFleet)    //new 
                {
                    pen.Color = Color.White;
                    pen.Width += 2;
                    pen.DashStyle = DashStyle.Dash;

                    for (int i = Math.Max(fleet.way.Current - 1, 0); i < fleet.way.Count - 1; i++)
                    {
                        Vector scrFrom = getScreenCoordOf(fleet.way[i]);
                        scrFrom.X -= 10;
                        scrFrom.Y -= 10;
                        Vector scrTo = getScreenCoordOf(fleet.way[i + 1]);
                        scrTo.X -= 10;
                        scrTo.Y -= 10;

                        g.DrawLine(pen,
                                new PointF((float)scrFrom.X + 10, (float)scrFrom.Y + 10),
                                new PointF((float)scrTo.X + 10, (float)scrTo.Y + 10));
                    }
                }

                if (targSys != null && false)   //old
                {
                    Vector scrFrom = getScreenCoordOf(flSys);
                    scrFrom.X -= 10;
                    scrFrom.Y -= 10;
                    Vector scrTo = getScreenCoordOf(targSys);
                    scrTo.X -= 10;
                    scrTo.Y -= 10;

                    pen.Color = Color.White;
                    pen.Width += 2;
                    pen.DashStyle = DashStyle.Dash;

                    g.DrawLine(pen,
                            new PointF((float)scrFrom.X + 10, (float)scrFrom.Y + 10),
                            new PointF((float)scrTo.X + 10, (float)scrTo.Y + 10));
                }
                pen.Color = Color.Gold;
                pen.DashStyle = DashStyle.Solid;
                pen.Width -= 2;
            }

            //рисуем гиперпереходы
            for (int i = 0; i < state.Galaxy.lanes.Count; i++)
            {
                StarWarp w = state.Galaxy.lanes[i];

                g.DrawLine(Pens.Gray,
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
            Vector centerScr = getScreenCoordOf(system.PLN[0]);
            for (int i = 0; i < system.PLN.Count; i++)
            {
                StarSystems.PLANET p = system.PLN[i];

                Vector scr = getScreenCoordOf(p);

                g.DrawEllipse(new Pen(Color.White), (float)centerScr.X - p.DISTANCE, (float)centerScr.Y - p.DISTANCE, p.DISTANCE * 2, p.DISTANCE * 2);
                g.FillEllipse(new SolidBrush(p.CLR), new RectangleF((float)scr.X - p.SIZE / 2, (float)scr.Y - p.SIZE / 2, p.SIZE, p.SIZE));
                g.DrawString(p.name, new Font("arial", 7.0f), new SolidBrush(Color.White), new PointF((float)scr.X, (float)scr.Y));
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
            spinX += dx * 0.01;
            spinY += dy * 0.01;
        }
        /// <summary>
        /// Изменяет масштаб
        /// </summary>
        /// <param name="dv">Величина изменения</param>
        public void ChangeScale(double dv)
        {
            if (dv > 0)
            {
                if (scaling >= 10)
                    return;
                else
                    scaling += 0.2f;
            }
            else
            {
                if (scaling <= 0.4)
                    return;
                else
                    scaling -= 0.2f;
            }
            UpdateCenters();
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на флоте
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Флот для проверки</param>
        public bool CursorIsOnObject(MouseEventArgs e, Fleet obj)
        {
            Vector scr = getScreenCoordOf(obj);
            scr.X -= 10;
            scr.Y -= 10;
            return (e.X + dispersion) / scaling > (scr.X - dispersion) &&
                   (e.X - dispersion) / scaling < (scr.X + dispersion) &&
                   (e.Y + dispersion) / scaling > (scr.Y - dispersion) &&
                   (e.Y - dispersion) / scaling < (scr.Y + dispersion);
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на звездной системе
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Система для проверки</param>
        public bool CursorIsOnObject(MouseEventArgs e, StarSystem obj)
        {
            Vector scr = getScreenCoordOf(obj);
            double starSize = (obj.type + 1) * 1.5;

            return (e.X + dispersion) / scaling > (scr.X - starSize / 2) &&
                   (e.X - dispersion) / scaling < (scr.X + starSize / 2) &&
                   (e.Y + dispersion) / scaling > (scr.Y - starSize / 2) &&
                   (e.Y - dispersion) / scaling < (scr.Y + starSize / 2);
        }
        /// <summary>
        /// Проверка нахождения курсора мыши на планете
        /// </summary>
        /// <param name="e">Представляет информацию о курсоре</param>
        /// <param name="obj">Планета для проверки</param>
        public bool CursorIsOnObject(MouseEventArgs e, StarSystems.PLANET obj)
        {
            Vector scr = getScreenCoordOf(obj);

            return e.X > (scr.X - obj.SIZE / 2) &&
                   e.X < (scr.X + obj.SIZE / 2) &&
                   e.Y > (scr.Y - obj.SIZE / 2) &&
                   e.Y < (scr.Y + obj.SIZE / 2);
        }
        /// <summary>
        /// Рассчитывает дистанцию от одного объекта до другого
        /// </summary>
        /// <param name="from">Первый объект</param>
        /// <param name="to">Второй оъект</param>
<<<<<<< HEAD
        /// <returns></returns>
        public static double Distance(SpaceObject from, SpaceObject to)
=======
        public double Distance(SpaceObject from, SpaceObject to)
>>>>>>> master
        {
            return Math.Sqrt(Math.Pow((to.x - from.x), 2) + Math.Pow((to.y - from.y), 2) + Math.Pow((to.z - from.z), 2));
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
    }
}
