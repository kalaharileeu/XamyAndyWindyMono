using Microsoft.Xna.Framework;

namespace MonoWinAnShare
{
    public class ImageEffect
    {
        protected Imagebase image;
        public bool IsActive;
        public Vector2 AmountOfFrames;

        public void SetAmountFrames(Vector2 value)
        {
            AmountOfFrames.X = value.X;
            AmountOfFrames.Y = value.Y;
        }

        public ImageEffect()
        {
            IsActive = false;
        }

        public virtual void LoadContent(Imagebase Image)
        {
            image = Image;
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
