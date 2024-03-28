using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureApp
{
    internal class StandartProbabilityService : IProbabilityService
    {
        private Random _random;
        public StandartProbabilityService(Random random) 
        {
            _random = random;
        }
        public uint GetProbability(uint max) => (uint)_random.Next((int)max) + 1;
    }
}
