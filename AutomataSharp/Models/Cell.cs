namespace AutomataSharp
{
    public class Cell
    {
        public int State { get; private set; }
        public IConvolutionRule Rule { get; set; }
        public IFilterKernel Filter { get; set; }
        public HistoryQueue<int> StateHistory { get; }

        public Cell() { }

        public Cell(int state, IConvolutionRule rule, IFilterKernel filter, int historyLimit = int.MaxValue)
        {
            State = state;
            Rule = rule;
            Filter = filter;
            StateHistory = new HistoryQueue<int>(historyLimit);
        }

        public void SetState(int state)
        {
            State = state;
        }
    }
}
