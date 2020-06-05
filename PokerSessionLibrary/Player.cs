using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class Player : IPlayer
    { 
        public string Name { get; set; }
        public decimal Stack { get; set; }
        public int Wins { get; set; }
        public Hand Hand { get; set; }
        public PokerTable Table { get; set; }
        
        private Player()
        {
            Wins = 0;
            Hand = new Hand();
        }
        
        public Player(string name, decimal stack) : this()
        {
            Name = name;
            Stack = stack;
        }
        
        public void RevealHand()
        {
            Console.WriteLine($"Your hand rank: {Hand.Rank.GetString()}");
            Hand.Sort();
            Hand.Display();
            GraphicsHelper.Instance.ResetConsoleColor();
        }
        
        public decimal Ante()
        {
            if (!IsValidAnte())
            {
                var ante = Stack;
                Stack = 0;
                return ante;
            }
            Stack -= House.MinAnte;
            return House.MinAnte;
        }
        
        public decimal Bet()
        {
            Console.WriteLine($"Do you want to bet {House.MaxBet}?");
            Console.WriteLine("[Any Key] Bet");
            Console.WriteLine("[N] Pass");
            
            if (Console.ReadKey(true).Key.Equals(ConsoleKey.N)) return 0;

            if (!IsValidBet())
            {
                var bet = Stack;
                Stack = 0;
                return bet;
            }
            Stack -= House.MaxBet;
            return House.MaxBet;
        }
        
        private bool IsValidAnte()
        {
            return Stack - House.MinAnte > 0;
        }
        
        private bool IsValidBet()
        {
            return Stack - House.MaxBet > 0;
        }
        
        public decimal Collect(decimal amount)
        {
            if (amount < 1)
                throw new ArgumentException("Stack increase cannot be negative.");

            this.Stack += amount;

            return amount;
        }
        
        public List<Card> Discard()
        {
            var discards = new List<Card>();
            Card discarded;

            RevealHand();
            var discardIndices = GetDiscardIndices();

            for (var i = 0; i < discardIndices.Length; i++)
            {
                var currentIndex = discardIndices[i];

                if (IsValidIndex(currentIndex))
                {
                    discarded = Hand[currentIndex];
                    Hand[currentIndex] = Table.Dealer.DealCard();
                    discards.Add(discarded);
                }

            }

            return discards;
        }


        private int[] GetDiscardIndices()
        {
            var desiredIndices = Console.ReadLine().Split();
            Console.WriteLine();

            var indices = desiredIndices
                .Select(
                    (input) => {
                        Int32.TryParse(input, out int validIndex);

                        if (validIndex >= 1 && validIndex <= House.MaxHandSize)
                            return validIndex - 1;
                        return -1;
                    }
                ).Where(index => index != -1).Take(House.MaxDiscards).ToArray();

            return indices;
        }
        
        private bool IsValidIndex(int index)
        {
            return index >= 0 && index <= House.MaxHandSize - 1;
        }
        
        public override string ToString()
        {
            return $"{Name}";
        }
        
        public int CompareTo(IPlayer other)
        {
            return Hand.CompareTo(other.Hand);
        }
    }
}
