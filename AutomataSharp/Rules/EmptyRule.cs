using System;
using System.Collections.Generic;
using System.Text;

namespace AutomataSharp
{
    public class EmptyRule : IConvolutionRule
    {
        public int RunRule(IList<Cell> cells)
        {
            return 0;
        }
    }
}
