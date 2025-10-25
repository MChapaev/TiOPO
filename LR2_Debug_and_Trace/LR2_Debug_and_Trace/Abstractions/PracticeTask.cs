namespace LR2_Debug_and_Trace.Abstractions
{
    using System.Diagnostics;

    public abstract class PracticeTask : ITask
    {
        protected double Result { get; set; }

        public double GetResult()
        {
            Trace.Unindent();
            return Result;
        }

        public virtual void Run()
        {
            InitializeTrace();
            CalculateResult();
        }

        protected void InitializeTrace()
        {
            Trace.Listeners
                .Add(new TextWriterTraceListener(Console.Out));
            StreamWriter txt =
                new StreamWriter(
                    new FileStream("trace.txt", FileMode.OpenOrCreate));

            Trace.Listeners.Add(new TextWriterTraceListener(txt));
            Trace.AutoFlush = true;
            Trace.Indent();

            Trace.TraceInformation("Начало трассировки задачи 1");
            Trace.WriteLine(String.Format("Дата : {0}", DateTime.Now));
        }

        protected abstract void CalculateResult();
    }
}
