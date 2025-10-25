using LR2_Debug_and_Trace;
using LR2_Debug_and_Trace.StartupExtensions;

new Startup()
    .InitializeTask1(a: .1, b: 1, function: x => Math.Log(1 + x) / x)
    .Run()
    .PrintResult()
    .Whitespace()
    .InitializeTask2()
    .Run()
    .PrintResult();