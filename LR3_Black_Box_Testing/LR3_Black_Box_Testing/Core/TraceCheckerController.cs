using System.Diagnostics;
using LR3_Black_Box_Testing.Abstractions;

namespace LR3_Black_Box_Testing.Core
{
    public class TraceCheckerController : CheckerController, IDisposable
    {
        public TraceCheckerController(IPointChecker checker) : base(checker) 
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        public override CheckerController PrintInfo(string? info = null)
        {
            Trace.TraceInformation(info);
            return this;
        }

        protected override void OutputResult()
        {
            Trace.TraceInformation(ResultBuilder.ToString());
        }

        public void Dispose()
        {
            Trace.Unindent();
        }
    }
}
