using OpenTK;
using OpenTK.Input;
using System.Collections.Generic;

namespace TankWorld.UI.Amination
{
    class MyInput
    {
        private static List<Key> keysDown;
        private static List<Key> keysDownLast;
        private static List<MouseButton> buttonsDown;
        private static List<MouseButton> buttonsDownLast;

        public static void Initialise(GameWindow game)
        {
            keysDown = new List<Key>();
            keysDownLast = new List<Key>();
            buttonsDown = new List<MouseButton>();
            buttonsDownLast = new List<MouseButton>();

            game.KeyDown += Game_KeyDown;
            game.KeyUp += Game_KeyUp;
            game.MouseDown += Game_MouseDown;
            game.MouseUp += Game_MouseUp;
        }

        private static void Game_MouseUp(object sender, MouseButtonEventArgs e)
        {
           while(buttonsDown.Contains(e.Button))
            {
                buttonsDown.Remove(e.Button);
            }
        }

        private static void Game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonsDown.Add(e.Button);
        }

        private static void Game_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (keysDown.Contains(e.Key))
            {
                keysDown.Remove(e.Key);
            }
        }

        private static void Game_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            keysDown.Add(e.Key);
        }
        
        public static void Update()
        {
            keysDownLast = new List<Key>(keysDown);
            buttonsDownLast = new List<MouseButton>(buttonsDown);
        }

        public static bool KeyPress(Key key)
        {
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }

        public static bool KeyRelease(Key key)
        {
            return (keysDown.Contains(key) && !keysDownLast.Contains(key));
        }

        public static bool KeyDown(Key key)
        {
            return (keysDown.Contains(key));
        }

        public static bool MousePress(MouseButton Button)
        {
            return (buttonsDown.Contains(Button) && !buttonsDownLast.Contains(Button));
        }

        public static bool MouseRelease(MouseButton Button)
        {
            return (buttonsDown.Contains(Button) && !buttonsDownLast.Contains(Button));
        }

        public static bool MouseDown(MouseButton Button)
        {
            return (buttonsDown.Contains(Button));
        }
    }
}
