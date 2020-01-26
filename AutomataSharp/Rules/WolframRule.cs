using System;
using System.Collections.Generic;
using System.Text;

namespace AutomataSharp
{
    public class WolframRule : IConvolutionRule
    {
        public int RuleNumber { get; set; }

        private const int _mapSize = 8;
        private int[] _ruleMap = new int[_mapSize];

        public WolframRule(int ruleNumber)
        {
            if (ruleNumber > 255 || ruleNumber < 0)
                throw new ArgumentOutOfRangeException($"{nameof(ruleNumber)} ({ruleNumber})is outside the valid range [0-255]");

            RuleNumber = ruleNumber;

            for (int i = 0; i < _mapSize; i++, ruleNumber >>= 1)
                _ruleMap[i] = ruleNumber & 1;
        }

        public int RunRule(IList<Cell> cells)
        {
            var index = cells[0].State * 4 + cells[1].State * 2 + cells[2].State;
            return _ruleMap[index];
        }
    }
}
