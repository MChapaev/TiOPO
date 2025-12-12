using LR8_Test_Doubles.Core;
using LR8_Test_Doubles.Data.Models;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace GraphLib.Tests
{
    public class ShortestPathTests
    {
        [Test]
        public void AllPairsShortestPaths_ComputesCorrectDistances()
        {
            // build a simple graph:
            // 0 -5- 1 -2- 2 ; 0 -9- 2
            var edges = new Edge[] {
                new Edge(0,1,5),
                new Edge(1,2,2),
                new Edge(0,2,9)
            };

            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var finder = new DijkstraShortestPathFinder();
            var matrix = finder.AllPairsShortestPaths(graph);

            // distance 0->2 should be 7 (0->1->2)
            ClassicAssert.AreEqual(7, matrix[0,2]);
            // distance 2->0 should be 7 (undirected)
            ClassicAssert.AreEqual(7, matrix[2,0]);
            // diagonal should be 0
            ClassicAssert.AreEqual(0, matrix[1,1]);
        }

        [Test]
        public void ReconstructPath_Returns_CorrectPath()
        {
            var edges = new Edge[] {
                new Edge(0,1,5),
                new Edge(1,2,2),
                new Edge(0,2,9)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var finder = new DijkstraShortestPathFinder();
            var (dist, prev) = finder.Dijkstra(graph, 0);

            var path = finder.ReconstructPath(0, 2, prev);
            ClassicAssert.AreEqual(new int[] {0,1,2}, path.ToArray());
        }
    }
}
