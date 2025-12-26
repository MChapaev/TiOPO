using LR9_NSubstitute_Testing.Core;
using NSubstitute;
using NUnit.Framework;

namespace LR8_Test_Doubles.Tests.NSubstitute
{
    [TestFixture]
    public class FileGraphProcessorNSubstituteTests
    {
        private FileGraphProcessor _processor;

        [SetUp]
        public void SetUp()
        {
            _processor = new FileGraphProcessor();
        }

        [Test]
        public void ReadEdges_WithValidInput_ReturnsCorrectEdges()
        {
            // Arrange
            string input = "0 1 5\n1 2 3\n0 2 8";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(3));
            Assert.That(edges[0].From, Is.EqualTo(0));
            Assert.That(edges[0].To, Is.EqualTo(1));
            Assert.That(edges[0].Weight, Is.EqualTo(5));
        }

        [Test]
        public void ReadEdges_WithSubstitutedReader_ReturnsEdges()
        {
            // Arrange
            var mockReader = Substitute.For<TextReader>();
            mockReader.ReadLine().Returns("0 1 5", "1 2 3", null);

            // Act
            var edges = _processor.ReadEdges(mockReader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
            Assert.That(edges[0].Weight, Is.EqualTo(5));
            Assert.That(edges[1].Weight, Is.EqualTo(3));
        }

        [Test]
        public void ReadEdges_WithSubstitutedReader_VerifyReadLineCallCount()
        {
            // Arrange
            var mockReader = Substitute.For<TextReader>();
            mockReader.ReadLine().Returns("0 1 5", (string)null);

            // Act
            var edges = _processor.ReadEdges(mockReader).ToList();

            // Assert
            mockReader.Received(2).ReadLine();
            Assert.That(edges, Has.Count.EqualTo(1));
        }

        [Test]
        public void ReadEdges_WithSubstitutedReader_VerifyExactCallCount()
        {
            // Arrange
            var mockReader = Substitute.For<TextReader>();
            mockReader.ReadLine().Returns("0 1 5", "1 2 3", (string)null);

            // Act
            var edges = _processor.ReadEdges(mockReader).ToList();

            // Assert
            mockReader.Received(3).ReadLine();
        }

        [Test]
        public void ReadEdges_WithTabSeparators_ReturnsCorrectEdges()
        {
            // Arrange
            string input = "0\t1\t5\n1\t2\t3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
            Assert.That(edges[0].Weight, Is.EqualTo(5));
        }

        [Test]
        public void ReadEdges_WithCommaSeparators_ReturnsCorrectEdges()
        {
            // Arrange
            string input = "0,1,5\n1,2,3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
            Assert.That(edges[0].From, Is.EqualTo(0));
        }

        [Test]
        public void ReadEdges_WithComments_SkipsCommentLines()
        {
            // Arrange
            string input = "# This is a comment\n0 1 5\n# Another comment\n1 2 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
        }

        [Test]
        public void ReadEdges_WithEmptyLines_SkipsEmptyLines()
        {
            // Arrange
            string input = "0 1 5\n\n\n1 2 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
        }

        [Test]
        public void ReadEdges_WithLeadingTrailingWhitespace_TrimsCorrectly()
        {
            // Arrange
            string input = "  0 1 5  \n  1 2 3  ";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
            Assert.That(edges[0].From, Is.EqualTo(0));
        }

        [Test]
        public void ReadEdges_WithNullReader_ThrowsArgumentNullException()
        {
            // Arrange
            TextReader reader = null;

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void ReadEdges_WithInvalidFormat_ThrowsFormatException()
        {
            // Arrange
            string input = "0 1";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Test]
        public void ReadEdges_WithNonIntegerVertex_ThrowsFormatException()
        {
            // Arrange
            string input = "a b 5";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Test]
        public void ReadEdges_WithNonIntegerWeight_ThrowsFormatException()
        {
            // Arrange
            string input = "0 1 abc";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<FormatException>(act);
        }

        [Test]
        public void ReadEdges_WithSelfLoop_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "0 0 5";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void ReadEdges_WithNegativeWeight_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "0 1 -5";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void ReadEdges_WithParallelEdges_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "0 1 5\n0 1 3";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void ReadEdges_WithParallelEdgesReversed_ThrowsInvalidOperationException()
        {
            // Arrange
            string input = "0 1 5\n1 0 3";
            using var reader = new StringReader(input);

            // Act
            TestDelegate act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }

        [Test]
        public void ReadEdges_WithZeroWeight_ReturnsEdge()
        {
            // Arrange
            string input = "0 1 0";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(1));
            Assert.That(edges[0].Weight, Is.EqualTo(0));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithValidMatrix_WritesCorrectOutput()
        {
            // Arrange
            var matrix = new int?[3, 3]
            {
                { null, 5, 8 },
                { 5, null, 3 },
                { 8, 3, null }
            };
            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 3);
            string output = writer.ToString();

            // Assert
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.That(lines, Has.Length.EqualTo(3));
            Assert.That(lines[0], Does.Contain("5"));
            Assert.That(lines[0], Does.Contain("8"));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithSubstitutedWriter_VerifiesWriteLineCalls()
        {
            // Arrange
            var matrix = new int?[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    matrix[i, j] = i * 3 + j;

            var mockWriter = Substitute.For<TextWriter>();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, mockWriter, 3);

            // Assert
            mockWriter.Received(3).WriteLine(Arg.Any<string>());
        }

        [Test]
        public void WriteAdjacencyMatrix_WithSubstitutedWriter_VerifiesDataWritten()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            var mockWriter = Substitute.For<TextWriter>();
            var capturedLines = new List<string>();
            mockWriter.WriteLine(Arg.Do<string>(line => capturedLines.Add(line)));

            // Act
            _processor.WriteAdjacencyMatrix(matrix, mockWriter, 2);

            // Assert
            Assert.That(capturedLines, Has.Count.EqualTo(2));
            Assert.That(capturedLines[0], Does.Contain("*"));
            Assert.That(capturedLines[0], Does.Contain("5"));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithMaxRowsLimit_WritesOnlySpecifiedRows()
        {
            // Arrange
            var matrix = new int?[5, 5];
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    matrix[i, j] = (i == j) ? null : i + j;

            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 2);
            string output = writer.ToString();

            // Assert
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.That(lines, Has.Length.EqualTo(2));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNullValues_WritesAsterisk()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 2);
            string output = writer.ToString();

            // Assert
            Assert.That(output, Does.Contain("*"));
            Assert.That(output, Does.Contain("5"));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithLargeMatrix_WritesOnlyFirstColumnsLimitedByMaxRowsToDisplay()
        {
            // Arrange
            var matrix = new int?[2, 5];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                    matrix[i, j] = i * 5 + j;

            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 2);
            string output = writer.ToString();

            // Assert
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            Assert.That(lines, Has.Length.EqualTo(2));
            Assert.That(lines[0], Is.EqualTo("0 1"));
        }


