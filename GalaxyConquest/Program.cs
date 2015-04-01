using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Gwen.Control;
using SFML.Graphics;
using SFML.Window;
using Tao.OpenGl;
using KeyEventArgs = SFML.Window.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

using System.Runtime.InteropServices;

using GalaxyConquest.Drawing;
using GalaxyConquest.Game;
using GalaxyConquest.SpaceObjects;

namespace GalaxyConquest
{
    public static class Program
    {
        private static Gwen.Input.SFML.GwenInput m_Input;    //SFML inputs
        private static RenderWindow m_Window;               //SFML window
        private static Canvas m_Canvas;                     //GWEN canvas

        public static ScreenManager screenManager;
        public static bool quitFlag = false;

        public static int width = 800;
        public static int height = 600;



        public static Gwen.Font fontLogo;
        public static Gwen.Font fontText;
        public static Gwen.Font fontButtonLabels;


        public static GameState Game;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            {


                // Create main window
                m_Window = new RenderWindow(new VideoMode((uint)width, (uint)height), "GalaxyConquest", Screen_Settings.fullScreen ? Styles.Fullscreen : (Styles.Close | Styles.Resize) | Styles.Titlebar, new ContextSettings(32, 0));

                // Setup event handlers
                m_Window.Closed += OnClosed;
                m_Window.KeyPressed += OnKeyPressed;
                m_Window.Resized += OnResized;
                m_Window.KeyReleased += window_KeyReleased;
                m_Window.MouseButtonPressed += window_MouseButtonPressed;
                m_Window.MouseButtonReleased += window_MouseButtonReleased;
                m_Window.MouseWheelMoved += window_MouseWheelMoved;
                m_Window.MouseMoved += window_MouseMoved;
                m_Window.TextEntered += window_TextEntered;

                m_Window.SetFramerateLimit(60);


                // Create a sprite for the background
                Sprite background = new Sprite(new global::SFML.Graphics.Texture("background.jpg"));
                background.Texture.Repeated = true;


                // Disable lighting
                Gl.glDisable(Gl.GL_LIGHTING);
                // Configure the viewport (the same size as the window)
                Gl.glViewport(0, 0, (int)m_Window.Size.X, (int)m_Window.Size.Y);


                const int fps_frames = 50;
                List<long> ftime = new List<long>(fps_frames);
                long lastTime = 0;

                // create GWEN renderer
                Gwen.Renderer.SFML.GwenRenderer gwenRenderer = new Gwen.Renderer.SFML.GwenRenderer(m_Window);

                // Create GWEN skin
                //Skin.Simple skin = new Skin.Simple(GwenRenderer);
                Gwen.Skin.TexturedBase skin = new Gwen.Skin.TexturedBase(gwenRenderer, "DefaultSkin.png");

                // set default font
                Gwen.Font defaultFont = new Gwen.Font(gwenRenderer) { Size = 10, FaceName = "OpenSans.ttf" };

                // try to load, fallback if failed
                if (gwenRenderer.LoadFont(defaultFont))
                {
                    gwenRenderer.FreeFont(defaultFont);
                }

                skin.SetDefaultFont(defaultFont.FaceName);
                defaultFont.Dispose(); // skin has its own

                // Create a Canvas (it's root, on which all other GWEN controls are created)
                m_Canvas = new Canvas(skin);
                m_Canvas.SetSize(width, height);
                //                m_Canvas.ShouldDrawBackground = true;
                //                m_Canvas.BackgroundColor = System.Drawing.Color.FromArgb(255, 150, 170, 170);
                m_Canvas.KeyboardInputEnabled = true;
                m_Canvas.ShouldDrawBackground = false;
                m_Canvas.DrawDebugOutlines = false;

                // Create GWEN input processor
                m_Input = new Gwen.Input.SFML.GwenInput(m_Window, m_Canvas);






                fontLogo = new Gwen.Font(gwenRenderer);
                fontLogo.FaceName = "OpenSans.ttf";
                fontLogo.Size = 35;

                fontText = new Gwen.Font(gwenRenderer);
                fontText.FaceName = "OpenSans.ttf";
                fontText.Size = 15;

                fontButtonLabels = new Gwen.Font(gwenRenderer);
                fontButtonLabels.FaceName = "OpenSans.ttf";
                fontButtonLabels.Size = 25;





                /*Gwen.Control.StatusBar m_StatusBar;
                m_StatusBar = new Gwen.Control.StatusBar(m_Canvas);
                m_StatusBar.Dock = Gwen.Pos.Bottom;
                */
                double Fps = 0.0; // fps to display

                screenManager = new ScreenManager(m_Canvas);
                screenManager.LoadScreen("mainmenu");


                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                while (m_Window.IsOpen() && !quitFlag)
                {
                    m_Window.SetActive();
                    m_Window.DispatchEvents();
                    m_Window.Clear();

                    m_Window.Draw(background);




                    /////////////////////////

                    Gl.glClearColor(0f, 0f, 0f, 0f);

                    Gl.glBegin(Gl.GL_QUADS);

                    float x = 100.0f;
                    float y = 100.0f;

                    Gl.glVertex2f(x + 0f, y + 0f);
                    Gl.glVertex2f(x + 0f, y + 1f);
                    Gl.glVertex2f(x + 1f, y + 1f);
                    Gl.glVertex2f(x + 1f, y + 0f);

                    Gl.glEnd();

                    //m_Window.SwapBuffers();





                    if (ftime.Count == fps_frames)
                        ftime.RemoveAt(0);

                    ftime.Add(stopwatch.ElapsedMilliseconds - lastTime);
                    lastTime = stopwatch.ElapsedMilliseconds;

                    if (stopwatch.ElapsedMilliseconds > 1000)
                    {
                        Fps = 1000f * ftime.Count / ftime.Sum();
                        stopwatch.Restart();
                    }
                    //m_StatusBar.Text = "GalaxyConquest v.0.1 - " + Math.Round(Fps, 2).ToString() + " fps";

                    // render GWEN canvas
                    m_Canvas.RenderCanvas();

                    m_Window.Display();
                }

                fontLogo.Dispose();
                fontText.Dispose();
                fontButtonLabels.Dispose();

                // we only need to dispose the canvas, it will take care of disposing all its children
                // and current game screen
                m_Canvas.Dispose();

                // also dispose of these
                skin.Dispose();
                gwenRenderer.Dispose();
            }
            /*catch (Exception e)
            {
                String msg = String.Format("Exception: {0}\n{1}", e.Message, e.StackTrace);
                MessageBox.Show(msg);
            }*/

