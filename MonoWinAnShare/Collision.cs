
//using System.Drawing;
using Microsoft.Xna.Framework;


namespace MonoWinAnShare
{
    /// <summary>
    /// This is called from collsion manager to check if the rectangle collide
    /// </summary>
//#if (__ANDROID__ || WINDOWS)

    //using System.Drawing;

    public class Collision
    {
        const int s_buffer = 15;//use to be 4. Up to increase collision sensitivity
        //Check fo 
        public static bool RectRect(Rectangle A, Rectangle B)
        {
            int aHBuf = A.Height / s_buffer;
            int aWBuf = A.Width / s_buffer;

            int bHBuf = B.Height / s_buffer;
            int bWBuf = B.Width / s_buffer;

            // if the bottom of A is less than the top of B - no collision
            if ((A.Y + A.Height) - aHBuf <= B.Y + bHBuf) { return false; }

            // if the top of A is more than the bottom of B = no collision
            if (A.Y + aHBuf >= (B.Y + B.Height) - bHBuf) { return false; }

            // if the right of A is less than the left of B - no collision
            if ((A.X + A.Width) - aWBuf <= B.X + bWBuf) { return false; }

            // if the left of A is more than the right of B - no collision
            if (A.X + aWBuf >= (B.X + B.Width) - bWBuf) { return false; }

            // otherwise there has been a collision
            return true;
        }

        public static bool RectPoint(Rectangle A, Vector2 point)
        {
            // if the bottom of A is less than point.Y - no collision
            //That means point.Y is underneath
            if ((A.Y + A.Height) <= point.Y) { return false; }
            // if the top of A is more than point.Y - no collision
            //It means that point.Y is above
            if (A.Y >= point.Y) { return false; }
            // if the right of A is less than point.Y - no collision
            //point is to the right
            if ((A.X + A.Width) <= point.X) { return false; }
            // if the left of A is more than point.Y - no collision
            //point is to the left
            if (A.X >= point.X) { return false; }
            // otherwise there has been a collision
            return true;
        }
    }
//#endif
}


