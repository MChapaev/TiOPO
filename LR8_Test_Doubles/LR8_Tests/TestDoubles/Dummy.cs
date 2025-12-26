using System.Text;

namespace LR8_Tests.TestDoubles
{
    public class DummyReader : TextReader { }
    public class DummyWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}
