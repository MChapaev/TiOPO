using System.Numerics;
using LR3_Black_Box_Testing.Abstractions;

namespace LR3_Black_Box_Testing.Core
{
    public class LineChecker : IPointChecker
    {
        private int 
            _result,
            _size;

        private Vector2 _point;

        public LineChecker(int size)
        {
            _size = size;
        }

        public void SetPoint(Vector2 point)
        {
            _point = point;
        }

        public void Check()
        {
            _result =
                IsInRange() 
                ? (IsInShape() 
                    ? 3 
                    : 2) 
                : 1;
        }

        public int GetResult()
        {
            return _result;
        }

        private bool IsInRange()
        {
            return _point.LengthSquared() <= _size;
        }

        private bool IsInShape()
        {
            double angle = Math.Atan2(_point.Y, _point.X);

            if (angle < 0)
                angle += 2 * Math.PI;

            bool inFirstSector = angle >= Math.PI / 4 && angle <= Math.PI / 2;
            bool inThirdSector = angle >= 5 * Math.PI / 4 && angle <= 3 * Math.PI / 2;

            return inFirstSector || inThirdSector;
        }
    }
}
