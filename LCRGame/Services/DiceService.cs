using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Services
{
    public class DiceService : IDiceService
    {
        public TDiceSideType RollDice<TDiceSideType>(TDiceSideType[] diceSides) where TDiceSideType : Enum
        {
            return diceSides[0];
        }
    }
}
