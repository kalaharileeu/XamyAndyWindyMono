using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
using MonoWinAnShare;

namespace MonoXamarin
{
    //This class has Android and Windows specific code
    public class Menu
    {
        public event EventHandler OnMenuChange;

        public string Axis;
        public string Effects;
        //Because it is a list you have to XMl element
        [XmlElement("Item")]
        public List<MenuItem> Items;
        int itemNumber;
        string id;
        //if a selection was done by mouse or finger
        bool selected;

        public int ItemNumber
        {
            get { return itemNumber; }
        }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                OnMenuChange(this, null);
            }
        }

        public bool Selected
        {
            get { return selected; }
        }

        public void Transition(float alpha)
        {
            foreach (MenuItem item in Items)
            {
                item.Image.IsActive = true;
                item.Image.Alpha = alpha;
                if (alpha == 0.0f)
                    item.Image.FadeEffect.Increase = true;
                else
                    item.Image.FadeEffect.Increase = false;
            }
        }

        void AlignMeniItems()
        {
            Vector2 dimensions = Vector2.Zero;
            //add all the dimensions together to get total dimension
            foreach (MenuItem item in Items)
                dimensions += new Vector2(item.Image.SourceRect.Width, 
                    item.Image.SourceRect.Height);
            //further edit dimension to give the beffer spacing on the screen
            dimensions = new Vector2((ScreenManager.Instance.Dimensions.X -
                dimensions.X) / 2, (ScreenManager.Instance.Dimensions.Y - dimensions.Y) / 2);

            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                    item.Image.Position = new Vector2(dimensions.X,
                        (ScreenManager.Instance.Dimensions.Y - item.Image.SourceRect.Height) / 2);
                else if (Axis == "Y")
                    item.Image.Position = new Vector2((ScreenManager.Instance.Dimensions.X -
                        item.Image.SourceRect.Width) / 2, dimensions.Y);
                //Shift the dimensions value to the next value to fall below or to left of previous
                dimensions += new Vector2(item.Image.SourceRect.Width,
                    item.Image.SourceRect.Height);
                //Set the menuitem collsion rectangle
                item.SetTargetRectangle(
                    item.Image.Position.X, 
                    item.Image.Position.Y,
                    item.Image.SourceRect.Width,
                    item.Image.SourceRect.Height);
            }
        }

        public Menu()
        {
            id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            //Introduced for mouse and android
            selected = false;
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');
            foreach (MenuItem item in Items)
            {
                item.Image.LoadContent();
                foreach (string s in split)
                    item.Image.ActivateEffect(s);
            }
            AlignMeniItems();
        }

        public void UnloadContent()
        {
            selected = false;
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public void Update(GameTime gametime)
        {
#if !__ANDROID__
            //This sequence changes the itmenumber as the keys are pressed, the 
            //itemnumber will be fed back to MenuManger if the Enter key is pressed 
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                    itemNumber--;
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                    itemNumber++;
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                    itemNumber--;
            }
            //Check that the item numbers are within the bounds of
            //the amount of inumbers
            if (itemNumber < 0)
                itemNumber = 0;
            else if (itemNumber > Items.Count - 1)
                itemNumber = Items.Count - 1;

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                    Items[i].Image.IsActive = true;
                else
                    Items[i].Image.IsActive = false;

                Items[i].Image.Update(gametime);
            }
            //check for mouse button release
            if(InputManager.Instance.LeftButtonRelease())
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    //Check check if the item rectangle and finger touch collision
                    if (Collision.RectPoint(Items[i].TargetRectangle, 
                        InputManager.Instance.LeftButtonReleasePostion()))
                    {
                        itemNumber = i;
                        selected = true;
                        System.Diagnostics.Debug.WriteLine("Boom!!!");
                    }
                }
            }
#endif
#if __ANDROID__
            //update and flash all the images in android
            for (int i = 0; i < Items.Count; i++)
            {
                //Image active means it will fade in and out
                Items[i].Image.IsActive = true;
                Items[i].Image.Update(gametime);
            }
            if (InputManager.Instance.TouchRelease())
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    //Check check if the item rectangle and finger touch collision
                    if (Collision.RectPoint(Items[i].TargetRectangle, InputManager.Instance.TouchReleaseVector))
                    {
                        itemNumber = i;
                        selected = true;
                    }
                }
            }
#endif
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem item in Items)
                item.Image.Draw(spriteBatch);
        }
    }
}
