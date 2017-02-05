using Microsoft.Xna.Framework;

namespace MonoWin
{
    public class FadeEffect : ImageEffect
    {
        public float Fadespeed;
        public bool Increase;

        public FadeEffect()
        {
            Fadespeed = 1;
            Increase = false;
        }

        public override void LoadContent(ref Image Image)
        {
            base.LoadContent(ref Image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (image.IsActive)
            {
                if (!Increase)
                    image.Alpha -= Fadespeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                else
                    image.Alpha += Fadespeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (image.Alpha < 0.0f)
                {
                    Increase = true;
                    image.Alpha = 0.0f;
                }
                else if (image.Alpha > 1.0f)
                {
                    Increase = false;
                    image.Alpha = 1.0f;
                }
            }
            else
                image.Alpha = 1.0f;
        }
    }
}
