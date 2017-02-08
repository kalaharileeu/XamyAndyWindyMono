using System;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace MonoWinAnShare
{
    public class GameScreen
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;

        public GameScreen()
        {

        }

        public void SetContent(ContentManager contentmanager)
        {
            content = new ContentManager(contentmanager.ServiceProvider, "Content");
            //content = new ContentManager(screenmanager.Content.ServiceProvider, "Content");
        }

        public virtual void LoadContent(ScreenManagerbase sm)
        {

        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            //TODO: If screenmanager transitioning then do not update
            InputManager.Instance.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
