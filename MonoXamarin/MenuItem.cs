using Microsoft.Xna.Framework;
using System;

namespace MonoXamarin
{
    /// <summary>
    /// MenuItme is used my Menu. Menu hsas list of MenuItems. Menu use the targetrectangle to 
    /// check id a menu is touched or clicked.
    /// </summary>
    //public class MenuItem
    //{
    //    public void SetTargetRectangle(float x, float y, int width, int height)
    //    {
    //        targetrectangle = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), width, height);
    //    }
    //    public Rectangle TargetRectangle
    //    {
    //        get {return targetrectangle;}
    //    }
    //    //tells us if menu item is linked to the menu or screen
    //    public string LinkType;
    //    public string LinkID;
    //    public Image Image;
    //    //Would like to give menu item a rectngle of its position
    //    Rectangle targetrectangle;

    //}
    public class MenuItem
    {
        //Screen can subscribe to the event 
        public event EventHandler OnLeftClickUP;
        public void SetTargetRectangle(float x, float y, int width, int height)
        {
            targetrectangle = new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), width, height);
        }
        public Rectangle TargetRectangle
        {
            get { return targetrectangle; }
        }
        //If the object is clicked fire the event
        public void LeftClickedUP()
        {
            //if not null invoke
            OnLeftClickUP?.Invoke(this, null);
        }

        //tells us if menu item is linked to the menu or screen
        public string LinkType;
        //LinkID is a new screen the new screen name 
        public string LinkID;
        //Image can be image link or text
        public Image Image;
        //Would like to give menu item a rectngle of its position
        Rectangle targetrectangle;
    }

}
