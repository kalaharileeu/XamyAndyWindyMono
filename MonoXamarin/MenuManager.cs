using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoXamarin
{
    /// <summary>
    /// The TitleScreen class drives this Menumanger. Load, unload, update, draw
    /// The Menu manager instanve lives in TitleScreen instance.
    /// </summary>
    public class MenuManager
    {
        Menu menu;
        bool isTransitioning;

        void Transition(GameTime gameTime)
        {
            if (isTransitioning)
            {
                for (int i = 0; i < menu.Items.Count; i++)
                {
                    menu.Items[i].Image.Update(gameTime);
                    float first = menu.Items[0].Image.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Image.Alpha;
                    if (first == 0.0f && last == 0.0f)
                        menu.ID = menu.Items[menu.ItemNumber].LinkID;
                    else if (first == 1.0f && last == 1.0f)
                    {
                        isTransitioning = false;
                        foreach (MenuItem item in menu.Items)
                            item.Image.RestoreEffects();
                    }
                }
            }
        }

        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += menu_OnMenuChange;
        }
        //unload the old menu and load the new menu
        void menu_OnMenuChange(object sender, EventArgs e)
        {
            XmlManager<Menu> xmlMenuManager = new XmlManager<Menu>();
            menu.UnloadContent();//unload the current menu
            //so you can call a tansition effect here
            //do not thin this is quite right think it shoulf be. sender.ID
            menu = xmlMenuManager.Load(menu.ID);//load the new menu. Menu.ID is a path
            //Menu loads all the menu items
            menu.LoadContent();
            menu.OnMenuChange += menu_OnMenuChange;
            menu.Transition(0.0f);
            foreach (MenuItem item in menu.Items)
            {
                item.Image.StoreEffects();
                item.Image.ActivateEffect("FadeEffect");
            }
        }

        public void LoadContent(string menuPath)
        {
            if (menuPath != String.Empty)
                //Set the .xml path for new menu to be loaded
                menu.ID = menuPath;
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }
        /// <summary>
        /// The Update will use Inputmanger to load the new screen according to input.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {

            if (!isTransitioning)
            {
                menu.Update(gameTime);
#if !__ANDROID__
                //When the Enter jkey is pressed the screen changes
                if (InputManager.Instance.KeyPressed(Keys.Enter))
                {
                    //Start here tonight
                    //If the linktype is a ccreen then change screen els edo nothing
                    //The linktype if indicates what screen to change to
                    if (menu.Items[menu.ItemNumber].LinkType == "Screen")
                    {
                        ScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumber].LinkID);
                        //return;
                    }
                    else
                    {
                        //Activete the next menu item to transition
                        isTransitioning = true;
                        menu.Transition(1.0f);
                        foreach (MenuItem item in menu.Items)
                        {
                            item.Image.StoreEffects();
                            //Activate fading when transitioning
                            item.Image.ActivateEffect("FadeEffect");
                        }
                    }
                }

#endif
                //This is for MOUSE or FINGER then go process that item
                if (menu.Selected)
                {
                    if (menu.Items[menu.ItemNumber].LinkType == "Screen")
                        ScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumber].LinkID);
                        return;
                }
#if __ANDROID__

#endif
            }

            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
