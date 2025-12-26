using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class StubTextReader : TextReader
    {
        private readonly string[] _lines;
        private int _currentLine;

        public StubTextReader(string[] lines)
        {
            _lines = lines;
        }

        public override string ReadLine()
        {
            return _currentLine >= _lines.Length ? null : _lines[_currentLine++];
        }
    }

    public class StubTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(char value) { }
        public override void WriteLine(string value) { }
    }
}
