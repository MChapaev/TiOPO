using System.Numerics;

namespace LR3_Black_Box_Testing.Abstractions
{
    public interface IPointChecker
    {
        void SetPoint(Vector2 point);

        void Check();

        int GetResult();
    }
}
