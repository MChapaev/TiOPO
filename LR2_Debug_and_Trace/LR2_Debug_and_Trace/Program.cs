using LR2_Debug_and_Trace;
using LR2_Debug_and_Trace.StartupExtensions;

new Startup()
    .WaitForInput()
    .InitializeTask1(a: .1, b: 1, function: x => Math.Log(1 + x) / x)
    .Run()
    .PrintResult()
    .WaitForInput()
    .Whitespace()
    .InitializeTask2(10)
    .Run()
    .PrintResult("Сумма факториала");