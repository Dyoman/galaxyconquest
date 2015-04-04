using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML;
using Gwen;
using Gwen.Control;

namespace GalaxyConquest
{
    /// <summary>
    /// Class for handling game screen switching
    /// </summary>
    public class ScreenManager
    {
        private Base currentScreen = null;              //Current game screen
        private Canvas m_canvas = null;

        public ScreenManager(Canvas canvas)
        {
            m_canvas = canvas;            
        }

        public void LoadScreen(string name)
        {
            if(currentScreen != null)
            {
                //clear all controls
                m_canvas.Children.Clear();
                currentScreen.Dispose();
            }

            if (name == "mainmenu")
            {
                currentScreen = new Screen_MainMenu(m_canvas);
            }
            else if (name == "newgame")
            {
                currentScreen = new Screen_NewGame(m_canvas);
            }
            else if (name == "planet")
            {
                currentScreen = new ScreenPlanet(m_canvas);
            }
            else if (name == "gamescreen")
            {
                currentScreen = new Screen_GameScreen(m_canvas);
                currentScreen.KeyboardInputEnabled = true;
                
            }
            else if (name == "settings")
            {
                currentScreen = new Screen_Settings(m_canvas);
            }
            else if (name == "techtree")
            {
                currentScreen = new Screen_TechTree(m_canvas);
            }
            else if (name == "solarSystem")
            {
                currentScreen = new ScreenSolarSystem(m_canvas);
            }
            else if (name == "combat")
            {
                currentScreen = new Screen_Combat(m_canvas);
            } 
        }

        void currentScreen_Clicked(Base sender, ClickedEventArgs arguments)
        {
            System.Windows.Forms.MessageBox.Show(arguments.X.ToString() + "," + arguments.Y.ToString());
        }
    }
}
