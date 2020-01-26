using System.Collections.Generic;

namespace AutomataSharp
{
    public class NeighborFilter1D : IFilterKernel
    {
        private static IConvolutionRule _emptyRule = new EmptyRule();
        private static IFilterKernel _defaultFilter = new NeighborFilter1D();
        private Cell _defaultCell = new Cell(0, _emptyRule, _defaultFilter);

        public IEnumerable<Cell> Filter(Cell[,] grid, int x, int y)
        {
            var left = x <= 0 ? _defaultCell : grid[x - 1, y];
            var center = grid[x, y];
            var right = (x + 1) >= grid.GetLength(0) ? _defaultCell : grid[x + 1, y];

            yield return left;
            yield return center;
            yield return right;
        }
    }
}
