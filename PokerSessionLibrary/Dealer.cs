using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class Dealer : IDealer
    {
        Deck deck;
        int topCardIndex;
        static Random random = new Random();
        
        public PokerTable Table { get; set; }
        
        public Dealer()
        {
            deck = new Deck();
            topCardIndex = deck.Size - 1;
        }
        
        public void Setup()
        {
            var GraphicsHelper = new GraphicsHelper();
            GraphicsHelper.Instance.TypeLine("Dealer: Setting up...\n");
            Table.Setup();
            topCardIndex = deck.Size - 1;
            ShuffleDeck();
            GraphicsHelper.Instance.Wait();
        }
        
        private void ShuffleDeck()
        {
            for (int i = deck.Size - 1; i > 0; i--)
            {
                int secondCardIndex = random.Next(0, 52);
                Card temp = deck[i];
                deck[i] = deck[secondCardIndex];
                deck[secondCardIndex] = temp;
            }
        }
        
        private void BurnCard()
        {
            Table.Muck(deck[topCardIndex--]);
        }
        
        public Card DealCard()
        {
            return deck[topCardIndex--];
        }

        public void CollectAnte()
        {
            GraphicsHelper.Instance.TypeLine("Dealer: Please ante up...\n");
            decimal totalAntes = 0;
            GraphicsHelper.Instance.Wait();

            foreach (IPlayer player in Table)
            {
                var currentAnte = player.Ante();
                totalAntes += currentAnte;
                GraphicsHelper.Instance.TypeLine($"Dealer: {player} anteed {currentAnte:C2}.");
            }

            IncreasePot(totalAntes);

        }
        
        public void CollectBets()
        {
            GraphicsHelper.Instance.TypeLine("Dealer: Place your bets...\n");

            decimal totalBets = 0;
            GraphicsHelper.Instance.Wait();

            foreach (IPlayer player in Table.ToList())
            {
                decimal currentBet = player.Bet();
                if (currentBet == 0)
                {
                    Table.Players.Remove(player);
                    GraphicsHelper.Instance.TypeLine($"Dealer: {player} pass");
                    continue;
                }
                totalBets += currentBet;
                GraphicsHelper.Instance.TypeLine($"Dealer: {player} bet {currentBet:C2}.");
            }

            IncreasePot(totalBets);
        }
        
        private void IncreasePot(decimal amount)
        {
            Table.IncreasePot(amount);
            GraphicsHelper.Instance.TypeLine($"\nDealer: The pot is currently {Table.Pot:C2}.\n");
        }
        
        public void DealHands()
        {
            GraphicsHelper.Instance.TypeLine("Dealer: Dealing cards....\n");

            var currentHandIndex = 0;
            var maxHandIndex = House.MaxHandSize - 1;
            BurnCard();

            while (currentHandIndex <= maxHandIndex)
            {
                for (int playerIndex = 0; playerIndex < Table.Players.Count; playerIndex++)
                {
                    var player = Table.Players[playerIndex];
                    player.Hand[currentHandIndex] = DealCard();
                }
                currentHandIndex++;
            }

            GraphicsHelper.Instance.Wait();

        }
        
        public void CollectTrades()
        {
            GraphicsHelper.Instance.TypeLine("Dealer: Trade in your cards....\n");

            GraphicsHelper.Instance.Wait();

            List<Card> trades = new List<Card>();

            foreach (IPlayer player in Table)
            {
                if (IsHumanPlayer(player))
                {
                    Console.CursorVisible = true;
                    GraphicsHelper.Instance.TypeLine("Dealer: Please enter the positions of the cards you wish to discard (1 - 5):\n");
                }

                var currentTrades = player.Discard();

                if (!(currentTrades.Count == 0))
                    trades.AddRange(currentTrades);

                GraphicsHelper.Instance.TypeLine($"Dealer: {player} traded in {currentTrades.Count} card(s).\n");
            }

            Console.CursorVisible = false;
            Table.Muck(trades);
        }

        private static bool IsHumanPlayer(IPlayer player)
        {
            return player.GetType() == typeof(Player);
        }


        public void AnnounceShowdown()
        {
            GraphicsHelper.Instance.TypeLine("Dealer: It's time to showdown...\n");
            Showdown();

            var winner = CompareHands();
            var winningHand = winner.Hand.Rank.GetString().ToLower();
            winner.Wins++;

            if (IsHumanPlayer(winner))
                GraphicsHelper.Instance.TypeLine($"Dealer: You win with a {winningHand}, and are awarded {DistributePot(winner):C2}\n");

            else
                GraphicsHelper.Instance.TypeLine($"Dealer: {winner} wins with a {winningHand}, and is awarded {DistributePot(winner):C2}\n");

        }
        
        private void Showdown()
        {
            foreach (IPlayer player in Table)
            {
                player.RevealHand();
                Console.WriteLine();
                GraphicsHelper.Instance.Wait();
            }
            
        }
        
        public IPlayer CompareHands()
        {
            return Table.Players.OrderByDescending(player => player.Hand).First();
        }
        
        private decimal DistributePot(IPlayer player)
        {
            return player.Collect(Table.ClearPot());
        }

        public void KickLosers()
        {
            Table.Players.RemoveAll(p => p.Stack <= 0);
        }
    }
}
