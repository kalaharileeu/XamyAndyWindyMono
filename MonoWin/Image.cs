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
        Dictionary<string, ImageEffect> effectList;
        public FadeEffect FadeEffect;
        public SpriteSheetEffect SpriteSheetEffect;

        /// <summary>
        /// This constructor used when loading through xml serialization
        /// </summary>
        public Image()
        {
            effectList = new Dictionary<string, ImageEffect>();
        }
        /// <summary>
        /// Use this constructor with XML file serialization
        /// </summary>
        /// <param name="ImageSource"></param>
        public Image(string ImageSource)
        {
            effectList = new Dictionary<string, ImageEffect>();
        }

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
        //ref needed here chaning the point to object
        void SetEffect<T>(ref T effect)
        {
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            else
            {
                (effect as ImageEffect).IsActive = true;
                var obj = this;
                (effect as ImageEffect).LoadContent(ref obj);
            }
            if (!effectList.ContainsKey((effect.GetType().ToString().Replace("MonoWin.", ""))))
                effectList.Add(effect.GetType().ToString().Replace("MonoWin.", ""), (effect as ImageEffect));

        }

        public override void ActivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = true;
                var obj = this;
                effectList[effect].LoadContent(ref obj);
                if (effect == "SpriteSheetEffect")
                    effectList[effect].SetAmountFrames(this.amountofframes);
            }
        }

        public override void DeactivateEffect(string effect)
        {
            if (effectList.ContainsKey(effect))
            {
                effectList[effect].IsActive = false;
                effectList[effect].UnloadContent();
            }
        }
        //Store the effects of the class in the effects string
        public override void StoreEffects()
        {
            Effects = String.Empty;
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    Effects += effect.Key + ":";
            }
            if (Effects != String.Empty)
                Effects.Remove(Effects.Length - 1);
        }

        public override void RestoreEffects()
        {
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);

            string[] split = Effects.Split(':');
            foreach (string s in split)
                ActivateEffect(s);
        }

        public override void LoadContent()
        {
            System.Diagnostics.Debug.WriteLine("LoadContent Image!");
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
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

            renderTarget = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice,
                (int)dimensions.X, (int)dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);
            //The screen is its own render target
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.Spritebatch.Begin();
            if (Texture != null)
                ScreenManager.Instance.Spritebatch.Draw(Texture, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.DrawString(font, Text, Vector2.Zero, Color.White);
            ScreenManager.Instance.Spritebatch.End();

            Texture = renderTarget;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);

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
            foreach (var effect in effectList)
                DeactivateEffect(effect.Key);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var effect in effectList)
            {
                if (effect.Value.IsActive)
                    effect.Value.Update(gameTime);
            }
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
