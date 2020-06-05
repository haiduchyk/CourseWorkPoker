using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class Hand : IComparable<Hand>, IEnumerable<Card>, IEquatable<Hand>
    {
        private const int MaxHandSize = 5;
        private Card[] hand;
        
        public int Size => hand.Length;

        public PokerHand Rank => PokerHandEvaluator.EvaluatePokerHand(hand);
        
        public Card HighCard => hand[MaxHandSize - 1];
        
        public Hand()
        {
            hand = new Card[MaxHandSize];
        }
        
        public Hand(Card[] cards) : this()
        {
            for (var i = 0; i < hand.Length; i++)
                hand[i] = cards[i];
        }

        public Card this[int index]
        {
            get
            {
                if (index < 0 || index > hand.Length - 1)
                    throw new IndexOutOfRangeException("Index out of range");

                return hand[index];
            }
            set
            {
                if (index < 0 || index > hand.Length - 1)
                    throw new IndexOutOfRangeException("Index out of range");

                hand[index] = value;
            }
        }
        
        public void Display()
        {
            foreach (var card in hand)
                card.Display();
        }
        
        public void Sort()
        {
            hand = hand.OrderBy(card => (int)card.Rank).ToArray();
        }
        
        
        public override bool Equals(object other)
        {
            return this.Equals(other as Hand);
        }

        public override int GetHashCode()
        {
            return (hand != null ? hand.GetHashCode() : 0);
        }

        public static bool operator ==(Hand left, Hand right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Hand left, Hand right)
        {
            return !Equals(left, right);
        }

        public bool Equals(Hand other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Rank.Equals(other.Rank);
        }
        
        
        public int CompareTo(Hand other)
        {
            if (Equals(other))
                return HighCard.CompareTo(other.HighCard);
            return Rank.CompareTo(other.Rank);
        }
        
        
        public IEnumerator<Card> GetEnumerator() => ((IEnumerable<Card>)hand).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Card>)hand).GetEnumerator();
    }
}
