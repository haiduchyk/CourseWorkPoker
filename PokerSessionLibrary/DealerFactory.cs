using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public static class DealerFactory
    {
        public static IDealer CreateDealer()
        {
            return new Dealer();
        }
    }
}
