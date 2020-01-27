using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutomataSharp.UnitTests
{
    public class NeighborFilter1DTests
    {
        private static IConvolutionRule _rule = new EmptyRule();

        [Theory]
        [InlineData(new int[] { 0 }, 0, EdgeWrap.Zero, new int[] { 0, 0, 0 })]
        [InlineData(new int[] { 1 }, 0, EdgeWrap.Zero, new int[] { 0, 1, 0 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 2, EdgeWrap.Zero, new int[] { 1, 0, 1 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 0, EdgeWrap.Zero, new int[] { 0, 1, 1 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 4, EdgeWrap.Zero, new int[] { 1, 1, 0 })]

        [InlineData(new int[] { 0 }, 0, EdgeWrap.Periodic, new int[] { 0, 0, 0 })]
        [InlineData(new int[] { 1 }, 0, EdgeWrap.Periodic, new int[] { 1, 1, 1 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 2, EdgeWrap.Periodic, new int[] { 1, 0, 1 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 0, EdgeWrap.Periodic, new int[] { 1, 1, 1 })]
        [InlineData(new int[] { 1, 1, 0, 1, 1 }, 4, EdgeWrap.Periodic, new int[] { 1, 1, 1 })]
        public void Filter_ReturnsExpected(int[] states, int index, EdgeWrap wrap, int[] expected)
        {
            var cells = new Cell[states.Length, 1];
            var filter = new NeighborFilter1D(wrap);
            for (int x = 0; x < states.Length; x++)
                cells[x, 0] = new Cell(states[x], _rule, filter);

            var filteredCells = filter.Filter(cells, index, 0);
            var actual = filteredCells.Select(x => x.State).ToArray();

            Assert.Equal(expected, actual);
        }
    }
}
