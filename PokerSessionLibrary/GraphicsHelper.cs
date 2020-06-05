using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class GraphicsHelper
    {
        private static GraphicsHelper instance = new GraphicsHelper();

        public static GraphicsHelper Instance => instance;

        public void SetConsoleColor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        
        public void ResetConsoleColor()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        
        public void EraseSelector(int x, int dx)
        {
            Console.CursorLeft = x;
            Console.Write(" ");

            Console.CursorLeft = x + dx;
            Console.Write(" ");
        }
        
        public void DrawSelector(int x, int dx)
        {
            Console.CursorLeft = x;
            Console.Write(">");
            Console.CursorLeft = x + dx;
            Console.Write("<");
        }
        
        public void TypeLine(string value)
        {
            Type(value + "\n");
        }
        
        private static void Type(string value)
        {
            foreach (char character in value)
            {
                Console.Write(character);
                Thread.Sleep(10);
            }
        }
        
        public void Wait(int value = 500)
        {
            Thread.Sleep(value);
        }
    }
}
