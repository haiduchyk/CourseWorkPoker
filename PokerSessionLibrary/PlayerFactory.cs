using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSessionLibrary
{
    public static class PlayerFactory
    {
        private static int playersCreated;
        
        public static IPlayer CreatePlayer(PlayerType playerType)
        {
            string playerName = $"Player {++playersCreated}";

            switch (playerType)
            {
                case PlayerType.Human:
                    return new Player($"You", House.InitialStack);
                case PlayerType.Computer:
                    return new Computer(playerName, House.InitialStack);
                default:
                    throw new InvalidEnumArgumentException("Invalid player type, could not construct player.");
            }
        }
    }
}
