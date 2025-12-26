using LR10_Fluent_Assertions.Abstractions;
using LR10_Fluent_Assertions.Core;

namespace LR10_Fluent_Assertions.Data.Factories
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
