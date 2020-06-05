using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public interface IPlayer : IComparable<IPlayer>
    {
        string Name { get; set; }
        decimal Stack { get; set; }
        int Wins { get; set; }
        Hand Hand { get; set; }
        PokerTable Table { get; set; }
        
        decimal Ante();
        decimal Bet();
        decimal Collect(decimal amount);
        List<Card> Discard();
        void RevealHand();
        

    }
}
