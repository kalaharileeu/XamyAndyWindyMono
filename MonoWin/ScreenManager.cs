using System;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoWinAnShare;
using System.Collections.Generic; 

namespace MonoWin
{
    /*Loading screens and chaning screens*/
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
                //if (letternumberMenu.Items[letternumberMenu.ItemNumber].LinkType == "Screen")
                if(item.LinkType == "Screen")
                    ChangeScreens(item.LinkID);
            }
        }

        public void ChangeScreens(String screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoWin." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 1.0f;
            //There is a screen transitioning happening
            setIsTransitioning(true);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            Image.LoadContent(this);
            
            ////*****************************Image and Imagebase swop**********************************
            ////Create a new Imgae from Xml deserialized Imagebase through copy constructor
            //var image = new Image(Image);
            ////reassign the Imgaebase with Image instance
            //Image = image;
            ////Now i can use LoadContent
            //(Image as Image).LoadContent(this);
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
}
