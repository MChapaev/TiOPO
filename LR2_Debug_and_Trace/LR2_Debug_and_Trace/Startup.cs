using LR2_Debug_and_Trace.Abstractions;
using System.Diagnostics;

namespace LR2_Debug_and_Trace
{
    public class Startup
    {
        private ITask _task = null!;

        internal void SetTask(ITask task) => _task = task;

        public Startup Run()
        {
            Debug.Assert(_task != null);
            _task.Run();
            return this;
        }

        public Startup PrintResult(string message = "Task done")
        {
            Console.WriteLine($"{message}: {_task.Result()}");
            return this;
        }
    }
}
