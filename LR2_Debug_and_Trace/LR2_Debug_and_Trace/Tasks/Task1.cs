using System.Diagnostics;
using LR2_Debug_and_Trace.Abstractions;

namespace LR2_Debug_and_Trace.Tasks
{
    internal class Task1 : ITask
    {
        private double 
            _a,
            _b,
            _epsilon = 0.0001,
            _result;

        private int 
            _firstNameIndex = 13, // M
            _lastNameIndex = 3; // C

        private Func<double, double> _function;

        public Task1(Func<double, double> function, double a, double b)
        {
            _function = function;
            _a = a;
            _b = b;
        }

        private static void InitializeTrace()
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

        public void Run()
        {
            InitializeTrace();
            _result = ComputeIntegral();
        }

        public double Result()
        {
            Trace.Unindent();
            return _result;
        }

        private double ComputeIntegral()
        {

            int steps = 2;
            double integralOld = 0.0;
            double integralNew = 0.0;
            double delta;

            do
            {
                integralNew = RectangleMethod(steps);
                if (steps > 2)
                {
                    delta = Math.Abs(integralNew - integralOld) / (Math.Pow(2, 1) - 1);
                    Trace.WriteLine($"n={steps}, integralNew={integralNew}, integralOld={integralOld}, delta={delta}");
                }
                else
                {
                    delta = double.MaxValue;
                    Trace.WriteLine($"n={steps}, integralNew={integralNew}, integralOld={integralOld}, delta=N/A (first iteration)");
                }

                Trace.WriteLineIf(steps == _firstNameIndex, $"[FN] Интеграл на шаге {_firstNameIndex} = {integralNew}");
                Trace.WriteLineIf(steps == _lastNameIndex, $"[LN] Интеграл на шаге {_lastNameIndex} = {integralNew}");

                integralOld = integralNew;
                steps *= 2;

            } while (delta > _epsilon);

            Trace.WriteLine($"Конечный интеграл: {integralNew} с точностью {_epsilon}");
            return integralNew;
        }

        private double RectangleMethod(int n)
        {
            double h = (_b - _a) / n;
            double sum = 0.0;

            for (int i = 0; i < n; i++)
            {
                double x = _a + i * h;
                Trace.Assert(x >= _a && x <= _b, $"x вышло за границы: x={x}");
                double fx = _function(x);
                sum += fx;
                Trace.WriteLine($"i={i}, x={x}, f(x)={fx}, sum={sum}");
            }

            return sum * h;
        }
    }
}
