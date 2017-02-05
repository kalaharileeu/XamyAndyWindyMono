using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using MonoWinAnShare;

namespace MonoXamarin
{
    /// <summary>
    /// LetterNumberMenu handles the layout and the UI for LetterNumberScreen
    /// </summary>
    public class LetterNumberMenu
    {
        public string Axis;
        public string Effects;
        //Because it is a list you have to XMl element
        [XmlElement("Item")]
        public List<MenuItem> Items;
        int itemNumber;
       // string id;
        //if a selection was done by mouse or finger
        //bool selected;

        public int ItemNumber
        {
            get { return itemNumber; }
        }

        //public bool Selected
        //{
        //    get { return selected; }
        //}
        //Constructor
        public LetterNumberMenu()
        {
            //id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            //Introduced for mouse and android
            //selected = false;
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
            foreach (MenuItem item in Items)
                item.Image.UnloadContent();
        }

        public void Update(GameTime gametime)
        {
           // selected = false;//Important tto reset here!
            for (int i = 0; i < Items.Count; i++)
            {
                //Image active means it will fade in and out
                Items[i].Image.IsActive = true;
                Items[i].Image.Update(gametime);
            }
#if WINDOWS
            //check for mouse button release
            if (InputManager.Instance.LeftButtonRelease())
            {
                //selected = false;
                System.Diagnostics.Debug.WriteLine("Left button release");
                for (int i = 0; i < Items.Count; i++)
                {
                    //Check check if the item rectangle and finger touch collision
                    if (Collision.RectPoint(Items[i].TargetRectangle,
                        InputManager.Instance.LeftButtonReleasePostion()))
                    {
                        //fire the event for the item
                        Items[i].LeftClickedUP();
                    }
                }
            }
#endif
#if __ANDROID__
            //Checif the finger was lifted
            if (InputManager.Instance.TouchRelease())
            {
                //Check with item was touched
                for (int i = 0; i < Items.Count; i++)
                {
                    //Check check if the item rectangle and finger touch collision
                    if (Collision.RectPoint(Items[i].TargetRectangle, 
                        InputManager.Instance.TouchReleaseVector))
                    {
                        //fire the event for the item
                        Items[i].LeftClickedUP();
                        //itemNumber = i;
                        //selected = true;
                        System.Diagnostics.Debug.WriteLine("2boom");
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
        //Layout here
        void AlignMeniItems()
        {
            //18 items for this screen so 6 X 3 spacing. 6 wide, 3 high
            Vector2 layoutstack = new Vector2(6, 3);
            //This is the buffer from for y form the top
            //This space will be taken up by something else: A heading? 
            int yTopBuffer = 50;
            //MAX Length and width. find the max item width from all the item inages
            var maxwidth = Items.Max(x => x.Image.SourceRect.Width);
            var maxheight = Items.Max(x => x.Image.SourceRect.Height);

            //Below is the total area that all the text will take in 6 X 3 format
            Vector2 layoutArea = new Vector2(layoutstack.X * maxwidth, layoutstack.Y * maxheight);
            //blow is the width of the frame around the total area of text
            Vector2 frame = new Vector2((ScreenManager.Instance.Dimensions.X - layoutArea.X) / 2,
                (ScreenManager.Instance.Dimensions.Y - layoutArea.Y) / 2);
            //The layout mostly based on layoutstack Vector(6, 3), maxwidth, maxheight   
            //ILP = Item List Position
            int ILP = -1;
            ///Find the item with the longest lenth, the space all towards that length
            for (int i = 0; i < layoutstack.X; i++)
            {
                //create a copy of frame, going to modify it, this will
                //be modified in the next loop
                Vector2 framedimension = new Vector2(frame.X, frame.Y);
                for (int j = 0; j < layoutstack.Y; j++)
                {
                    ILP++;//Item List Posiition
                    //check that the item count does not go higher that the items in Items
                    if (ILP > (Items.Count - 1))
                        ILP = Items.Count - 1;
                    //To center the text in the middle of the max text width. Add (Maxwitdh - realwidth / 2)
                    int tocenter = (maxwidth - Items[ILP].Image.SourceRect.Width) / 2;
                    Items[ILP].Image.Position = new Vector2(frame.X + tocenter, framedimension.Y);
                    //Shift Y coordinates down
                    framedimension += new Vector2(0, maxheight);
                    //Set the menuitem collsion rectangle
                    Items[ILP].SetTargetRectangle(Items[ILP].Image.Position.X, Items[ILP].Image.Position.Y,
                        Items[ILP].Image.SourceRect.Width, Items[ILP].Image.SourceRect.Height);
                }
                //Shift x to the right
                frame += new Vector2(maxwidth, 0);
            }
        }
    }
}
