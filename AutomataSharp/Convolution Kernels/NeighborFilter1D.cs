using System;
using System.Collections.Generic;

namespace AutomataSharp
{
    public class NeighborFilter1D : IFilterKernel
    {
        public EdgeWrap EdgeWrap { get; }
        private static IConvolutionRule _emptyRule = new EmptyRule();
        private static IFilterKernel _defaultFilter = new NeighborFilter1D(EdgeWrap.Zero);
        private Cell _defaultCell;

        public NeighborFilter1D(EdgeWrap wrap)
        {
            EdgeWrap = wrap;

            if(EdgeWrap == EdgeWrap.Zero)
                _defaultCell = new Cell(0, _emptyRule, _defaultFilter);
        }

        public IEnumerable<Cell> Filter(Cell[,] grid, int x, int y)
        {
            if (grid is null)
                throw new ArgumentNullException($"{nameof(Filter)} parameter {nameof(grid)} is null");
            if (grid.Length == 0)
                throw new ArgumentException($"{nameof(Filter)} parameter {nameof(grid)} contained zero cells");

            if (EdgeWrap == EdgeWrap.Zero)
                return FilterZero(grid, x, y);
            else if (EdgeWrap == EdgeWrap.Periodic)
                return FilterPeriodic(grid, x, y);
            else
                throw new NotSupportedException($"{nameof(Filter)} {nameof(EdgeWrap)} mode with value ({EdgeWrap}) is not supported");
        }

        private IEnumerable<Cell> FilterZero(Cell[,] grid, int x, int y)
        {
            yield return WrapZero(grid, x - 1, y);
            yield return WrapZero(grid, x, y);
            yield return WrapZero(grid, x + 1, y);
        }

        private Cell WrapZero(Cell[,] grid, int x, int y)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);
            if (x < 0 || x >= width || y < 0 || y >= height)
                return _defaultCell;
            return grid[x, y];
        }

        private IEnumerable<Cell> FilterPeriodic(Cell[,] grid, int x, int y)
        {
            int length = grid.GetLength(0);
            yield return grid[WrapIndex(x - 1, length), y];
            yield return grid[x, y];
            yield return grid[WrapIndex(x + 1, length), y];
        }

        private int WrapIndex(int index, int length)
        {
            return ((index % length) + length) % length;
        }
    }
}