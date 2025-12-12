using LR8_Test_Doubles.Core;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace GraphLib.Tests
{
    public class GraphReaderWriterTests
    {
        [Test]
        public void ReadEdges_And_WriteAdjacencyMatrix_Using_StringReader_StringWriter()
        {
            var input = """
            # example
            0 1 5
            1 2 2
            3 2 7
            """;

            var reader = new StringReader(input);
            var processor = new FileGraphProcessor();
            var edges = processor.ReadEdges(reader).ToList();
            ClassicAssert.AreEqual(3, edges.Count);

            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = graph.GetMatrixCopy();

            var sw = new StringWriter();
            processor.WriteAdjacencyMatrix(matrix, sw, maxRowsToDisplay: 10);
            var output = sw.ToString().Trim();

            // expected rows: vertex count should be 4 (0..3)
            var expectedFirstLine = "* 5 * *";
            StringAssert.StartsWith(expectedFirstLine, output);
        }
    }
}
