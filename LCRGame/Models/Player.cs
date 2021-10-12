using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Models
{
    public class Player
    {
        public bool Active { get; set; }
        public int Chips { get; set; }
        public string Name { get; private set; }

        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            Name = name;
        }
    }
}
