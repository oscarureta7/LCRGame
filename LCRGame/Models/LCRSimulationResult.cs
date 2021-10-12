using System;
using System.Collections.Generic;
using System.Text;

namespace LCRGame.Models
{
    public class LCRSimulationResult
    {
        public int ShortestGame { get; set; }
        public int LongestGame { get; set; }
        public int AverageLength { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
