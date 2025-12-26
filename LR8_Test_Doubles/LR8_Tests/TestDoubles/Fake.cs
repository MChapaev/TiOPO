namespace LR8_Tests.TestDoubles
{
    public class FakeWriter : StringWriter
    {
        public string GetOutput() => ToString();
    }
}
