namespace LR8_Tests.TestDoubles
{
    public class MockWriter : StringWriter
    {
        public bool WriteCalled { get; private set; }
        public override void Write(string? value)
        {
            WriteCalled = true;
            base.Write(value);
        }
    }
}
