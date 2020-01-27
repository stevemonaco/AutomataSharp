using System;
using Xunit;

namespace AutomataSharp.UnitTests
{
    public class WolframRuleTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(0, 1, 1, 1, 0)]

        [InlineData(110, 1, 1, 1, 0)]
        [InlineData(110, 1, 1, 0, 1)]
        [InlineData(110, 1, 0, 1, 1)]
        [InlineData(110, 1, 0, 0, 0)]
        [InlineData(110, 0, 1, 1, 1)]
        [InlineData(110, 0, 1, 0, 1)]
        [InlineData(110, 0, 0, 1, 1)]
        [InlineData(110, 0, 0, 0, 0)]

        [InlineData(255, 0, 0, 0, 1)]
        [InlineData(255, 1, 0, 1, 1)]
        [InlineData(255, 1, 1, 1, 1)]
        public void RunRule_ReturnsExpected(int ruleNumber, int leftState, int centerState, int rightState, int expected)
        {
            var rule = new WolframRule(ruleNumber);
            var filter = new NeighborFilter1D(EdgeWrap.Zero);
            var left = new Cell(leftState, rule, filter);
            var center = new Cell(centerState, rule, filter);
            var right = new Cell(rightState, rule, filter);
            var cells = new[] { left, center, right };
            
            int actual = rule.RunRule(cells);

            Assert.Equal(expected, actual);
        }
    }
}
