using LR8_Tests.TestDoubles;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace LR8_Test_Doubles.Tests
{
    public class TxtFileProcessor
    {
        public string Process(TextReader reader, TextWriter writer)
        {
            var data = reader.ReadToEnd();
            var output = data.ToUpperInvariant();
            writer.Write(output);
            return output;
        }

        [TestFixture]
        public class TxtFileProcessorTests
        {
            [Test]
            public void Process_WithStringReaderAndStringWriter_ReturnsUppercasedText()
            {
                // Arrange
                var input = "Hello NUnit";
                var reader = new StringReader(input); // real StringReader used as a stub here
                var writer = new StringWriter();
                var processor = new TxtFileProcessor();


                // Act
                var result = processor.Process(reader, writer);


                // Assert
                ClassicAssert.AreEqual(input.ToUpperInvariant(), result);
                ClassicAssert.AreEqual(input.ToUpperInvariant(), writer.ToString());
            }

            [Test]
            public void Process_WithMockWriter_WriteIsCalled()
            {
                // Arrange
                var input = "call check";
                var reader = new StringReader(input);
                var mock = new MockWriter();
                var processor = new TxtFileProcessor();


                // Precondition
                ClassicAssert.IsFalse(mock.WriteCalled, "WriteCalled should be false before processing");


                // Act
                var output = processor.Process(reader, mock);


                // Assert
                ClassicAssert.IsTrue(mock.WriteCalled, "Write should have been called on the mock writer");
                ClassicAssert.AreEqual(input.ToUpperInvariant(), output);
                ClassicAssert.AreEqual(input.ToUpperInvariant(), mock.ToString());
            }

            [Test]
            public void Process_WithSpyReader_RecordsReadContent()
            {
                // Arrange
                var content = "spy me";
                var spy = new SpyReader(content);
                var writer = new StringWriter();
                var processor = new TxtFileProcessor();


                // Act
                var outStr = processor.Process(spy, writer);


                // Assert
                // SpyReader exposes the original read content
                ClassicAssert.AreEqual(content, spy.ReadContent);
                ClassicAssert.AreEqual(content.ToUpperInvariant(), outStr);
            }

            [Test]
            public void Process_WithStubReader_ReturnsStubDataUppercased()
            {
                // Arrange
                var stub = new StubReader();
                var fakeWriter = new FakeWriter();
                var processor = new TxtFileProcessor();

                // Act
                var outStr = processor.Process(stub, fakeWriter);

                // Assert
                ClassicAssert.AreEqual("STUB DATA".ToUpperInvariant(), outStr);
                ClassicAssert.AreEqual(outStr, fakeWriter.GetOutput());
            }

            [Test]
            public void Process_WithDummyWriter_DoesNotThrow()
            {
                // Arrange
                var input = "nothing special";
                var reader = new StringReader(input);
                using var dummy = new DummyWriter();
                var processor = new TxtFileProcessor();


                // Act & Assert
                Assert.DoesNotThrow(() => processor.Process(reader, dummy));
            }
        }
    }
}