using System;
using System.Collections.Generic;
using System.Text;

using LCRGame.Models;

namespace LCRGame.Services
{
    public interface IDiceService
    {
        LCRDiceSides RollDie();
        public LCRDiceSides[] RollDice(int diceCount);
    }
}
