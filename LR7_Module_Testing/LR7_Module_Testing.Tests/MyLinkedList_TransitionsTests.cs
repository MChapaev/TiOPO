using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR7_Module_Testing.MyStack;


namespace LR7_Module_Testing.Tests
{
    [TestFixture]
    public class MyLinkedListStack_TransitionsTests
    {
        [Test]
        public void Transition_EmptyToNonEmpty_OnPush()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);

            ClassicAssert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void Transition_NonEmptyToEmpty_OnLastPop()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);

            stack.Pop();

            ClassicAssert.IsTrue(stack.IsEmpty);
        }

        [Test]
        public void Transition_NonEmptyToNonEmpty_OnPush()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            ClassicAssert.IsFalse(stack.IsEmpty);
        }

        [Test]
        public void Transition_NonEmptyToNonEmpty_OnPop()
        {
            var stack = new MyLinkedListStack<int>();
            stack.Push(1);
            stack.Push(2);

            stack.Pop();

            ClassicAssert.IsFalse(stack.IsEmpty);
        }
    }
}
