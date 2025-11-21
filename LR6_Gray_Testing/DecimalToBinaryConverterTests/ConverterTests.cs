using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR6_Gray_Testing.Abstractions;
using LR6_Gray_Testing.Core;

namespace DecimalToBinaryConverterTests
{
    [TestFixture]
    public class ConverterTests
    {
        private Convertable _converter = null!;

        [SetUp]
        public void Setup()
        {
            _converter = new DecimalToBinaryConverter();
        }

        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("2", "10")]
        [TestCase("10", "1010")]
        [TestCase("255", "11111111")]
        [TestCase("1024", "10000000000")]
        public void Convert_ValidDecimalStrings_ReturnsBinary(string input, string expected)
        {
            string result = _converter.Convert(input);
            ClassicAssert.AreEqual(expected, result);
        }

        [TestCase("-1", "11111111111111111111111111111111")]
        [TestCase("2147483647", "1111111111111111111111111111111")]
        [TestCase("-2147483648", "10000000000000000000000000000000")]
        public void Convert_EdgeCases_ReturnsCorrectBinary(string input, string expected)
        {
            string result = _converter.Convert(input);
            ClassicAssert.AreEqual(expected, result);
        }

        [TestCase("")]
        [TestCase("abc")]
        [TestCase("12.34")]
        [TestCase("9999999999999999999999")]
        public void Convert_InvalidDecimalStrings_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => _converter.Convert(input));
        }

        [Test]
        public void Convert_DoesNotReturnNull_ForValidInput()
        {
            string result = _converter.Convert("123456789");
            ClassicAssert.IsNotNull(result);
            ClassicAssert.IsNotEmpty(result);
        }
    }
}
