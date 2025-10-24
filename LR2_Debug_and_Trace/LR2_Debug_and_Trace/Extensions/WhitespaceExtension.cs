namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class WhitespaceExtension
    {
        public static Startup Whitespace(this Startup startup)
        {
            string whitespace = new string('#', 20);
            Console.WriteLine($"\n{whitespace}\n");
            return startup;
        }
    }
}
