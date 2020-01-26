using System.Collections.Generic;

namespace AutomataSharp
{
    public class HistoryQueue<T>
    {
        private Queue<T> _queue;
        public int Limit { get; set; }
        public int Count { get => _queue.Count; }

        public HistoryQueue(int limit = int.MaxValue)
        {
            Limit = limit;
            _queue = new Queue<T>(Limit);
        }

        public void Clear() => _queue.Clear();
        public bool Contains(T item) => _queue.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => _queue.CopyTo(array, arrayIndex);
        public T Dequeue() => _queue.Dequeue();

        public void Enqueue(T item)
        {
            if (Count == Limit)
                _queue.Dequeue();
            _queue.Enqueue(item);
        }

        public T Peek() => _queue.Peek();
        public T[] ToArray() => _queue.ToArray();
        public void TrimExcess() => _queue.TrimExcess();
        public bool TryDequeue(out T result) => _queue.TryDequeue(out result);
        public bool TryPeek(out T result) => _queue.TryPeek(out result);
    }
}
