using System;
using System.Collections.Generic;
using System.Text;

using LCRGame.Models;

namespace LCRGame.Services
{
    public class DiceService : IDiceService
    {
        Random randomGenerator;
        
        public DiceService()
        {
            randomGenerator = new Random();
        }

        public LCRDiceSides RollDie()
        {
            return GetDieSideType(randomGenerator.Next(0, 6));
        }

        public LCRDiceSides[] RollDice(int diceCount)
        {
            LCRDiceSides[] result = new LCRDiceSides[diceCount];

            for(int i = 1; i < diceCount; i++)
            {
                result[i] = GetDieSideType(randomGenerator.Next(0, 6));
            }

            return result;
        }

        private LCRDiceSides GetDieSideType(int side)
        {
            if (side < 3)
                return LCRDiceSides.Dot;

            switch (side)
            {
                case 3:
                    return LCRDiceSides.C;
                case 4:
                    return LCRDiceSides.L;
                case 5:
                    return LCRDiceSides.R;
                default:
                    return LCRDiceSides.Dot;
            }
        }
    }
}
