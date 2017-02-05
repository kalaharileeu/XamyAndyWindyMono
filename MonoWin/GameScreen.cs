using System;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoWin
{
    public class GameScreen
    {
        protected ContentManager content;
        [XmlIgnore]
        public Type Type;
        public string XmlPath;

        public GameScreen()
        {
            Type = this.GetType();//Gets the type 
            XmlPath = "Content/" + Type.ToString().Replace("MonoWin.", "") + ".xml";
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
