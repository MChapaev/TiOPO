using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR7_Module_Testing.MyStack;


namespace LR7_Module_Testing.Tests
{
    [TestFixture]
    public class MyLinkedList_EmptyStateTests
    {
        [Test]
        public void Pop_Throws_WhenEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Test]
        public void Peek_Throws_WhenEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Test]
        public void Count_IsZero_WhenEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            ClassicAssert.AreEqual(0, stack.Count);
        }

        [Test]
        public void IsEmpty_IsTrue_WhenEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            ClassicAssert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Push_FromEmpty_LeadsToNonEmpty()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(10);
            ClassicAssert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void Push_FromEmpty_AllowsPeek()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(20);
            ClassicAssert.AreEqual(20, stack.Peek());
        }

        [Test]
        public void Push_FromEmpty_SetsCountToOne()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(30);
            ClassicAssert.AreEqual(1, stack.Count);
        }
    }
}
