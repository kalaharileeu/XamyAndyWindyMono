using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;
using System;

namespace MonoWinAnShare
{
    public class ScreenManagerbase
    {
        [XmlIgnore]//Can make dimensions below public and load it through Xml
        public Vector2 Dimensions { protected set; get; }
        [XmlIgnore]
        public ContentManager Content { protected set; get; }
        //protected XmlManager<GameScreen> xmlGameScreenManager;
        protected GameScreen currentScreen, newScreen;
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch Spritebatch;
        public Imagebase Image;//this has todo with screen change
        [XmlIgnore]
        protected bool IsTransitioning;// { private set; get; }

        public ScreenManagerbase()
        {
        }

        //set transitioning variable for affected instances
        //gets called at start and end of transition
        protected void setIsTransitioning(bool isOrNot)
        {
            IsTransitioning = isOrNot;
            InputManager.Instance.SetisTransitioning(isOrNot);
        }

        public virtual void ChangeScreens(String screenName)
        {
            //newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoWinAnShare." + screenName));
            Image.IsActive = true;
            Image.FadeEffect.Increase = true;
            Image.Alpha = 1.0f;
            //There is a screen transitioning happening
            setIsTransitioning(true);
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
                    currentScreen.LoadContent(this);
                }
                else if (Image.Alpha == 0.0f)
                {
                    Image.IsActive = false;
                    //Screen transition stop
                    setIsTransitioning(false);
                }
            }
        }

        public virtual void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(this);
            Image.LoadContent(this);
        }
        public virtual void UnloadContent()
        {
            currentScreen.UnloadContent();
            Image.UnloadContent();
        }
        public virtual void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            //If it is transitioning draw the transition image
            if (IsTransitioning)
                Image.Draw(spriteBatch);
        }

    }
}
