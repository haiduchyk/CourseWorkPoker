using System.ComponentModel;

namespace CardLibrary
{
    public static class SuitExtensions
    {
        public static string GetSymbol(this Suit suit)
        {
            switch (suit)
            {
                case Suit.Club:
                    return "♣";
                case Suit.Diamond:
                    return "♦";
                case Suit.Heart:
                    return "♥";
                case Suit.Spade:
                    return "♠";
                default:
                    throw new InvalidEnumArgumentException("Invalid enum");
            }
        }
    }
}
