using NUnit.Framework;
using Variant01.Abstractions;
using Variant01.Core;

namespace LR12_Black_Box_Library_Testing
{
    public class AreaCheckerBlackBoxTests
    {
        private const double Epsilon = 0.0001;

        [TestCaseSource(nameof(PointsInUpperHalf))]
        public void IsPointInArea_PointsOnOrNearOriginInUpperHalf_ReturnsTrue(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.True);
        }

        [TestCaseSource(nameof(PointsOnBoundaryUpperHalf))]
        public void IsPointInArea_PointsOnBoundaryInUpperHalf_ReturnsTrue(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x + Epsilon, y + Epsilon), Is.True);
        }

        [TestCaseSource(nameof(PointsFarOutsideUpperHalf))]
        public void IsPointInArea_PointsFarOutsideInUpperHalf_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        [TestCaseSource(nameof(PointsInLowerLeftRegion))]
        public void IsPointInArea_PointsInLowerLeftRegion_ReturnsTrue(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.True);
        }

        [TestCaseSource(nameof(PointsOnRightBoundaryLowerHalf))]
        public void IsPointInArea_PointsOnOrNearRightBoundaryInLowerHalf_ReturnsTrue(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.True);
        }

        [TestCaseSource(nameof(CornerPoints))]
        public void IsPointInArea_PointsOnCorners_ReturnsTrue(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.True);
        }

        [TestCaseSource(nameof(PointsRightOfBoundaryLowerHalf))]
        public void IsPointInArea_PointsRightOfCertainBoundaryInLowerHalf_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        [TestCaseSource(nameof(PointsLeftOfBoundaryLowerHalf))]
        public void IsPointInArea_PointsLeftOfLeftBoundaryInLowerHalf_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        [TestCaseSource(nameof(PointsBelowBoundaryLowerHalf))]
        public void IsPointInArea_PointsBelowLowerBoundaryInLowerHalf_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        [TestCaseSource(nameof(PointsOutsideDiagonalBoundary))]
        public void IsPointInArea_PointsOutsideDiagonalBoundaryInLowerHalf_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        [Test]
        public void IsPointInArea_PointAtZeroY_BehavesConsistently()
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(0, 0), Is.True);
            Assert.That(checker.IsPointInArea(1, 0), Is.True);
        }

        [Test]
        public void IsPointInArea_PointAtNegativeXAndZeroY_ReturnsTrue()
        {
            IArea checker = new Area(10);
            Assert.That(checker.IsPointInArea(-1, 0), Is.True);
        }

        [Test]
        public void IsPointInArea_WithDifferentRadii_ScalesAppropriately()
        {
            IArea checkerSmall = new Area(5);
            IArea checkerLarge = new Area(10);

            Assert.That(checkerSmall.IsPointInArea(6, 0), Is.False);
            Assert.That(checkerLarge.IsPointInArea(6, 0), Is.True);
        }

        [Test]
        public void IsPointInArea_WithDifferentRadii_ScalesTriangleAppropriately()
        {
            IArea checkerSmall = new Area(5);
            IArea checkerLarge = new Area(10);

            Assert.That(checkerSmall.IsPointInArea(-6, -1), Is.False);
            Assert.That(checkerLarge.IsPointInArea(-6, -1), Is.True);
        }

        [Test]
        public void IsPointInArea_OriginIsAlwaysInArea()
        {
            IArea checker1 = new Area(1);
            IArea checker2 = new Area(100);

            Assert.That(checker1.IsPointInArea(0, 0), Is.True);
            Assert.That(checker2.IsPointInArea(0, 0), Is.True);
        }

        [TestCaseSource(nameof(SmallRadiusPoints))]
        public void IsPointInArea_SmallRadius_PointsNearOrigin(double x, double y)
        {
            IArea checker = new Area(0.1);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }

        private static IEnumerable<TestCaseData> PointsInUpperHalf =>
            new[]
            {
                new TestCaseData(0, 0),
                new TestCaseData(5, 0),
                new TestCaseData(-5, 0),
                new TestCaseData(0, 5),
                new TestCaseData(0, 10),
                new TestCaseData(3, 4),
                new TestCaseData(6, 8)
            };

        private static IEnumerable<TestCaseData> PointsOnBoundaryUpperHalf =>
            new[]
            {
                new TestCaseData(10, 0),
                new TestCaseData(7.071, 7.071),
                new TestCaseData(-7.071, 7.071),
                new TestCaseData(0, 10)
            };

        private static IEnumerable<TestCaseData> PointsFarOutsideUpperHalf =>
            new[]
            {
                new TestCaseData(15, 0),
                new TestCaseData(11, 0),
                new TestCaseData(8, 8),
                new TestCaseData(0, 15),
                new TestCaseData(-15, 0)
            };

        private static IEnumerable<TestCaseData> PointsInLowerLeftRegion =>
            new[]
            {
                new TestCaseData(-5, -5),
                new TestCaseData(-10, -10),
                new TestCaseData(-1, -1),
                new TestCaseData(-8, -1)
            };

        private static IEnumerable<TestCaseData> PointsOnRightBoundaryLowerHalf =>
            new[]
            {
                new TestCaseData(0, -5),
                new TestCaseData(0, -10),
                new TestCaseData(-0.001, -5)
            };

        private static IEnumerable<TestCaseData> CornerPoints =>
            new[]
            {
                new TestCaseData(-10, 0),
                new TestCaseData(-10, -10),
                new TestCaseData(-0.1, -0.1)
            };

        private static IEnumerable<TestCaseData> PointsRightOfBoundaryLowerHalf =>
            new[]
            {
                new TestCaseData(1, -5),
                new TestCaseData(5, -5),
                new TestCaseData(2, -2)
            };

        private static IEnumerable<TestCaseData> PointsLeftOfBoundaryLowerHalf =>
            new[]
            {
                new TestCaseData(-15, -5),
                new TestCaseData(-11, -5),
                new TestCaseData(-20, -1)
            };

        private static IEnumerable<TestCaseData> PointsBelowBoundaryLowerHalf =>
            new[]
            {
                new TestCaseData(-5, -15),
                new TestCaseData(-5, -11),
                new TestCaseData(0, -15)
            };

        private static IEnumerable<TestCaseData> PointsOutsideDiagonalBoundary =>
            new[]
            {
                new TestCaseData(-11, -1),
                new TestCaseData(-6, -6),
                new TestCaseData(-11, -11)
            };

        private static IEnumerable<TestCaseData> SmallRadiusPoints =>
            new[]
            {
                new TestCaseData(0, 1),
                new TestCaseData(-1, 0),
                new TestCaseData(-0.5, -0.5)
            };
    }
}
