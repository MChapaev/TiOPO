using LR2_Debug_and_Trace.Tasks;

namespace LR2_Debug_and_Trace.StartupExtensions
{
    public static class ExtensionTask1
    {
        public static Startup InitializeTask1(this Startup startup)
        {
            startup.SetTask(new Task1());
            return startup;
        }
    }
}
