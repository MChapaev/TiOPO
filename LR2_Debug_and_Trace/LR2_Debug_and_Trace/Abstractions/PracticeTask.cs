namespace LR2_Debug_and_Trace.Abstractions
{
    using System.Diagnostics;

    public abstract class PracticeTask : ITask
    {
        private string _taskName = null!;

        protected double Result { get; set; }

        StreamWriter _txt =
    new StreamWriter(
        new FileStream("trace.txt", FileMode.OpenOrCreate));

        public PracticeTask(string taskName)
        {
            _taskName = taskName;
        }

        public virtual void Run()
        {
            InitializeTrace();
            CalculateResult();
        }

        public double GetResult()
        {
            StopTrace();
            return Result;
        }

        private void InitializeTrace()
        {
            Trace.Listeners
                .Add(new TextWriterTraceListener(Console.Out));
            
            Trace.Listeners.Add(new TextWriterTraceListener(_txt));
            Trace.AutoFlush = true;
            Trace.Indent();

            Trace.TraceInformation($"{_taskName}. Начало трассировки.");
            Trace.WriteLine(String.Format("Дата : {0}", DateTime.Now));
        }
        
        private void StopTrace()
        {
            Trace.Unindent();
            _txt.Close();
        }
        protected abstract void CalculateResult();
    }
}
