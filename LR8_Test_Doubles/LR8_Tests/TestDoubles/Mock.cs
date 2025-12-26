using NUnit.Framework.Legacy;
using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class MockTextReader : TextReader
    {
        private readonly string[] _lines;
        private int _currentLine;

        public bool WasReadLineCalled { get; private set; }
        public int ReadLineCallCount { get; private set; }

        public MockTextReader(string[] lines)
        {
            _lines = lines;
        }

        public override string ReadLine()
        {
            WasReadLineCalled = true;
            ReadLineCallCount++;
            return _currentLine >= _lines.Length ? null : _lines[_currentLine++];
        }

        public void VerifyReadWasCalled()
        {
            ClassicAssert.IsTrue(WasReadLineCalled);
        }

        public void VerifyReadCallCount(int expectedCount)
        {
            ClassicAssert.AreEqual(expectedCount, ReadLineCallCount);
        }
    }

    public class MockTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public bool WasWriteLineCalled { get; private set; }
        public int WriteLineCallCount { get; private set; }
        public List<string> WrittenLines { get; } = new();

        public override void WriteLine(string value)
        {
            WasWriteLineCalled = true;
            WriteLineCallCount++;
            WrittenLines.Add(value);
        }

        public override void Write(char value) { }

        public void VerifyWriteWasCalled()
        {
            ClassicAssert.IsTrue(WasWriteLineCalled);
        }

        public void VerifyWriteCallCount(int expectedCount)
        {
            ClassicAssert.AreEqual(expectedCount, WriteLineCallCount);
        }

        public void VerifyLineWritten(string expectedLine)
        {
            ClassicAssert.Contains(expectedLine, WrittenLines);
        }
    }
}
