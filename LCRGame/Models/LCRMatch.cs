using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Models
{
    public class LCRMatch
    {
        public int NumberOfPlayers { get; }
        public List<Player> Players { get; private set; }

        public LCRMatch(int numberOfPlayers, int startingChips) {

            Players = new List<Player>();

            for(int i = 0; i <= numberOfPlayers; ++i)
            {
                Players.Add(new Player(startingChips, "Player " + i));
            }

            Console.WriteLine(String.Format("New match created: {0} Players", Players.Count)) ;
        }
    }
}
