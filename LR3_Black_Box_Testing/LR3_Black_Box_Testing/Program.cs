using LR3_Black_Box_Testing.Abstractions;
using LR3_Black_Box_Testing.Core;
using System.Numerics;


IPointChecker _checker = new LineChecker(10);

Console.WriteLine("Классы эквивалентности: 3. Точка в окружности, в фигуре.");
Console.WriteLine("1. Точка вне окружности - |x| или |y| > 10.");
Console.WriteLine("2. Точка в окружности, вне фигуры - |x| или |y| <= 10.");
Console.WriteLine("2. Точка в окружности, в фигуре - угол [pi/4, pi/2], [5pi/4, 3pi/2].");
Console.Write("Введите целочисленные координаты точек x y: ");

Vector2 _point = ParsePoint(Console.ReadLine());

_checker.SetPoint(_point);
_checker.Check();

Console.WriteLine($"Результат: {_checker.GetResult()}");

static Vector2 ParsePoint(string input)
{
    var p = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    return new Vector2(
        int.Parse(p[0]),
        int.Parse(p[1])
    );
}
