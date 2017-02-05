using Microsoft.Xna.Framework;

namespace MonoWin
{
    public class ImageEffect
    {
        protected Image image;
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

        public virtual void LoadContent(ref Image Image)
        {
            this.image = Image;
        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
