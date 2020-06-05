using CardGameConsole.Menus.MenuItems;
using CardGameConsole.Properties;
using PokerSessionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameConsole.Menus
{
    public class MainMenu
    {
        int currentMenuIndex;
        IMenuItem[] menuItems;

        public MainMenu()
        {
            currentMenuIndex = 0;
            menuItems = new IMenuItem[] { new StartGameItem(48, 11), new ExitGameItem(48, 13) };
        }

        public void Open()
        {
            Console.Clear();
            GraphicsHelper.Instance.SetConsoleColor();

            var optionSelected = false;
            Console.CursorVisible = false;
            Console.WriteLine(Resources.MainMenuContent);

            Console.CursorTop = menuItems[currentMenuIndex].Y;
            GraphicsHelper.Instance.DrawSelector(menuItems[currentMenuIndex].X, 15);

            while (!optionSelected)
            {
   
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        GraphicsHelper.Instance.EraseSelector(menuItems[currentMenuIndex].X, 15);
                        currentMenuIndex--;

                        if (currentMenuIndex < 0)
                            currentMenuIndex = menuItems.Length - 1;

                        break;

                    case ConsoleKey.DownArrow:
                        GraphicsHelper.Instance.EraseSelector(menuItems[currentMenuIndex].X, 15);
                        currentMenuIndex++;

                        if (currentMenuIndex > menuItems.Length - 1)
                            currentMenuIndex = 0;

                        break;

                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        optionSelected = true;
                        break;

                }

                Console.CursorTop = menuItems[currentMenuIndex].Y;
                GraphicsHelper.Instance.DrawSelector(menuItems[currentMenuIndex].X, 15);
            }

            while (currentMenuIndex != -1)
            {
                menuItems[currentMenuIndex].Select();
                Open();
            }
        }
    }
}
