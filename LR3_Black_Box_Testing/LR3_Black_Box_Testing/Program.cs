using LR3_Black_Box_Testing.Abstractions;
using LR3_Black_Box_Testing.Core;

int _size = 10;

CheckerController _controller = new TraceCheckerController(new LineChecker(_size));

// Run
_controller
    .PrintInfo()
    .SetupData(Vector2Factory.Data(_size))
    .Run()
    .PrintResult();
