using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGameConsole.Menus;
using PokerSessionLibrary;

namespace CardGameConsole
{
    class PokerProgram
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(120, 25);
            var mainMenu = new MainMenu();
            mainMenu.Open();
            Console.ReadLine();
        } 

    } 

} 
