using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Models
{
    public class Player
    {
        public int Chips { get; private set; }
        public string Name { get; private set; }

        public Player(int chipsAtStart, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            Chips = chipsAtStart;
            Name = name;
        }
    }
}
