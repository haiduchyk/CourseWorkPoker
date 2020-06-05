using CardGameConsole.Properties;
using PokerSessionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardGameConsole.Menus.MenuItems
{
    public class ExitGameItem : IMenuItem
    {
        bool exitGame;
        public int X { get; set; }
        public int Y { get; set; }

        public ExitGameItem(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Select()
        {
            Console.Clear();
            Console.WriteLine(Resources.ExitMenuContent);
            var optionSelected = false;
            Console.CursorTop = 11;
            GraphicsHelper.Instance.DrawSelector(59, 5);

            while (!optionSelected)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            exitGame = true;
                            GraphicsHelper.Instance.EraseSelector(59, 5);
                            GraphicsHelper.Instance.DrawSelector(46, 6);
                            break;

                        case ConsoleKey.RightArrow:
                            exitGame = false;
                            GraphicsHelper.Instance.EraseSelector(46, 6);
                            GraphicsHelper.Instance.DrawSelector(59, 5);
                            break;

                        case ConsoleKey.Spacebar:
                        case ConsoleKey.Enter:
                            optionSelected = true;
                            break;

                        case ConsoleKey.Escape:
                            return;
                    }

                    
                }
            }

            if (exitGame)
                Environment.Exit(0);
        }

    }
}
