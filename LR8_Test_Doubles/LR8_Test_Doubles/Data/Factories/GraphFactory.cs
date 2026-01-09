using LR8_Test_Doubles.Abstractions;
using LR8_Test_Doubles.Core;

namespace LR8_Test_Doubles.Data.Factories
{
    public static class GraphFactory
    {
        public static AdjacencyMatrixGraph CreateFrom(TextReader reader, IGraphReader parser)
        {
            var edges = parser.ReadEdges(reader);
            return AdjacencyMatrixGraph.FromEdges(edges);
        }
    }
}
