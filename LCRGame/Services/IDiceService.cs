using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Services
{
    public interface IDiceService
    {
        TDiceSideType RollDice <TDiceSideType> (TDiceSideType[] diceSides) where TDiceSideType : Enum;
    }
}
