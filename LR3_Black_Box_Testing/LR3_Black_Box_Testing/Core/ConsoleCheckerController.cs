using System.Numerics;
using System.Text;
using LR3_Black_Box_Testing.Abstractions;
using LR3_Black_Box_Testing.Extensions;

namespace LR3_Black_Box_Testing.Core
{
    public class ConsoleCheckerController : CheckerController
    {
        private StringBuilder _infoBuilder = new StringBuilder();

        public ConsoleCheckerController(IPointChecker checker) : base(checker) { }

        public override CheckerController SetupData(ICollection<Vector2>? data = null)
        {
            return base.SetupData(ParseInput());
        }

        public override CheckerController PrintInfo(string? info = null)
        {
            if (info != null)
                _infoBuilder.AppendLine(info);
            SetupInfo();
            Console.WriteLine(_infoBuilder.ToString());
            return this;
        }

        protected override void OutputResult()
        {
            Console.WriteLine(ResultBuilder.ToString());
        }

        private void SetupInfo()
        {
            _infoBuilder.AppendLine("Классы эквивалентности: 3. Точка в окружности, в фигуре.");
            _infoBuilder.AppendLine("1. Точка вне окружности - |x| или |y| > 10.");
            _infoBuilder.AppendLine("2. Точка в окружности, вне фигуры - |x| или |y| <= 10.");
            _infoBuilder.AppendLine("2. Точка в окружности, в фигуре - угол [pi/4, pi/2], [5pi/4, 3pi/2].");
        }

        private ICollection<Vector2> ParseInput()
        {
            Console.Write("Введите целочисленные координаты точек x y: ");
            bool isParsed = new Vector2().TryParse(Console.ReadLine(), out var point, out var e);
            if (!isParsed)
            {
                Console.WriteLine("Введены некорретные данные. Повторите попытку.\n");
                return ParseInput();
            }
            return new List<Vector2>() { point };
        }
    }
}
