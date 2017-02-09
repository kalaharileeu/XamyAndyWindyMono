using System;
using System.Collections.Generic;

//using System.Xml.Serialization;

using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoWinAnShare;

namespace MonoWin
{
    public class Image : Imagebase
    {
        //ContentManager content;
        //Dictionary<string, ImageEffect> effectList;
        //public FadeEffect FadeEffect;
        //public SpriteSheetEffect SpriteSheetEffect;

        /// <summary>
        /// This constructor used when loading through xml deserialization
        /// </summary>
        public Image()
        {
          //  effectList = new Dictionary<string, ImageEffect>();
        }
        /// <summary>
        /// Use this constructor when constructing without Xml
        /// deserialization
        /// </summary>
        /// <param name="ImageSource">path to image</param>
        public Image(string ImageSource)
        {
            //effectList = new Dictionary<string, ImageEffect>();
            //source = ImageSource;
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

        public override void LoadContent(ScreenManagerbase screenmanager)
        {
            base.LoadContent(screenmanager);
        }

        public override void UnloadContent()
        {
            //content.Unload();
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
