using LR8_Test_Doubles.Core;

namespace LR8_Test_Doubles.Abstractions
{
    public interface IShortestPathFinder
    {
        (int?[] distances, int?[] predecessors) Dijkstra(AdjacencyMatrixGraph graph, int source);
        
        int?[,] AllPairsShortestPaths(AdjacencyMatrixGraph graph);

        IList<int> ReconstructPath(int source, int target, int?[] predecessors);
    }
}
