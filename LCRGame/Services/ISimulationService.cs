using System;
using System.Collections.Generic;
using System.Text;

using LCRGame.Models;

namespace LCRGame.Services
{
    public interface ISimulationService
    {
        public LCRSimulationResult Simulate(Player[] players, int initialChips, int totalRounds, IDiceService diceService);
    }
}
