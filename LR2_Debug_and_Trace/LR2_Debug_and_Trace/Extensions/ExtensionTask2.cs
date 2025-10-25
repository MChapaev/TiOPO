using LR2_Debug_and_Trace.Tasks;

namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class ExtensionTask2
    {
        public static Startup InitializeTask2(this Startup startup, int to)
        {
            startup.SetTask(new Task2(to, "Задача 2"));
            return startup;
        }
    }
}
