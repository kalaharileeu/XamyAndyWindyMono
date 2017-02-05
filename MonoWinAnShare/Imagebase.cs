using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace MonoWinAnShare
{
    public class Imagebase
    {
        [XmlAttribute]
        public string source;
        [XmlAttribute]
        public int width;//width in pixel
        [XmlAttribute]
        public int height;//height in pixel
        //public string Path;
        public float Alpha;
        public string Text, FontName;
        public Vector2 Position, Scale;
        public Rectangle SourceRect;
        public bool IsActive;
        [XmlIgnore]
        public Texture2D Texture;
        Vector2 origin;
        //ContentManager content;
        protected RenderTarget2D renderTarget;//Something clever?
        protected SpriteFont font;
        public string Effects;
        [XmlIgnore]
        public Vector2 amountofframes;
        /// <summary>
        /// This constructor used when loading through xml serialization
        /// </summary>
        public Imagebase()
        {
            source = Text = Effects = String.Empty;//source used to be path
            FontName = "Fonts/Arial50";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            //effectList = new Dictionary<string, ImageEffect>();
            amountofframes = new Vector2(5, 1);//This is the default value for player

        }
        /// <summary>
        /// Constructor without xml serialization
        /// </summary>
        /// <param name="ImageSource"></param>
        public Imagebase(string ImageSource)
        {
            source = ImageSource;
            Text = Effects = String.Empty;//source used to be path
            FontName = "Fonts/Arial50";
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Alpha = 1.0f;
            SourceRect = Rectangle.Empty;
            //effectList = new Dictionary<string, ImageEffect>();
           // amountofframes = new Vector2(5, 1);//This is the default value for player
        }
        public virtual void ActivateEffect(string effect){}

        public virtual void DeactivateEffect(string effect){}
        //Store the effects of the class in the effects string
        public virtual void StoreEffects(){}

        public virtual void RestoreEffects(){}

        public virtual void LoadContent(){}

        public virtual void UnloadContent()
        {
            //content.Unload();
            //foreach (var effect in effectList)
            //    DeactivateEffect(effect.Key);
        }

        public virtual void Update(GameTime gameTime){}

        //this is mostly for single frame sprites or multiframe with spritesheet effects
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(SourceRect.Width / 2, SourceRect.Height / 2);//this if mainly for a zoom affect
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha,
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }

        //no image effects, but draw a specific frame in a multiframe sprite ex. for Scores
        public virtual void Draw(SpriteBatch spriteBatch, int framenumberx, int framenumbery)
        {
            SourceRect = new Rectangle(framenumberx * width,
                framenumbery * height, width, height);//cast to int, rectangle takes int
            spriteBatch.Draw(Texture, Position + origin, SourceRect, Color.White * Alpha,
                0.0f, origin, Scale, SpriteEffects.None, 0.0f);
        }
    }
}
