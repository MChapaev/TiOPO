using LR2_Debug_and_Trace;
using LR2_Debug_and_Trace.StartupExtensions;

new Startup()
    .InitializeTask1()
    .Run()
    .PrintResult()
    .InitializeTask2()
    .Run()
    .PrintResult();