using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR7_Module_Testing.MyStack;


namespace LR7_Module_Testing.Tests
{
    [TestFixture]
    public class MyLinkedList_FilledStateTests
    {
        [Test]
        public void Peek_ReturnsTop_WithoutChangingCount()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            var top = stack.Peek();

            ClassicAssert.AreEqual(2, top);
            ClassicAssert.AreEqual(2, stack.Count);
        }

        [Test]
        public void Push_IncreasesCount()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            ClassicAssert.AreEqual(2, stack.Count);
        }

        [Test]
        public void Push_MakesItemNewTop()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(5);
            stack.Push(10);

            ClassicAssert.AreEqual(10, stack.Peek());
        }

        [Test]
        public void Pop_ReturnsTopElement()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            var value = stack.Pop();
            ClassicAssert.AreEqual(2, value);
        }

        [Test]
        public void Pop_DecreasesCount()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            stack.Pop();

            ClassicAssert.AreEqual(1, stack.Count);
        }

        [Test]
        public void Pop_FromSingleElement_LeadsToEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);

            stack.Pop();

            ClassicAssert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Pop_WhenMoreThanOne_KeepsNonEmptyState()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            stack.Pop();

            ClassicAssert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void IsEmpty_IsFalse_WhenNonEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);

            ClassicAssert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void LIFO_IsPreserved()
        {
            var stack = new MyLinkedListStack<int>();

            stack.Push(10);
            stack.Push(20);
            stack.Push(30);

            ClassicAssert.AreEqual(30, stack.Pop());
            ClassicAssert.AreEqual(20, stack.Pop());
            ClassicAssert.AreEqual(10, stack.Pop());
        }
    }
}