        [Test]
        public void WriteAdjacencyMatrix_WithNullMatrix_ThrowsArgumentNullException()
        {
            // Arrange
            int?[,] matrix = null;
            using var writer = new StringWriter();

            // Act
            TestDelegate act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 3);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNullWriter_ThrowsArgumentNullException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            TextWriter writer = null;

            // Act
            TestDelegate act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 3);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void WriteAdjacencyMatrix_WithInvalidMaxRows_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            TestDelegate act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 0);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNegativeMaxRows_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            TestDelegate act = () => _processor.WriteAdjacencyMatrix(matrix, writer, -1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Test]
        public void IntegrationTest_ProcessAndWriteMatrix_CompleteWorkflow()
        {
            // Arrange
            string input = "0 1 5\n1 2 3\n0 2 8";
            using var reader = new StringReader(input);
            using var writer = new StringWriter();

            // Act
            var edges = _processor.ReadEdges(reader).ToList();
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = graph.GetMatrixCopy();
            _processor.WriteAdjacencyMatrix(matrix, writer, 3);
            string output = writer.ToString();

            // Assert
            Assert.That(output, Is.Not.Empty);
            Assert.That(output, Does.Contain("5"));
            Assert.That(output, Does.Contain("3"));
            Assert.That(output, Does.Contain("8"));
        }

        [Test]
        public void IntegrationTest_WithSubstitutedReadAndWrite()
        {
            // Arrange
            var mockReader = Substitute.For<TextReader>();
            mockReader.ReadLine().Returns("0 1 5", "1 2 3", (string)null);

            var mockWriter = Substitute.For<TextWriter>();

            // Act
            var edges = _processor.ReadEdges(mockReader).ToList();
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            _processor.WriteAdjacencyMatrix(matrix, mockWriter, 2);

            // Assert
            Assert.That(edges, Has.Count.EqualTo(2));
            mockReader.Received(3).ReadLine();
            mockWriter.Received(2).WriteLine(Arg.Any<string>());
        }

        [Test]
        public void ReadEdges_WithExtraSpaces_ParsesCorrectly()
        {
            // Arrange
            string input = "0    1    5";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(1));
            Assert.That(edges[0].Weight, Is.EqualTo(5));
        }

        [Test]
        public void ReadEdges_WithLargeNumbers_ParsesCorrectly()
        {
            // Arrange
            string input = "999999 1000000 2147483647";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges[0].From, Is.EqualTo(999999));
            Assert.That(edges[0].Weight, Is.EqualTo(2147483647));
        }

        [Test]
        public void WriteAdjacencyMatrix_WithSingleVertex_WritesCorrectly()
        {
            // Arrange
            var matrix = new int?[1, 1] { { null } };
            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 1);
            string output = writer.ToString();

            // Assert
            Assert.That(output.Trim(), Is.EqualTo("*"));
        }

        [Test]
        public void ReadEdges_EmptyInput_ReturnsEmptyList()
        {
            // Arrange
            string input = "";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Is.Empty);
        }

        [Test]
        public void ReadEdges_OnlyComments_ReturnsEmptyList()
        {
            // Arrange
            string input = "# Comment 1\n# Comment 2\n# Comment 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Is.Empty);
        }

        [Test]
        public void ReadEdges_MultipleEdges_CountsCorrectly()
        {
            // Arrange
            string input = "0 1 1\n1 2 2\n2 3 3\n3 0 4";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            Assert.That(edges, Has.Count.EqualTo(4));
        }

        [Test]
        public void WriteAdjacencyMatrix_VerifiesWriteLineArgumentType()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            var mockWriter = Substitute.For<TextWriter>();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, mockWriter, 2);

            // Assert
            mockWriter.Received().WriteLine(Arg.Is<string>(s => !string.IsNullOrEmpty(s)));
        }

        [Test]
        public void ReadEdges_WithSubstitutedReader_VerifiesCallSequence()
        {
            // Arrange
            var mockReader = Substitute.For<TextReader>();
            var callSequence = new List<int>();
            mockReader.ReadLine().Returns(
                _ => { callSequence.Add(1); return "0 1 5"; },
                _ => { callSequence.Add(2); return "1 2 3"; },
                _ => { callSequence.Add(3); return null; }
            );

            // Act
            var edges = _processor.ReadEdges(mockReader).ToList();

            // Assert
            Assert.That(callSequence, Has.Count.EqualTo(3));
            Assert.That(edges, Has.Count.EqualTo(2));
        }

        [TearDown]
        public void TearDown()
        {
            _processor = null;
        }
    }
}
