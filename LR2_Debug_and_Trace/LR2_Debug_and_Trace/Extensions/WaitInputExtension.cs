namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class WaitInputExtension
    {
        public static Startup WaitForInput(this Startup startup, string message = "Ожидание ввода...")
        {
            Console.WriteLine(message);
            Console.ReadLine();
            return startup;
        }
    }
}
