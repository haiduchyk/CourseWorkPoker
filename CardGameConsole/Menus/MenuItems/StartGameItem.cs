using PokerSessionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameConsole.Menus.MenuItems
{
    public class StartGameItem : IMenuItem
    {
        public int X { get; set; }
        public int Y { get; set; }

        public StartGameItem(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Select()
        {
            Console.Clear();

            var dealer = DealerFactory.CreateDealer();
            var player1 = PlayerFactory.CreatePlayer(PlayerType.Human);
            var player2 = PlayerFactory.CreatePlayer(PlayerType.Computer);
            var player3 = PlayerFactory.CreatePlayer(PlayerType.Computer);
            var player4 = PlayerFactory.CreatePlayer(PlayerType.Computer);
            var players = new List<IPlayer>() { player1, player2, player3, player4 };

            PokerGameFactory.CreateGame(players, dealer).Start();
        }
    }
}
