using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary
{
    public class PokerHandEvaluator
    {
        //There is one suit of cards in the Poker hand
        public static bool IsFlush(Card[] hand)
        {
            hand = hand.OrderBy(item => (int)item.Suit).ToArray();
            var maxCardIndex = hand.Length - 1;
            return hand[0].Suit.Equals(hand[maxCardIndex].Suit);
        } 

        //The cards are increasing continuously in rank
        public static bool IsStraight(Card[] hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToArray();
            var sequentialRank = (int)hand[0].Rank + 1;

            for (var i = 1; i < hand.Length; i++)
            {
                if ((int)hand[i].Rank != sequentialRank++)
                    return false;
            }
            return true;
        } 


        //A straight flush hand is a 5 cards of the same suit in sequential rank
        public static bool IsStraightFlush(Card[] hand) => IsStraight(hand) && IsFlush(hand);

        // A royal flush hand is the highest straight flush with a high ace
        public static bool IsRoyalFlush(Card[] hand) => IsStraightFlush(hand) && hand[4].Rank == Rank.Ace;
        
        //A full house hand has 3 cards of the same rank and 2 other cards of the same rank
        public static bool IsFullHouse(Card[] hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToArray();

            // a a a b b
            var lowFullHouse = (int)hand[0].Rank == (int)hand[1].Rank &&
                                (int)hand[1].Rank == (int)hand[2].Rank &&
                                (int)hand[3].Rank == (int)hand[4].Rank;
            // a a b b b
            var highFullHouse = (int)hand[0].Rank == (int)hand[1].Rank &&
                                 (int)hand[2].Rank == (int)hand[3].Rank &&
                                 (int)hand[3].Rank == (int)hand[4].Rank;
            return lowFullHouse || highFullHouse;
        }

        // A four of a kind hand has 4 cards of the same rank.
        public static bool IsFourOfAKind(Card[] hand)
        {
            hand = hand.OrderBy(item => (int)item.Rank).ToArray();
            // a a a a b
            bool lowFourOfAKind = (int)hand[0].Rank == (int)hand[1].Rank &&
                                  (int)hand[1].Rank == (int)hand[2].Rank &&
                                  (int)hand[2].Rank == (int)hand[3].Rank;
            // a b b b b 
            bool highFourOfAKind = (int)hand[1].Rank == (int)hand[2].Rank &&
                                   (int)hand[2].Rank == (int)hand[3].Rank &&
                                   (int)hand[3].Rank == (int)hand[4].Rank;

            return lowFourOfAKind || highFourOfAKind;

        } 


        //A three of a kind hand has 3 cards of the same rank and 2 cards of any rank.

        public static bool IsThreeOfAKind(Card[] hand)
        {
            if (IsFourOfAKind(hand) || IsFullHouse(hand)) return false;

            hand = hand.OrderBy(item => (int)item.Rank).ToArray();

            // a a a b c
            bool lowThreeOfAKind = (int)hand[0].Rank == (int)hand[1].Rank &&
                                    (int)hand[1].Rank == (int)hand[2].Rank;

            // a b b b c
            bool middleThreeOfAKind = (int)hand[1].Rank == (int)hand[2].Rank &&
                                      (int)hand[2].Rank == (int)hand[3].Rank;

            // a b c c c
            bool highThreeOfAKind = (int)hand[2].Rank == (int)hand[3].Rank &&
                                     (int)hand[3].Rank == (int)hand[4].Rank;

            return lowThreeOfAKind || middleThreeOfAKind || highThreeOfAKind;
        }

        //A two pair hand has two cards of the same rank and two different cards of the same rank.

        public static bool IsTwoPair(Card[] hand)
        {
            if (IsFourOfAKind(hand) || IsFullHouse(hand) || IsThreeOfAKind(hand)) return false;

            hand = hand.OrderBy(item => (int)item.Rank).ToArray();

            // a a b b c
            bool lowTwoPair = (int)hand[0].Rank == (int)hand[1].Rank &&
                              (int)hand[2].Rank == (int)hand[3].Rank;

            // a a b c c
            bool cornerTwoPair = (int)hand[0].Rank == (int)hand[1].Rank &&
                                 (int)hand[3].Rank == (int)hand[4].Rank;

            // a b b c c
            bool highTwoPair = (int)hand[1].Rank == (int)hand[2].Rank &&
                               (int)hand[3].Rank == (int)hand[4].Rank;

            return lowTwoPair || cornerTwoPair || highTwoPair;

        } 
        
        //A one pair hand has two cards of the same rank

        public static bool IsOnePair(Card[] hand)
        {
            if (IsFourOfAKind(hand) || IsFullHouse(hand) || IsThreeOfAKind(hand) || IsTwoPair(hand))
                return false;

            hand = hand.OrderBy(item => (int)item.Rank).ToArray();
            // a a b c d
            var lowPair = (int)hand[0].Rank == (int)hand[1].Rank;
            // a b b c d
            var lowerMiddlePair = (int)hand[1].Rank == (int)hand[2].Rank;
            // a b c c d
            var higherMiddlePair = (int)hand[2].Rank == (int)hand[3].Rank;
            // a b c d d
            var higherPair = (int)hand[3].Rank == (int)hand[4].Rank;
            return lowPair || lowerMiddlePair || higherMiddlePair || higherPair;
        }
        
        public static PokerHand EvaluatePokerHand(Card[] hand)
        {
            if (IsRoyalFlush(hand)) return PokerHand.RoyalFlush;
            if (IsStraightFlush(hand)) return PokerHand.StraightFlush;
            if (IsFourOfAKind(hand)) return PokerHand.FourOfAKind;
            if (IsFullHouse(hand)) return PokerHand.FullHouse;
            if (IsFlush(hand)) return PokerHand.Flush;
            if (IsStraight(hand)) return PokerHand.Straight;
            if (IsThreeOfAKind(hand)) return PokerHand.ThreeOfAKind;
            if (IsTwoPair(hand)) return PokerHand.TwoPair;
            if (IsOnePair(hand)) return PokerHand.OnePair;
            return PokerHand.HighCard;
        }
    }
}
