using LR2_Debug_and_Trace;
using LR2_Debug_and_Trace.StartupExtensions;

new Startup()
    .InitializeTask1(x => Math.Log(1 + x) / x, .1, 1)
    .Run()
    .PrintResult()
    .Whitespace()
    .InitializeTask2()
    .Run()
    .PrintResult();