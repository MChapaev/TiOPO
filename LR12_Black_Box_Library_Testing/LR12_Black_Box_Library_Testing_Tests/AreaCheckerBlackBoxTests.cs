using NUnit.Framework;
using Variant01.Abstractions;
using Variant01.Core;

namespace LR12_Black_Box_Library_Testing
{
    [TestFixture]
    public class AreaCheckerBlackBoxTests_Simple
    {
        private const double Epsilon = 0.0001;

        private IArea _checker;

        [SetUp]
        public void SetUp()
        {
            _checker = new Area(10);
        }

        [TestCase(0, 0)]
        [TestCase(5, 0)]
        [TestCase(-5, 0)]
        [TestCase(0, 5)]
        [TestCase(0, 10)]
        [TestCase(3, 4)]
        [TestCase(6, 8)]
        public void IsPointInArea_UpperHalf_Inside_ReturnsTrue(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.True);
        }

        [TestCase(10, 0)]
        [TestCase(7.071, 7.071)]
        [TestCase(-7.071, 7.071)]
        [TestCase(0, 10)]
        public void IsPointInArea_UpperHalf_BoundaryPlusEpsilon_ReturnsTrue(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x + Epsilon, y + Epsilon), Is.True);
        }

        [TestCase(15, 0)]
        [TestCase(11, 0)]
        [TestCase(8, 8)]
        [TestCase(0, 15)]
        [TestCase(-15, 0)]
        public void IsPointInArea_UpperHalf_FarOutside_ReturnsFalse(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.False);
        }

        [TestCase(-5, -5)]
        [TestCase(-10, -10)]
        [TestCase(-1, -1)]
        [TestCase(-8, -1)]
        public void IsPointInArea_LowerLeftRegion_Inside_ReturnsTrue(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.True);
        }

        [TestCase(0, -5)]
        [TestCase(0, -10)]
        [TestCase(-0.001, -5)]
        public void IsPointInArea_LowerHalf_OnOrNearRightBoundary_ReturnsTrue(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.True);
        }

        [TestCase(-10, 0)]
        [TestCase(-10, -10)]
        [TestCase(-0.1, -0.1)]
        public void IsPointInArea_Corners_ReturnsTrue(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.True);
        }

        [TestCase(1, -5)]
        [TestCase(5, -5)]
        [TestCase(2, -2)]
        public void IsPointInArea_LowerHalf_RightOfBoundary_ReturnsFalse(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.False);
        }

        [TestCase(-15, -5)]
        [TestCase(-11, -5)]
        [TestCase(-20, -1)]
        public void IsPointInArea_LowerHalf_LeftOfLeftBoundary_ReturnsFalse(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.False);
        }

        [TestCase(-5, -15)]
        [TestCase(-5, -11)]
        [TestCase(0, -15)]
        public void IsPointInArea_LowerHalf_BelowLowerBoundary_ReturnsFalse(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.False);
        }

        [TestCase(-11, -1)]
        [TestCase(-6, -6)]
        [TestCase(-11, -11)]
        public void IsPointInArea_LowerHalf_OutsideDiagonalBoundary_ReturnsFalse(double x, double y)
        {
            Assert.That(_checker.IsPointInArea(x, y), Is.False);
        }

        [Test]
        public void IsPointInArea_PointAtZeroY_BehavesConsistently()
        {
            Assert.That(_checker.IsPointInArea(0, 0), Is.True);
            Assert.That(_checker.IsPointInArea(1, 0), Is.True);
        }

        [Test]
        public void IsPointInArea_PointAtNegativeXAndZeroY_ReturnsTrue()
        {
            Assert.That(_checker.IsPointInArea(-1, 0), Is.True);
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
            Assert.That(new Area(1).IsPointInArea(0, 0), Is.True);
            Assert.That(new Area(100).IsPointInArea(0, 0), Is.True);
        }

        [TestCase(0, 1)]
        [TestCase(-1, 0)]
        [TestCase(-0.5, -0.5)]
        public void IsPointInArea_SmallRadius_PointsNearOrigin_ReturnsFalse(double x, double y)
        {
            IArea checker = new Area(0.1);
            Assert.That(checker.IsPointInArea(x, y), Is.False);
        }
    }
}
