using NUnit.Framework;
using NUnit.Framework.Legacy;
using LR8_Test_Doubles.Core;
using LR8_Test_Doubles.Data.Models;
using LR8_Tests.TestDoubles;

namespace LR8_Tests
{
    [TestFixture]
    public class FileGraphProcessorTests
    {
        private FileGraphProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new FileGraphProcessor();
        }

        #region ReadEdges

        [Test]
        public void ReadEdges_ValidInput_ReturnsCorrectEdges()
        {
            var reader = new StubTextReader(new[] { "0 1 5", "1 2 3", "0 2 10" });
            var edges = _processor.ReadEdges(reader).ToList();

            ClassicAssert.AreEqual(3, edges.Count);
            ClassicAssert.AreEqual(new Edge(0, 1, 5), edges[0]);
            ClassicAssert.AreEqual(new Edge(1, 2, 3), edges[1]);
            ClassicAssert.AreEqual(new Edge(0, 2, 10), edges[2]);
        }

        [Test]
        public void ReadEdges_WithEmptyLinesAndComments_IgnoresThem()
        {
            var reader = new StubTextReader(new[]
            {
                "# comment", "", "0 1 5", " ", "# another", "1 2 3"
            });

            var edges = _processor.ReadEdges(reader).ToList();

            ClassicAssert.AreEqual(2, edges.Count);
            ClassicAssert.AreEqual(new Edge(0, 1, 5), edges[0]);
            ClassicAssert.AreEqual(new Edge(1, 2, 3), edges[1]);
        }

        [Test]
        public void ReadEdges_VariousSeparators_ParsesCorrectly()
        {
            var reader = new StubTextReader(new[] { "0 1 5", "2\t3\t7", "4,5,9" });
            var edges = _processor.ReadEdges(reader).ToList();

            ClassicAssert.AreEqual(3, edges.Count);
            ClassicAssert.AreEqual(new Edge(2, 3, 7), edges[1]);
        }

        [Test]
        public void ReadEdges_WithMock_VerifiesReadCallCount()
        {
            var reader = new MockTextReader(new[] { "0 1 5", "1 2 3" });
            _processor.ReadEdges(reader).ToList();

            reader.VerifyReadWasCalled();
            reader.VerifyReadCallCount(3);
        }

        [Test]
        public void ReadEdges_WithSpy_TracksAllReadRequests()
        {
            var reader = new SpyTextReader(new[] { "0 1 5", "#", "", "1 2 3" });
            var edges = _processor.ReadEdges(reader).ToList();

            ClassicAssert.AreEqual(5, reader.ReadLineCallCount);
            ClassicAssert.AreEqual(2, edges.Count);
            ClassicAssert.Contains("0 1 5", reader.RequestedLines);
        }

        [Test]
        public void ReadEdges_NullReader_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _processor.ReadEdges(null));
        }

        #endregion

        #region WriteAdjacencyMatrix 

        [Test]
        public void WriteAdjacencyMatrix_WithMock_VerifiesWriteCallCount()
        {
            var matrix = new int?[3, 3];
            var writer = new MockTextWriter();

            _processor.WriteAdjacencyMatrix(matrix, writer, 3);

            writer.VerifyWriteWasCalled();
            writer.VerifyWriteCallCount(3);
        }

        [Test]
        public void WriteAdjacencyMatrix_WithSpy_TracksAllWrittenLines()
        {
            var matrix = new int?[2, 2];
            var writer = new SpyTextWriter();

            _processor.WriteAdjacencyMatrix(matrix, writer, 2);

            ClassicAssert.AreEqual(2, writer.WriteLineCallCount);
            ClassicAssert.AreEqual(2, writer.WrittenLines.Count);
        }

        [Test]
        public void WriteAdjacencyMatrix_NullMatrix_ThrowsArgumentNullException()
        {
            using var writer = new StringWriter();
            Assert.Throws<ArgumentNullException>(() =>
                _processor.WriteAdjacencyMatrix(null, writer, 3));
        }

        #endregion

        [TearDown]
        public void TearDown()
        {
            _processor = null;
        }
    }
}
