using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class Computer : IPlayer
    {
        public string Name { get; set; }
        public decimal Stack { get; set; }
        public int Wins { get; set; }
        public Hand Hand { get; set; }
        public PokerTable Table { get; set; }
        
        private Computer()
        {
            Wins = 0;
            Hand = new Hand();
        }
        
        public Computer(string name, decimal stack) : this()
        {
            Name = name;
            Stack = stack;
        }
        
        public void RevealHand()
        {
            Console.WriteLine($"{Name}'s hand rank: {Hand.Rank.GetString()}");
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
            if (amount < 1) throw new ArgumentException("Stack increase cannot be negative.");
            Stack += amount;
            return amount;
        }
        
        public List<Card> Discard()
        {
            var discards = new List<Card>();
            var currentDiscards = 0;

            while (Hand.Rank == PokerHand.HighCard && currentDiscards < House.MaxDiscards)
            {
                Hand.Sort();
                var discardIndex = GetDiscardIndex();
                var discarded = Hand[discardIndex];
                Hand[discardIndex] = Table.Dealer.DealCard();
                discards.Add(discarded);
                currentDiscards++;
            }
            return discards;     
        }
        
        private int GetDiscardIndex()
        {
            return Hand.ToList().IndexOf((int)Hand.HighCard.Rank < (int)Rank.Ten ? Hand.HighCard : Hand.Min());
        }
        
        public override string ToString()
        {
            return $"{Name}";
        }

        public int CompareTo(IPlayer pOther)
        {
            return Hand.CompareTo(pOther.Hand);
        }
    }
}
