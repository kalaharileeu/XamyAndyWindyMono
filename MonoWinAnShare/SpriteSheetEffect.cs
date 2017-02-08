using Microsoft.Xna.Framework;

namespace MonoWinAnShare
{
    public class SpriteSheetEffect : ImageEffect
    {
        public int FrameCounter;
        public int SwitchFrame;
        public Vector2 CurrentFrame;

        public int FrameWidth
        {
            get
            {
                if (image.Texture != null)
                    return image.Texture.Width / (int)AmountOfFrames.X;
                return 0;
            }
        }

        public int FrameHeight
        {
            get
            {
                if (image.Texture != null)
                    return image.Texture.Height / (int)AmountOfFrames.Y;
                return 0;
            }
        }

        public SpriteSheetEffect()
        {
            AmountOfFrames = new Vector2();//This is for player
            ///AmountOfFrames = amountofframes;
            CurrentFrame = new Vector2(1, 0);
            SwitchFrame = 200;//To make animation slower or faster
            FrameCounter = 0;
        }
        //send a refernece to the efffect to manipulate immage values
        public override void LoadContent(Imagebase Image)
        {
            base.LoadContent(Image);
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
                //<remarks>Framecounter is animation speed<remark>
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    CurrentFrame.X++;
                    //<remarks>added "-101" because only want first 4 frames</remarks> 
                    if (CurrentFrame.X * FrameWidth >= image.Texture.Width - 101)
                        CurrentFrame.X = 0;
                }
            }
            else
                CurrentFrame.X = 1;

            image.SourceRect = new Rectangle((int)CurrentFrame.X * FrameWidth,
                (int)CurrentFrame.Y * FrameHeight, FrameWidth, FrameHeight);//cast to int, rectangle takes int
        }
    }
}
