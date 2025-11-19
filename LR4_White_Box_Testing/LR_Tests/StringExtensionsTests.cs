using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR4_White_Box_Testing.Extensions;

namespace LR_Tests
{
    [TestFixture]
    public class TrimExtensionTests
    {
        [Test]
        public void Trim_ShouldRemoveFirstAndLastCharacter_WhenStringIsLongEnough()
        {
            // Arrange
            string input = "Hello";
            string expected = "ell";

            // Act
            string result = input.MyTrim();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }

        [Test]
        public void Trim_ShouldReturnEmptyString_WhenLengthIsLessThanThree()
        {
            // Arrange
            string input = "Hi";
            string expected = string.Empty;

            // Act
            string result = input.MyTrim();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }

        [Test]
        public void Trim_ShouldThrowArgumentNullException_WhenInputIsNull()
        {
            // Arrange
            string input = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => input.MyTrim());
        }

        [Test]
        public void Trim_ShouldHandleExactlyThreeCharacterString()
        {
            // Arrange
            string input = "abc";
            string expected = "b";

            // Act
            string result = input.MyTrim();

            // Assert
            ClassicAssert.AreEqual(expected, result);
        }
    }

}
