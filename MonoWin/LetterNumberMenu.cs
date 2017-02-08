using Microsoft.Xna.Framework;

using MonoWinAnShare;

namespace MonoWin
{
    /// <summary>
    /// LetterNumberMenu handles the layout and the UI for LetterNumberScreen
    /// </summary>
    public class LetterNumberMenu : LetterNumberMenubase
    {
        //Constructor
        public LetterNumberMenu(){}

        public override void LoadContent(ScreenManagerbase screenmanagerbase)
        {
            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                //Load a new Image from xml loaded baseImage
                var image = new Image(item.Image);
                //set the old baseImage to the new Image
                ((item.Image = image) as Image).LoadContent(screenmanagerbase as ScreenManager);
                foreach (string s in split)
                    item.Image.ActivateEffect(s);
            }
            AlignMeniItems((screenmanagerbase as ScreenManager).Dimensions.X, (screenmanagerbase as ScreenManager).Dimensions.Y);
        }

        public override void UnloadContent()
        {
            //selected = false;
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
        }
    }
}
