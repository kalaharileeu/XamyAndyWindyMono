using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoWin
{
    /// <summary>
    /// Title screen is a simple class that drive the
    /// MenuManager. Is multi helper classes with Menu, Menu Item
    /// </summary>
    public class TitleScreen : GameScreen
    {
        MenuManager menuManager;

        public TitleScreen()
        {
            menuManager = new MenuManager();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //The menu manager load the content for menu to display
            //Calls MenuManger to load the content
            menuManager.LoadContent("Content/TitleMenu.xml");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menuManager.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            menuManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            menuManager.Draw(spriteBatch);
        }
    }
}
