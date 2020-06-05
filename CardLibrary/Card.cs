using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class Card : IComparable<Card>, IEquatable<Card>
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }
        
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public void Display()
        {
            SetDisplayColor();
            Console.WriteLine(this);
            Console.ResetColor();
        }

        private void SetDisplayColor()
        {
            if (Suit == Suit.Club || Suit == Suit.Spade) Console.ForegroundColor = ConsoleColor.Black;
            else Console.ForegroundColor = ConsoleColor.DarkRed;
        }
        
        public int CompareTo(Card other)
        {
            return Rank.CompareTo(other.Rank);
        }
        
        public override string ToString() => $"{Rank} {Suit.GetSymbol()}";
        
        public bool Equals(Card other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Suit.Equals(other.Suit) && Rank.Equals(other.Rank);
        }

    }
}
