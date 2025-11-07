using LR2_Debug_and_Trace.Tasks;

namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class ExtensionTask1
    {
        public static Startup InitializeTask1(this Startup startup, 
            Func<double, double> function, double a, double b)
        {
            startup.SetTask(new Task1(function, a, b, "Задача 1"));
            return startup;
        }
    }
}
