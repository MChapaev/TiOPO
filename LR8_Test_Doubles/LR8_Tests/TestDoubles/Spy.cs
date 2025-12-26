namespace LR8_Tests.TestDoubles
{
    public class SpyReader : StringReader
    {
        public string ReadContent { get; private set; }
        public SpyReader(string content) : base(content)
        {
            ReadContent = content;
        }
    }
}
