﻿using CardLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public interface IDealer
    {
        PokerTable Table { get; set; }

        void Setup();
        Card DealCard();
        void DealHands();
        void CollectAnte();
        void CollectBets();
        void CollectTrades();
        void AnnounceShowdown();
        void KickLosers();
    }
}
