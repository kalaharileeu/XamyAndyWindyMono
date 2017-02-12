using System;

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
                if(item.LinkType == "Screen")
                    ChangeScreens(item.LinkID);
            }
        }

        public override void ChangeScreens(String screenName)
        {
            base.ChangeScreens(screenName);
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoWin." + screenName));
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
}
