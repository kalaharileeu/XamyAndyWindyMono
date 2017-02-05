using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoWinAnShare;

namespace MonoWin
{
    /// <summary>
    /// This Screen will have ten letter and ten words naming those letters
    /// Not use a MenuManger like with Menu
    /// </summary>
    public class LetterNumberScreen : GameScreen
    {
        public LetterNumberScreen()
        {
            letternumberMenu = new LetterNumberMenu();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            XmlManager<LetterNumberMenu> xmlMenuManager = new XmlManager<LetterNumberMenu>();
            //The LetterNumberScreen load a meanu which is the layout and the content of the
            //screen. The menu also handles the input
            letternumberMenu = xmlMenuManager.Load("Content/PlayMenu.xml");//load the new menu
            letternumberMenu.LoadContent();
            //Subscribe to all the menu item events
            subscribe();
        }
        //Subscribe to menuitem events item events
        private void subscribe()
        {
            foreach(var menuitem in letternumberMenu.Items)
                if(menuitem.LinkID != null)
                    menuitem.OnLeftClickUP += Menuitem_OnLeftClickUP;
        }

        private void Menuitem_OnLeftClickUP(object sender, System.EventArgs e)
        {
            if (sender is MenuItem)
            {
                System.Diagnostics.Debug.WriteLine("Boom Event catched!!!");
                if (letternumberMenu.Items[letternumberMenu.ItemNumber].LinkType == "Screen")
                    ScreenManager.Instance.ChangeScreens((sender as MenuItem).LinkID);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            letternumberMenu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            letternumberMenu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            letternumberMenu.Draw(spriteBatch);
        }

        LetterNumberMenu letternumberMenu;
    }
}
