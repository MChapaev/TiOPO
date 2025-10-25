using LR2_Debug_and_Trace.Abstractions;
using System.Diagnostics;

namespace LR2_Debug_and_Trace.Tasks
{
    internal class Task2 : PracticeTask
    {
        private int _from = 1,
            _to;

        public Task2(int to)
        {
            _to = to;
        }

        protected override void CalculateResult()
        {
            Result = CalculateSum();
        }

        private int CalculateSum()
        {
            int sum = 0;

            for (int i = 0; i < _to; i++)
            {
                int factorial = Factorial(_from, i);
                try
                {
                    sum = checked(sum + factorial);
                }
                catch (Exception e) 
                {
                    Trace.WriteLine($"Error occured trying to sum values: {e.Message}");
                }
            }
            return sum;
        }

        private int Factorial(int from, int to)
        {
            int result = 1;
            for (int i = 1; i <= to; i++) result *= i;
            return result;
        }
    }
}