            m_Window.Dispose();
        }

        public static int percentW(int percent)
        {
            return width * percent / 100;
        }

        public static int percentH(int percent)
        {
            return height * percent / 100;
        }

        public static void applyVideoSettings()
        {
            m_Window.Dispose();
            Main();
        }

        #region RedirectsToGwen

        static void window_TextEntered(object sender, TextEventArgs e)
        {
            m_Input.ProcessMessage(e);
        }

        static void window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            m_Input.ProcessMessage(e);
        }

        static void window_MouseWheelMoved(object sender, MouseWheelEventArgs e)
        {
            m_Input.ProcessMessage(e);
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            m_Input.ProcessMessage(new Gwen.Input.SFML.GwenMouseButtonEventArgs(e, true));
        }

        static void window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            m_Input.ProcessMessage(new Gwen.Input.SFML.GwenMouseButtonEventArgs(e, false));
        }

        static void window_KeyReleased(object sender, KeyEventArgs e)
        {
            m_Input.ProcessMessage(new Gwen.Input.SFML.GwenKeyEventArgs(e, false));
        }

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            m_Window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
                m_Window.Close();

            if (e.Code == Keyboard.Key.F12)
            {
                Image img = m_Window.Capture();
                if (img.Pixels == null)
                {
                    MessageBox.Show("Failed to capture window");
                }
                string path = String.Format("screenshot-{0:D2}{1:D2}{2:D2}.png", DateTime.Now.Hour, DateTime.Now.Minute,
                                            DateTime.Now.Second);
                if (!img.SaveToFile(path))
                    MessageBox.Show(path, "Failed to save screenshot");
                img.Dispose();
            }
            else
                m_Input.ProcessMessage(new Gwen.Input.SFML.GwenKeyEventArgs(e, true));
        }

        /// <summary>
        /// Function called when the window is resized
        /// </summary>
        static void OnResized(object sender, SizeEventArgs e)
        {
            m_Window.SetView(new View(new FloatRect(0f, 0f, e.Width, e.Height)));
            m_Canvas.SetSize((int)e.Width, (int)e.Height);
        }
        #endregion
    }
}
