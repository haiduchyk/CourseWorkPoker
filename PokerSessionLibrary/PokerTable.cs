using CardLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public class PokerTable : IEnumerable<IPlayer>
    {
        private List<Card> muck;
        public List<IPlayer> Players { get; set; }

        public readonly List<IPlayer> AllPlayers;
        public IDealer Dealer { get; set; }

        public decimal Pot { get; set; }
        
        private PokerTable()
        {
            Players = new List<IPlayer>();
            muck = new List<Card>();
        }

        public IPlayer GetHuman()
        {
            return Players.FirstOrDefault(p => p is Player);
        } 
        
        public PokerTable(List<IPlayer> players, IDealer dealer) : this()
        {
            SeatDealer(dealer);
            SeatPlayers(players.Take(House.MaxPlayers).ToList());
            
            AllPlayers = Players.ToList();
        }
        
        public void SeatDealer(IDealer dealer)
        {
            Dealer = dealer;
            Dealer.Table = this;
        }
        
        public void SeatPlayers(List<IPlayer> players)
        {
            Players.AddRange(players);

            foreach (IPlayer player in this)
                player.Table = this;
        }
        
        public void Setup()
        {
            ClearPot();
            ClearMuck();
        }
        
        public void Muck(Card card)
        {
            muck.Add(card);
        }
        
        public void Muck(List<Card> cards)
        {
            muck.AddRange(cards);
        }
        
        public void IncreasePot(decimal amount)
        {
            if (amount < 1)
                throw new ArgumentException("Pot increase cannot be negative.");

            Pot += amount;

        }
        
        public decimal ClearPot()
        {
            var currentPot = Pot;
            Pot = 0;
            return currentPot;
        }
        
        public void ClearMuck()
        {
            muck.Clear();
        }
        
        public IEnumerator<IPlayer> GetEnumerator()
        {
            return ((IEnumerable<IPlayer>)Players).GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<IPlayer>)Players).GetEnumerator();
        }
    }
}
