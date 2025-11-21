namespace LR7_Module_Testing.MyStack
{
    public sealed class MyLinkedListStack<T> : IMyStack<T>
    {
        private Node? _head;

        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;

        public void Push(T item)
        {
            var node = new Node(item, _head);
            _head = node;
            Count++;
        }

        public T Pop()
        {
            if (_head is null)
                throw new InvalidOperationException("Stack is empty.");

            var value = _head.Value;
            _head = _head.Next;
            Count--;
            return value;
        }

        public T Peek()
        {
            if (_head is null)
                throw new InvalidOperationException("Stack is empty.");

            return _head.Value;
        }

        private sealed class Node
        {
            public T Value { get; }
            public Node? Next { get; }

            public Node(T value, Node? next)
            {
                Value = value;
                Next = next;
            }
        }
    }
}
