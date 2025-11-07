using LR3_Black_Box_Testing.Abstractions;
using LR3_Black_Box_Testing.Core;
using System.Numerics;

// Objects initialization
ICollection<Vector2> _data = new List<Vector2>()
{
    new Vector2(10, 1),
    new Vector2(10, 2),
    new Vector2(10, 3),
};

CheckerController _controller = new TraceCheckerController(new LineChecker(size: 10));

// Run
_controller
    .PrintInfo()
    .SetupData(_data)
    .Run()
    .PrintResult();
