using System;
using System.Collections.Generic;
using System.Text;

namespace AutomataSharp
{
    public interface IConvolutionRule
    {
        int RunRule(IList<Cell> cells);
    }
}
