namespace LR7_Module_Testing.MyStack
{
    public interface IMyStack<T>
    {
        int Count { get; }
        void Push(T item);
        T Pop();
        T Peek();
        bool IsEmpty { get; }
    }
}
