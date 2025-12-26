using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class FakeTextReader : TextReader
    {
        private readonly Queue<string> _lines;

        public FakeTextReader(string content)
        {
            _lines = new Queue<string>(
                content.Split(new[] { '\n' }, StringSplitOptions.None));
        }

        public override string ReadLine()
        {
            return _lines.Count > 0 ? _lines.Dequeue() : null;
        }
    }

    public class FakeTextWriter : TextWriter
    {
        private readonly StringBuilder _content = new();

        public override Encoding Encoding => Encoding.UTF8;

        public override void Write(char value) => _content.Append(value);
        public override void WriteLine(string value) => _content.AppendLine(value);

        public string GetContent() => _content.ToString();
    }
}
