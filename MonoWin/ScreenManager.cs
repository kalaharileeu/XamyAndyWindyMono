using System;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoWinAnShare;

//using MonoWinAnShare; 

namespace MonoWin
{
    /*Loading screens and chaning screens*/
    public class ScreenManager
    {
        private static ScreenManager instance;

        [XmlIgnore]//Can make dimensions below public and load it through Xml
        public Vector2 Dimensions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        private XmlManager<GameScreen> xmlGameScreenManager;
        GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch Spritebatch;
        public Image Image;//this has todo with screen change
        [XmlIgnore]
        public bool IsTransitioning { private set; get; }

        private ScreenManager()
        {
            Dimensions = new Vector2(1280, 720);//Galaxy S3 screen resolution
            //The current screen is tile screen
            currentScreen = new TitleScreen();
            //currentScreen = new LetterNumberScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            //currentScreen = xmlGameScreenManager.Load("Content/SplashScreen.xml");// TitleScreen.SplashScreen
        }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    //Creen manager request a instance of screenmaanger through xml manager
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    instance = xml.Load("Content/ScreenManager.xml");
                }
                return instance;
            }
        }
        //this is to create another screen for a screen change
        public void ChangeScreens(String screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoWin." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 1.0f;
            //There is a screen transitioning happening
            IsTransitioning = true;
        }

        void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                /**In ChangeScreen function  Alpha get sets to zero. Then Image update will 
                 * will increase it upwards */
                Image.Update(gameTime);
                if (Image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    //Set the xml manager to the current screen type
                    xmlGameScreenManager.Type = currentScreen.Type;
                    //if there is xml file available for the current screen name
                    if (File.Exists(currentScreen.XmlPath))//If this path exists
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    currentScreen.LoadContent();
                }
                else if (Image.Alpha == 0.0f)
                {
                    System.Diagnostics.Debug.WriteLine("Screen Manager Transition Doen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent();
            Image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }

    }
}
