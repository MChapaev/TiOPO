using System.Numerics;
using NUnit.Framework;
using LR3_Black_Box_Testing.Core;

namespace LR3_Tests
{
    public class LineCheckerTests
    {
        private const int _size = 10;

        private float _square1 => (float)Math.Sqrt(_size);
        private float _square2 => (float)Math.Sqrt(_size / 2f);

        private LineChecker checker = null!;

        [SetUp]
        public void Setup()
        {
            checker = new LineChecker(_size);
        }

        private void AssertResult(Vector2 point, int expected)
        {
            checker.SetPoint(point);
            checker.Check();

            Assert.That(checker.GetResult(), Is.EqualTo(expected),
                () => $"Expected result {expected} for point {point}");
        }

        [Test]
        public void OutsideShape_OutsideBoundary_Point1()
        {
            AssertResult(new Vector2(_square1 + 0.001f, 0f), 1);
        }

        [Test]
        public void OutsideShape_OutsideBoundary_Point2()
        {
            AssertResult(new Vector2(_square1 + 1f, 1f), 1);
        }

        [Test]
        public void OutsideShape_InsideBoundary_Point1()
        {
            AssertResult(new Vector2(_square1 - 0.001f, 0f), 2);
        }

        [Test]
        public void OutsideShape_InsideBoundary_Point2()
        {
            AssertResult(new Vector2(_square2, -_square2), 2);
        }

        [Test]
        public void InsideShape_InsideBoundary_Point1()
        {
            AssertResult(new Vector2(_square2 / 2f, _square2 / 2f), 3);
        }

        [Test]
        public void InsideShape_InsideBoundary_Point2()
        {
            AssertResult(new Vector2(0.5f, 1f), 3);
        }

        [Test]
        public void InsideShape_InsideBoundary_Point3()
        {
            AssertResult(new Vector2(-_square2, -_square2), 3);
        }

        [Test]
        public void InsideShape_InsideBoundary_Point4()
        {
            AssertResult(new Vector2(-1f, -1.5f), 3);
        }
    }
}
