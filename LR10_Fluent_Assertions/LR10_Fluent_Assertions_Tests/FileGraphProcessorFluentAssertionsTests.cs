using FluentAssertions;
using LR10_Fluent_Assertions.Core;
using LR10_Fluent_Assertions.Data.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LR8_Test_Doubles.Tests.FluentAssertions
{
    [TestFixture]
    public class FileGraphProcessorFluentAssertionsTests
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
            var input = "0 1 5\n1 2 3\n0 2 8";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(3);
            edges[0].From.Should().Be(0);
            edges[0].To.Should().Be(1);
            edges[0].Weight.Should().Be(5);
        }

        [Test]
        public void ReadEdges_WithTabSeparators_ReturnsCorrectEdges()
        {
            // Arrange
            var input = "0\t1\t5\n1\t2\t3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(2);
            edges[0].Weight.Should().Be(5);
        }

        [Test]
        public void ReadEdges_WithCommaSeparators_ReturnsCorrectEdges()
        {
            // Arrange
            var input = "0,1,5\n1,2,3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(2);
            edges[0].From.Should().Be(0);
            edges[0].To.Should().Be(1);
            edges[0].Weight.Should().Be(5);
        }

        [Test]
        public void ReadEdges_WithComments_SkipsCommentLines()
        {
            // Arrange
            var input = "# This is a comment\n0 1 5\n# Another comment\n1 2 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(2);
        }

        [Test]
        public void ReadEdges_WithEmptyLines_SkipsEmptyLines()
        {
            // Arrange
            var input = "0 1 5\n\n\n1 2 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(2);
        }

        [Test]
        public void ReadEdges_WithLeadingTrailingWhitespace_TrimsCorrectly()
        {
            // Arrange
            var input = "  0 1 5  \n  1 2 3  ";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(2);
            edges[0].From.Should().Be(0);
            edges[1].To.Should().Be(2);
        }

        [Test]
        public void ReadEdges_EmptyInput_ReturnsEmptyList()
        {
            // Arrange
            using var reader = new StringReader(string.Empty);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().BeEmpty();
        }

        [Test]
        public void ReadEdges_OnlyComments_ReturnsEmptyList()
        {
            // Arrange
            var input = "# Comment 1\n# Comment 2\n# Comment 3";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().BeEmpty();
        }

        [Test]
        public void ReadEdges_WithNullReader_ThrowsArgumentNullException()
        {
            // Arrange
            TextReader reader = null;

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void ReadEdges_WithInvalidFormat_ThrowsFormatException()
        {
            // Arrange
            var input = "0 1";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Test]
        public void ReadEdges_WithNonIntegerVertex_ThrowsFormatException()
        {
            // Arrange
            var input = "a b 5";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Test]
        public void ReadEdges_WithNonIntegerWeight_ThrowsFormatException()
        {
            // Arrange
            var input = "0 1 abc";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<FormatException>();
        }

        [Test]
        public void ReadEdges_WithSelfLoop_ThrowsInvalidOperationException()
        {
            // Arrange
            var input = "0 0 5";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ReadEdges_WithNegativeWeight_ThrowsInvalidOperationException()
        {
            // Arrange
            var input = "0 1 -5";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ReadEdges_WithParallelEdges_ThrowsInvalidOperationException()
        {
            // Arrange
            var input = "0 1 5\n0 1 3";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ReadEdges_WithParallelEdgesReversed_ThrowsInvalidOperationException()
        {
            // Arrange
            var input = "0 1 5\n1 0 3";
            using var reader = new StringReader(input);

            // Act
            Action act = () => _processor.ReadEdges(reader).ToList();

            // Assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void ReadEdges_WithZeroWeight_ReturnsEdge()
        {
            // Arrange
            var input = "0 1 0";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().ContainSingle();
            edges[0].Weight.Should().Be(0);
        }

        [Test]
        public void ReadEdges_WithExtraSpaces_ParsesCorrectly()
        {
            // Arrange
            var input = "0    1    5";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(1);
            edges[0].From.Should().Be(0);
            edges[0].To.Should().Be(1);
            edges[0].Weight.Should().Be(5);
        }

        [Test]
        public void ReadEdges_WithLargeNumbers_ParsesCorrectly()
        {
            // Arrange
            var input = "999999 1000000 2147483647";
            using var reader = new StringReader(input);

            // Act
            var edges = _processor.ReadEdges(reader).ToList();

            // Assert
            edges.Should().HaveCount(1);
            edges[0].From.Should().Be(999999);
            edges[0].To.Should().Be(1000000);
            edges[0].Weight.Should().Be(2147483647);
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
            var output = writer.ToString();

            // Assert
            output.Should().NotBeNullOrEmpty();
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            lines.Should().HaveCount(3);
            lines[0].Should().Contain("5");
            lines[0].Should().Contain("8");
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
            var output = writer.ToString();

            // Assert
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            lines.Should().HaveCount(2);
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNullValues_WritesAsterisk()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 2);
            var output = writer.ToString();

            // Assert
            output.Should().Contain("*");
            output.Should().Contain("5");
        }

        [Test]
        public void WriteAdjacencyMatrix_WithRectangularMatrix_RespectsMaxRowsToDisplayAsSquare()
        {
            // Arrange
            var matrix = new int?[2, 5];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                    matrix[i, j] = i * 5 + j;

            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 2);
            var output = writer.ToString();

            // Assert
            var lines = output.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            lines.Should().HaveCount(2);
            lines[0].Trim().Should().Be("0 1");
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNullMatrix_ThrowsArgumentNullException()
        {
            // Arrange
            int?[,] matrix = null;
            using var writer = new StringWriter();

            // Act
            Action act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 3);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNullWriter_ThrowsArgumentNullException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            TextWriter writer = null;

            // Act
            Action act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 3);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void WriteAdjacencyMatrix_WithInvalidMaxRows_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            Action act = () => _processor.WriteAdjacencyMatrix(matrix, writer, 0);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void WriteAdjacencyMatrix_WithNegativeMaxRows_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var matrix = new int?[2, 2] { { null, 5 }, { 5, null } };
            using var writer = new StringWriter();

            // Act
            Action act = () => _processor.WriteAdjacencyMatrix(matrix, writer, -1);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void WriteAdjacencyMatrix_WithSingleVertex_WritesCorrectly()
        {
            // Arrange
            var matrix = new int?[1, 1] { { null } };
            using var writer = new StringWriter();

            // Act
            _processor.WriteAdjacencyMatrix(matrix, writer, 1);
            var output = writer.ToString();

            // Assert
            output.Trim().Should().Be("*");
        }

        [Test]
        public void IntegrationTest_ProcessAndWriteMatrix_CompleteWorkflow()
        {
            // Arrange
            var input = "0 1 5\n1 2 3\n0 2 8";
            using var reader = new StringReader(input);
            using var writer = new StringWriter();

            // Act
            var edges = _processor.ReadEdges(reader).ToList();
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = graph.GetMatrixCopy();
            _processor.WriteAdjacencyMatrix(matrix, writer, 3);
            var output = writer.ToString();

            // Assert
            output.Should().NotBeNullOrEmpty();
            output.Should().Contain("5");
            output.Should().Contain("3");
            output.Should().Contain("8");
        }

        [TearDown]
        public void TearDown()
        {
            _processor = null;
        }
    }
}
