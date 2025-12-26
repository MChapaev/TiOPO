using LR10_Fluent_Assertions.Core;

namespace LR10_Fluent_Assertions.Abstractions
{
    public interface IShortestPathFinder
    {
        (int?[] distances, int?[] predecessors) Dijkstra(AdjacencyMatrixGraph graph, int source);
        
        int?[,] AllPairsShortestPaths(AdjacencyMatrixGraph graph);

        IList<int> ReconstructPath(int source, int target, int?[] predecessors);
    }
}
