using System.Collections.Generic;

namespace AutomataSharp
{
    public interface IFilterKernel
    {
        IEnumerable<Cell> Filter(Cell[,] grid, int x, int y);
    }
}
