using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

#if __ANDROID__
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace MonoWinAnShare
{
    /// <summary>
    /// Inputmanager get polled constantly to check for keyboard and mouse updates
    /// then all the variables gets populated from the update function
    /// then pogram the also have to interogate inputmanager to find out which
    /// key or mouse button was pressed, this can be apporved
    /// </summary>
    public class InputManager
    {
        private static InputManager instance;
        //This can be set by the screenmanager if it is transitioning
        private bool isTransitioning;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                    //initialize to false on start up
                    instance.isTransitioning = false;
                }
                return instance;
            }
        }

        public void SetisTransitioning(bool isOrNot)
        {
            isTransitioning = isOrNot;
        }

        InputManager()
        {
            touchReleaseVector = new Vector2(-1, -1);
            leftmouseReleasePosition = new Vector2(-1, -1);
        }

#if !__ANDROID__
        //If the left button was releaed then return true
        public bool LeftButtonRelease()
        {
            if ((currentmousestate.LeftButton == ButtonState.Released) &&
                prevmousestate.LeftButton == ButtonState.Pressed)
            {
                leftmouseReleasePosition = new Vector2(currentmousestate.X, currentmousestate.Y);
                return true;
            }
            return false;
        }
        //Get the left mouse button release position
        public Vector2 LeftButtonReleasePostion()
        {
            return leftmouseReleasePosition;
        }

#endif
        public void Update()
        {
            previousState = currentState;
            prevmousestate = currentmousestate;
            if (!isTransitioning)
            {
                //TouchColection is a list of touch values
                currenttouchcollection = TouchPanel.GetState();
                if (currenttouchcollection.Count > 0)
                {
                    //get the first element in the touch collection
                    //work should realy be the last element for if looking
                    //for finger release. But this works for now
                    currentState = currenttouchcollection[0].State;
                }
#if !__ANDROID__

                if (!isTransitioning)
                {
                    currentmousestate = Mouse.GetState();
                }
#endif
            }
        }

        public bool TouchRelease()
        {
            if ((currentState == TouchLocationState.Released) && 
                ((previousState == TouchLocationState.Moved) || 
                (previousState == TouchLocationState.Pressed)))
            {
                System.Diagnostics.Debug.WriteLine("TouchRelease True");
                touchReleaseVector.X = currenttouchcollection[0].Position.X;
                touchReleaseVector.Y = currenttouchcollection[0].Position.Y;
                return true;
            }
            return false;
        }
        //Get the left mouse button release position
        public Vector2 TouchReleaseVector
        {
            get { return touchReleaseVector; }
        }
        //***************Private*******************************
        private Vector2 touchReleaseVector;
        private TouchLocationState currentState, previousState;
        private TouchCollection currenttouchcollection;

        ////Private Variables
        //private KeyboardState currentkeystate, prevkeystate;
        private MouseState currentmousestate, prevmousestate;
       // private bool isLeftMosueRelease;
        private Vector2 leftmouseReleasePosition;

    }
}
//#if !__ANDROID__
//InputManager()
//{
//    touchReleaseVector = new Vector2(-1, -1);
//    leftmouseReleasePosition = new Vector2(-1, -1);
//  //  isLeftMosueRelease = false;
//}
//GameSreen calls input manager update
//        public void Update()
//        {
//            prevkeystate = currentkeystate;
//            prevmousestate = currentmousestate;
//            if (!isTransitioning)
//            {
//            currentkeystate = Keyboard.GetState();
//            currentmousestate = Mouse.GetState();
//            }
//        }
//        //**********************Keyboard functions********************************
//        //Send a list of keys and see if they are pressed
//        public bool KeyPressed(params Keys[] keys)
//        {
//            foreach(Keys key in keys)
//            {
//                if (currentkeystate.IsKeyDown(key) && prevkeystate.IsKeyUp(key))
//                    return true;
//            }
//            return false;
//        }
//        //Send a list of keys and see if they are released
//        public bool KeyReleased(params Keys[] keys)
//        {
//            foreach (Keys key in keys)
//            {
//                if (currentkeystate.IsKeyUp(key) && prevkeystate.IsKeyDown(key))
//                    return true;
//            }
//            return false;
//        }
//        //Send a list of keys and see if they are down
//        public bool KeyDown(params Keys[] keys)
//        {
//            foreach (Keys key in keys)
//            {
//                if (currentkeystate.IsKeyDown(key))
//                    return true;
//            }
//            return false;
//        }
//        //**********************Mouse functions********************************
//*****************************All about touch input*********************************
//developer.xamarin.com/guides/cross-platform/game_development/monogame/introduction/part2/
/*
Defining GetDesiredVelocityFromInput
We’ll be using MonoGame’s TouchPanel class, which provides information about the current state of the touch screen.Let’s add a method which will check the TouchPanel and return our character’s desired velocity:

Vector2 GetDesiredVelocityFromInput()
{
    Vector2 desiredVelocity = new Vector2();

    TouchCollection touchCollection = TouchPanel.GetState();

    if (touchCollection.Count > 0)
    {
        desiredVelocity.X = touchCollection[0].Position.X - this.X;
        desiredVelocity.Y = touchCollection[0].Position.Y - this.Y;

        if (desiredVelocity.X != 0 || desiredVelocity.Y != 0)
        {
            desiredVelocity.Normalize();
            const float desiredSpeed = 200;
            desiredVelocity *= desiredSpeed;
        }
    }

    return desiredVelocity;
}

*/
/*Applying Velocity to Position
The velocity returned from GetDesiredVelocityFromInput needs to be applied to the character’s X and Y values to have any effect at runtime. We’ll modify the Update method as follows:

public void Update(GameTime gameTime)
{

    var velocity = GetDesiredVelocityFromInput ();

    this.X += velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
    this.Y += velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

    // temporary - we'll replace this with logic based off of which way the
    // character is moving when we add movement logic
    currentAnimation = walkDown;

    currentAnimation.Update (gameTime);
}*/
