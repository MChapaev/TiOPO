using LR9_NSubstitute_Testing.Abstractions;
using LR9_NSubstitute_Testing.Core;

namespace LR9_NSubstitute_Testing.Data.Factories
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
