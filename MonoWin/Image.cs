using System;
using System.Collections.Generic;

//using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoWinAnShare;

namespace MonoWin
{
    public class Image : Imagebase
    {
        ContentManager content;
        //Dictionary<string, ImageEffect> effectList;
        //public FadeEffect FadeEffect;
        //public SpriteSheetEffect SpriteSheetEffect;

        /// <summary>
        /// This constructor used when loading through xml deserialization
        /// </summary>
        public Image()
        {
            effectList = new Dictionary<string, ImageEffect>();
        }
        /// <summary>
        /// Use this constructor when constructing without Xml
        /// deserialization
        /// </summary>
        /// <param name="ImageSource">path to image</param>
        public Image(string ImageSource)
        {
            effectList = new Dictionary<string, ImageEffect>();
            source = ImageSource;
        }
        //Copy the base image values to the newly created image, ib is
        //created by the XmlManager
        public Image(Imagebase ib)
        {
            //Have to rassign these eve though it might still just be defaults
            source = ib.source;
            Text = ib.Text;
            Effects = ib.Effects;
            FontName = ib.FontName;
            Position = ib.Position;
            Scale = ib.Scale;
            Alpha = ib.Alpha;
            SourceRect = ib.SourceRect;
            effectList = new Dictionary<string, ImageEffect>();
            FadeEffect = null;
            SpriteSheetEffect = null;
            amountofframes = ib.amountofframes;//default
        }
        ////ref needed here chaning the point to object
        //void SetEffect<T>(ref T effect)
        //{
        //    if (effect == null)
        //        effect = (T)Activator.CreateInstance(typeof(T));
        //    else
        //    {
        //        (effect as ImageEffect).IsActive = true;
        //        var obj = this;
        //        (effect as ImageEffect).LoadContent(obj);
        //    }
        //    if (!effectList.ContainsKey((effect.GetType().ToString().Replace("MonoWinAnShare.", ""))))
        //        effectList.Add(effect.GetType().ToString().Replace("MonoWinAnShare.", ""), (effect as ImageEffect));

        //}

        public override void ActivateEffect(string effect)
        {
            base.ActivateEffect(effect);
        }

        public override void DeactivateEffect(string effect)
        {
            base.DeactivateEffect(effect);
        }
        //Store the effects of the class in the effects string
        public override void StoreEffects()
        {
            base.StoreEffects();
        }

        public override void RestoreEffects()
        {
            base.RestoreEffects();
        }

        public void LoadContent(ScreenManager screenmanager)
        {
            content = new ContentManager(screenmanager.Content.ServiceProvider, "Content");
#if WINDOWS
            if (source != String.Empty)
                Texture = content.Load<Texture2D>(source);
#endif
#if __ANDROID__
            //Loading for android
            if (source != String.Empty)
            {
                using (var stream = TitleContainer.OpenStream(source))
                {
                    Texture = Texture2D.FromStream(ScreenManager.Instance.GraphicsDevice, stream);
                }
            }
#endif
            font = content.Load<SpriteFont>(FontName);

            Vector2 dimensions = Vector2.Zero;

            if (Texture != null)
                dimensions.X += Texture.Width;
            dimensions.X += font.MeasureString(Text).X;

            if (Texture != null)
                dimensions.Y = Math.Max(Texture.Height, font.MeasureString(Text).Y);
            else
                dimensions.Y = font.MeasureString(Text).Y;

            if (SourceRect == Rectangle.Empty)
                SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);

            renderTarget = new RenderTarget2D(screenmanager.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            screenmanager.GraphicsDevice.SetRenderTarget(renderTarget);
            //The screen is its own render target
            screenmanager.GraphicsDevice.Clear(Color.Transparent);
            screenmanager.Spritebatch.Begin();
            if (Texture != null)
                screenmanager.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);
            screenmanager.Spritebatch.DrawString(font, Text, Vector2.Zero, Color.White);
            screenmanager.Spritebatch.End();

            Texture = renderTarget;

            screenmanager.GraphicsDevice.SetRenderTarget(null);
            //Load the defaulf effects, they will be null initialy, activated to not active
            SetEffect<FadeEffect>(ref FadeEffect);
            SetEffect<SpriteSheetEffect>(ref SpriteSheetEffect);

            if (Effects != String.Empty)
            {
                string[] split = Effects.Split(':');
                foreach (string item in split)
                    ActivateEffect(item);
            }
        }

        public override void UnloadContent()
        {
            content.Unload();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        //this is mostly for single frame sprites or multiframe with spritesheet effects
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        //no image effects, but draw a specific frame in a multiframe sprite ex. for Scores
        public override void Draw(SpriteBatch spriteBatch, int framenumberx, int framenumbery)
        {
            base.Draw(spriteBatch, framenumberx, framenumbery);
        }
    }
}
