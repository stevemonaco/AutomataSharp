namespace AutomataSharp
{
    public class Cell
    {
        public int State { get; private set; }
        public IConvolutionRule Rule { get; set; }
        public IFilterKernel Filter { get; set; }
        public HistoryQueue<int> StateHistory { get; }
        private int _nextState;

        public Cell() { }

        public Cell(int state, IConvolutionRule rule, IFilterKernel filter)
        {
            State = state;
            Rule = rule;
            Filter = filter;
        }

        public void SetNextState(int state)
        {
            _nextState = state;
        }

        public void StepState()
        {
            StateHistory?.Enqueue(State);
            State = _nextState;
        }
    }
}
