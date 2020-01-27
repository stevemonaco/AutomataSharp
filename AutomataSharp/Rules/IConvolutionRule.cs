using System;
using System.Collections.Generic;
using System.Text;

namespace AutomataSharp
{
    public enum EdgeWrap { Zero, Periodic }
    public interface IConvolutionRule
    {
        int RunRule(IList<Cell> cells);
    }
}
