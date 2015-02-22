using System;
using System.Collections.Generic;
using GalaxyConquest.Game;
using System.Linq;
using System.Text;

namespace GalaxyConquest.Tactics
{
    class FindWay
    {
        public static int FindDirection(int x1, int x2, int y1, int y2)
        {
            int direction = 0;
            if (x2 == x1 && y2 - y1 < 0) direction = Constants.MOVE_TOP;
            else if (x2 - x1 > 0 && y2 - y1 <= 0) direction = Constants.MOVE_RIGHT_TOP;
            else if (x2 - x1 > 0 && y2 - y1 > 0) direction = Constants.MOVE_RIGHT_BOTTOM;
            else if (x2 == x1 && y2 - y1 > 0) direction = Constants.MOVE_BOTTOM;
            else if (x2 - x1 < 0 && y2 - y1 > 0) direction = Constants.MOVE_LEFT_BOTTOM;
            else if (x2 - x1 < 0 && y2 - y1 <= 0) direction = Constants.MOVE_LEFT_TOP;
            return direction;
        }

        public static void FindPriority(int direction, ref List<int> priority)
        {
            //if (priority != null) priority.RemoveRange(0, 6);
            switch (direction)
            {
                case Constants.MOVE_TOP:
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_BOTTOM);
                    break;
                case Constants.MOVE_RIGHT_TOP:
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    break;
                case Constants.MOVE_RIGHT_BOTTOM:
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    break;
                case Constants.MOVE_BOTTOM:
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_TOP);
                    break;
                case Constants.MOVE_LEFT_BOTTOM:
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    break;
                case Constants.MOVE_LEFT_TOP:
                    priority.Add(Constants.MOVE_LEFT_TOP);
                    priority.Add(Constants.MOVE_LEFT_BOTTOM);
                    priority.Add(Constants.MOVE_TOP);
                    priority.Add(Constants.MOVE_BOTTOM);
                    priority.Add(Constants.MOVE_RIGHT_TOP);
                    priority.Add(Constants.MOVE_RIGHT_BOTTOM);
                    break;
            }
        }

        public static int GetBoxway(Box currentBox, Box baseBox, Box targetBox, ref List<Box> boxWay, int maxLength, int k, TacticState tacticState)
        {
            if (maxLength > 0
                && (int)Math.Sqrt((targetBox.x - currentBox.x) * (targetBox.x - currentBox.x) + ((targetBox.y - currentBox.y) * (targetBox.y - currentBox.y)) * 0.35) <= maxLength)
            {
                //boxDescription.Text = "" + currentBox.id;
                int currentDirection = FindDirection(currentBox.x, targetBox.x, currentBox.y, targetBox.y);

                if (k != 0)
                {
                    currentDirection = (currentDirection + k) % 6;
                    if (currentDirection == 0) currentDirection = 6;
                }

                List<int> currentPriority = new List<int>();
                FindPriority(currentDirection, ref currentPriority);
                Box nextWaybox = GetNextWaybox(currentBox, baseBox, ref currentPriority, ref boxWay, tacticState);

                if (nextWaybox == null) return -1;

                boxWay.Add(nextWaybox);
                if (nextWaybox.id == targetBox.id)
                {
                    return 0;
                }
                else
                {
                    if (GetBoxway(nextWaybox, baseBox, targetBox, ref boxWay, maxLength - 1, 0, tacticState) == -1)
                    {
                        int i = 1;
                        for (i = 1; i < 6; i++)
                        {
                            if (GetBoxway(nextWaybox, baseBox, targetBox, ref boxWay, maxLength - 1, i, tacticState) == -1)
                            {
                                continue;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        boxWay.Remove(boxWay[boxWay.Count - 1]);
                        //txtRedShips.Text = txtRedShips.Text + "\nоп\n";
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {

                return -1;
            }
        }

        public static Box GetNextWaybox(Box currentBox, Box baseBox, ref List<int> priority, ref List<Box> previousBoxes, TacticState tacticState)
        {
            Box nextWaybox = null;
            int j;
            for (int i = 0; i < 6; i++)
            {
                switch (priority[i])
                {
                    case Constants.MOVE_TOP:
                        if (currentBox.y <= 1) continue;
                        nextWaybox = tacticState.cMap.boxes[currentBox.id - 1];
                        break;
                    case Constants.MOVE_RIGHT_TOP:
                        if (currentBox.y == 0 || currentBox.x == tacticState.cMap.width - 1) continue;
                        nextWaybox = tacticState.cMap.getBoxByCoords(currentBox.x + 1, currentBox.y - 1);
                        break;
                    case Constants.MOVE_RIGHT_BOTTOM:
                        if (currentBox.x == tacticState.cMap.width - 1 || currentBox.y == tacticState.cMap.height * 2 - 1) continue;
                        nextWaybox = tacticState.cMap.getBoxByCoords(currentBox.x + 1, currentBox.y + 1);
                        break;
                    case Constants.MOVE_BOTTOM:
                        if (currentBox.y >= tacticState.cMap.height * 2 - 2) continue;
                        nextWaybox = tacticState.cMap.boxes[currentBox.id + 1];
                        break;
                    case Constants.MOVE_LEFT_BOTTOM:
                        if (currentBox.x == 0 || currentBox.y == tacticState.cMap.height * 2 - 1) continue;
                        nextWaybox = tacticState.cMap.getBoxByCoords(currentBox.x - 1, currentBox.y + 1);
                        break;
                    case Constants.MOVE_LEFT_TOP:
                        if (currentBox.x == 0 || currentBox.y == 0) continue;
                        nextWaybox = tacticState.cMap.getBoxByCoords(currentBox.x - 1, currentBox.y - 1);
                        break;
                }
                //if (nextWaybox != null && nextWaybox.id != previousBox.id && nextWaybox.spaceObject == null)
                if (nextWaybox != null && nextWaybox.id != baseBox.id && nextWaybox.spaceObject == null)
                {
                    for (j = 0; j < previousBoxes.Count; j++)
                    {
                        if (nextWaybox.id == previousBoxes[j].id) break;
                    }
                    if (j == previousBoxes.Count)
                    {
                        break;
                    }
                }
            }
            if (nextWaybox.spaceObject != null) nextWaybox = null;
            return nextWaybox;
        }

        public static double AttackAngleSearch(double targetx, double targety, TacticSeed seed, TacticState tacticState)
        {
            double angle = 0;
            double shipx, shipy;
            if (seed.activeShip != null)
            {
                shipx = tacticState.cMap.boxes[seed.activeShip.boxId].x;  // координаты выделенного корабля
                shipy = tacticState.cMap.boxes[seed.activeShip.boxId].y;
                if (shipx == targetx) // избегаем деления на ноль
                {
                    if (shipy > targety)
                    {
                        angle = -90;
                    }
                    else
                    {
                        angle = 90;
                    }
                    if (seed.activePlayer != 1) angle = -angle;

                }
                else // находим угол, на который нужно повернуть корабль (если он не равен 90 градусов)
                {
                    angle = Math.Atan((targety - shipy) / (targetx - shipx)) * 180 / Math.PI;
                }
                // дальше идет коррекция, не пытайся разобраться как это работает, просто оставь как есть
                if (seed.activePlayer == 1)
                {
                    if (shipy == targety && shipx > targetx)
                    {
                        angle = 180;
                    }
                    else if (shipx > targetx && shipy < targety)
                    {
                        angle += 180;
                    }
                    else if (shipx > targetx && shipy > targety)
                    {
                        angle = angle - 180;
                    }
                }
                else if (seed.activePlayer != 1)
                {
                    if (shipy == targety && shipx < targetx)
                    {
                        angle = 180;
                    }
                    else if (shipx < targetx && shipy < targety)
                    {
                        angle -= 180;
                    }
                    else if (shipx < targetx && shipy > targety)
                    {
                        angle += 180;
                    }
                }
            }
            return angle;
        }
    }
}
