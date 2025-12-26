using LR9_NSubstitute_Testing.Core;

namespace LR9_NSubstitute_Testing.Abstractions
{
    public interface IShortestPathFinder
    {
        (int?[] distances, int?[] predecessors) Dijkstra(AdjacencyMatrixGraph graph, int source);
        
        int?[,] AllPairsShortestPaths(AdjacencyMatrixGraph graph);

        IList<int> ReconstructPath(int source, int target, int?[] predecessors);
    }
}
