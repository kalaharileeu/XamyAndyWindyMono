using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoWinAnShare;
using System.Collections.Generic;

namespace MonoXamarin
{
    public class ScreenManager : ScreenManagerbase
    {
        private XmlManager<GameScreen> xmlGameScreenManager;

        private ScreenManager()
        {
            Dimensions = new Vector2(1280, 720);//Galaxy S3 screen resolution
            //START with: The current screen is tile screen
            //currentScreen = new TitleScreen();
            currentScreen = new LetterNumberScreen();
            xmlGameScreenManager = new XmlManager<GameScreen>();
            xmlGameScreenManager.Type = currentScreen.Type;
            IsTransitioning = false;
        }

        //Sunscribsion registration from then currentscreen
        public void subscribe(List<MenuItem> items_to_register)
        {
            foreach (var menuitem in items_to_register)
                if (menuitem.LinkID != null)
                    menuitem.OnLeftClickUP += Menuitem_OnLeftClickUP;
        }
        //Triggered on event from menu item
        private void Menuitem_OnLeftClickUP(object sender, EventArgs e)
        {
            if (sender is MenuItem)
            {
                var item = sender as MenuItem;
                System.Diagnostics.Debug.WriteLine("Boom Event catched!!!");
                if (item.LinkType == "Screen")
                    ChangeScreens(item.LinkID);
            }
        }

        public override void ChangeScreens(String screenName)
        {
            base.ChangeScreens(screenName);
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoXamarin." + screenName));
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            //string i = Image.Text;
            //Image.LoadContent(this);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }



    //public class ScreenManager : ScreenManagerbase
    //{
    //    private static ScreenManager instance;

    //    //[XmlIgnore]//Can make dimensions below public and load it through Xml
    //    //public Vector2 Dimensions { private set; get; }
    //    //[XmlIgnore]
    //    //public ContentManager Content { private set; get; }
    //    private XmlManager<GameScreen> xmlGameScreenManager;

    //    //GameScreen currentScreen, newScreen;
    //    //[XmlIgnore]
    //    //public GraphicsDevice GraphicsDevice;
    //    //[XmlIgnore]
    //    //public SpriteBatch Spritebatch;

    //    //public Image Image;//this has todo with screen change
    //    //[XmlIgnore]
    //    //public bool IsTransitioning { private set; get; }

    //    private ScreenManager()
    //    {
    //        int height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    //        int width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    //        Image = new Image("Content/ScreenManager/FadeImage.png");
    //        Image.Scale = new Vector2(800, 480);
    //        Image.Effects = "FadeEffect";
    //        Dimensions = new Vector2(width, height);
    //        //currentScreen = new LetterNumberScreen();
    //        currentScreen = new LetterNumberScreen();
    //        xmlGameScreenManager = new XmlManager<GameScreen>();
    //        xmlGameScreenManager.Type = currentScreen.Type;
    //        //currentScreen = xmlGameScreenManager.Load("Content/TitleScreen.xml");
    //    }

    //    public static ScreenManager Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new ScreenManager();
    //                ////ScreenManager.xml used to be loaded here
    //            }
    //            return instance;
    //        }
    //    }
    //    //this is to create another screen for a screen change
    //    public void ChangeScreens(String screenName)
    //    {
    //        System.Diagnostics.Debug.WriteLine("Screen Manager Change screen");
    //        //new screen values comes from XML
    //        newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoXamarin." + screenName));
    //        Image.IsActive = true;
    //        Image.FadeEffect.Increase = true;
    //        Image.Alpha = 1.0f;
    //        IsTransitioning = true;
    //    }

    //    void Transition(GameTime gameTime)
    //    {
    //        if (IsTransitioning)
    //        {
    //            /**In ChangeScreen function  Alpha get sets to zero. Then Image update will 
    //             * will increase it upwards */
    //            Image.Update(gameTime);
    //            if (Image.Alpha == 1.0f)
    //            {
    //                System.Diagnostics.Debug.WriteLine(Image.Alpha);
    //                currentScreen.UnloadContent();
    //                currentScreen = newScreen;
    //                xmlGameScreenManager.Type = currentScreen.Type;
    //                if (File.Exists(currentScreen.XmlPath))//If this path exists
    //                    currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
    //                currentScreen.LoadContent();
    //                System.Diagnostics.Debug.WriteLine("Screen Manager Transition!!!!!!!!!!");
    //            }
    //            else if (Image.Alpha == 0.0f)
    //            {
    //                System.Diagnostics.Debug.WriteLine("Screen Manager Transition Doen!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    //                Image.IsActive = false;
    //                IsTransitioning = false;
    //            }
    //        }
    //    }

    //    public void LoadContent(ContentManager Content)
    //    {
    //        this.Content = new ContentManager(Content.ServiceProvider, "Content");
    //        currentScreen.LoadContent();
    //        Image.LoadContent();
    //    }

    //    public void UnloadContent()
    //    {
    //        currentScreen.UnloadContent();
    //        Image.UnloadContent();
    //    }

    //    public void Update(GameTime gameTime)
    //    {
    //        currentScreen.Update(gameTime);
    //        Transition(gameTime);
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        currentScreen.Draw(spriteBatch);
    //        if (IsTransitioning)
    //            Image.Draw(spriteBatch);
    //    }
    //}
}
