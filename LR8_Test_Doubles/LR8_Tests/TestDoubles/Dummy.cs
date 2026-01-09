using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class DummyTextReader : TextReader
    {
        public override string ReadLine() => null;
        public override int Read() => -1;
    }

    public class DummyTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(char value) { }
    }

}
