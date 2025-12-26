using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class SpyTextReader : TextReader
    {
        private readonly string[] _lines;
        private int _currentLine;

        public int ReadLineCallCount { get; private set; }
        public List<string> RequestedLines { get; } = new();

        public SpyTextReader(string[] lines)
        {
            _lines = lines;
        }

        public override string ReadLine()
        {
            ReadLineCallCount++;
            if (_currentLine >= _lines.Length)
            {
                RequestedLines.Add(null);
                return null;
            }

            var line = _lines[_currentLine++];
            RequestedLines.Add(line);
            return line;
        }
    }

    public class SpyTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public int WriteLineCallCount { get; private set; }
        public List<string> WrittenLines { get; } = new();

        public override void WriteLine(string value)
        {
            WriteLineCallCount++;
            WrittenLines.Add(value);
        }

        public override void Write(char value) { }
    }
}
