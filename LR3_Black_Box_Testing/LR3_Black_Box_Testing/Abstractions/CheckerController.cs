using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace LR3_Black_Box_Testing.Abstractions
{
    public abstract class CheckerController
    {
        private IPointChecker _checker = null!;
        private ICollection<Vector2> _data = new List<Vector2>();
        private ICollection<int> _result = new List<int>();

        protected StringBuilder ResultBuilder = new StringBuilder();

        public CheckerController(IPointChecker checker) => _checker = checker;

        public abstract CheckerController PrintInfo(string? info = null);

        public virtual CheckerController SetupData(ICollection<Vector2>? data = null)
        {
            _data = data!;
            return this;
        }

        public CheckerController Run()
        {
            _result = _data
                .Select(point =>
                {
                    _checker.SetPoint(point);
                    _checker.Check();
                    return _checker.GetResult();
                })
                .ToList();
            return this;
        }

        public void PrintResult()
        {
            ConvertResult();
            OutputResult();
        }

        private void ConvertResult()
        {
            int resultIndex = 1;
            foreach (var item in _result)
                ResultBuilder.AppendLine($"Результат {resultIndex++}: {item}");
        }

        protected abstract void OutputResult();

    }
}
