namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class WaitInputExtension
    {
        public static Startup WaitForInput(this Startup startup)
        {
            Console.ReadLine();
            return startup;
        }
    }
}
