using System.Numerics;
using NUnit.Framework;
using LR3_Black_Box_Testing.Core;

namespace LR3_Tests
{
    public class LineCheckerTests
    {
        private const int _size = 10;

        private static float _square1 => (float)Math.Sqrt(_size);
        private static float _square2 => (float)Math.Sqrt(_size / 2f);

        public static IEnumerable<TestCaseData> Points =>
            new List<TestCaseData>
            {
                // Outside shape & outside boundary. Result = 1
                new TestCaseData(new Vector2(_square1 + 0.001f, 0f), 1)
                    .SetName("OutsideShape_OutsideBoundary_Point1"),

                new TestCaseData(new Vector2(_square1 + 1f, 1f), 1)
                    .SetName("OutsideShape_OutsideBoundary_Point2"),

                // Outside shape & inside boundary. Result = 2
                new TestCaseData(new Vector2(_square1 - 0.001f, 0f), 2)
                    .SetName("OutsideShape_InsideBoundary_Point1"),

                new TestCaseData(new Vector2(_square2, -_square2), 2)
                    .SetName("OutsideShape_InsideBoundary_Point2"),

                // Inside shape & inside boundary. Result = 3
                new TestCaseData(new Vector2(_square2 / 2f, _square2 / 2f), 3)
                    .SetName("InsideShape_InsideBoundary_Point1"),

                new TestCaseData(new Vector2(0.5f, 1f), 3)
                    .SetName("InsideShape_InsideBoundary_Point2"),

                new TestCaseData(new Vector2(-_square2, -_square2), 3)
                    .SetName("InsideShape_InsideBoundary_Point3"),

                new TestCaseData(new Vector2(-1f, -1.5f), 3)
                    .SetName("InsideShape_InsideBoundary_Point4"),
            };

        [Test]
        [TestCaseSource(nameof(Points))]
        public void TestLineChecker(Vector2 point, int expected)
        {
            var checker = new LineChecker(_size);
            checker.SetPoint(point);
            checker.Check();

            Assert.That(checker.GetResult(), Is.EqualTo(expected));
        }
    }
}
