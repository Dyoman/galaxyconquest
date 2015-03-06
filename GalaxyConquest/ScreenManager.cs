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
            else if (name == "settings")
            {
                currentScreen = new Screen_Settings(m_canvas);
            }
        }
    }
}
