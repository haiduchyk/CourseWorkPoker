using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class Deck : IEnumerable<Card>
    {
        private const int MaxDeckSize = 52;
        private Card[] deck;
        public int Size => deck.Length;
        
        public Deck()
        {
            deck = GetDeck();
        }
        
        public Card this[int index]
        {
            get => deck[index];
            set => deck[index] = value;
        }
        
        private static Card[] GetDeck()
        {
            var temp = new Card[MaxDeckSize];
            var index = 0;

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    temp[index++] = new Card(suit, rank);
            }
            return temp;
        }
        
        public override string ToString() => $"[{string.Join<Card>(", ", deck)}]";
        
        public IEnumerator<Card> GetEnumerator() => ((IEnumerable<Card>)deck).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Card>)deck).GetEnumerator();
    }
} 
