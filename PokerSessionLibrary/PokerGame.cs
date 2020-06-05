using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class PokerGame
    {
        public PokerTable Table { get; private set; }
        public PokerGame(List<IPlayer> players, IDealer dealer)
        {
            Table = new PokerTable(players, dealer);
        }
        
        public void Start()
        {

            while (true)
            {
                Table.Dealer.KickLosers();
                Table.Dealer.Setup();
                Table.Dealer.CollectAnte();
                Table.Dealer.DealHands();
                Table.GetHuman()?.RevealHand();
                Table.Dealer.CollectBets();
                Table.Dealer.CollectTrades();
                Table.GetHuman()?.RevealHand();
                Table.Dealer.CollectBets();
                Table.Dealer.AnnounceShowdown();
                Table.Players = Table.AllPlayers.ToList();
                Table.Players.ForEach(p => GraphicsHelper.Instance.TypeLine($"{p.Name} have(s) ${p.Stack} left"));
                if (CheckForGameEnd()) break;
                
                GraphicsHelper.Instance.TypeLine("[Any Key] Play another round");
                GraphicsHelper.Instance.TypeLine("[N] Cash out");


                if (Console.ReadKey(true).Key.Equals(ConsoleKey.N))
                {
                    End();
                    break;
                }
                
                Console.Clear();
            }

        }

        private bool CheckForGameEnd()
        {
            Table.Dealer.KickLosers();
            
            if (Table.GetHuman() == null)
            {
                Console.WriteLine("Game Over");
                End();
                return true;
            }
            
            if (Table.Players.Count <= 1)
            {
                Console.WriteLine("You Win");
                End();
                return true;
            }

            return false;
        }

        public void End()
        {
            ShowStatistics();
            Console.WriteLine("[ESC] Return to main menu.");

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key.Equals(ConsoleKey.Escape))
                        return;
                }
            }
        }
        
        private void ShowStatistics()
        {
            var totalHands = Table.AllPlayers.Sum(player => player.Wins);

            foreach (var player in Table.AllPlayers)
            {
                Console.WriteLine($"{player} won {player.Wins} hand(s) out of {totalHands} hand(s).");
                Console.WriteLine($"Their total winnings this game were ${player.Stack}.\n\n");
            }
        }
    }
}
