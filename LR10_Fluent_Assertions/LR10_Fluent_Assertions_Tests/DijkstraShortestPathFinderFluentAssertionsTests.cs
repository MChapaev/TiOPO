using FluentAssertions;
using LR10_Fluent_Assertions.Core;
using LR10_Fluent_Assertions.Data.Models;
using NUnit.Framework;

namespace LR10_Fluent_Assertions_Tests.DjikstraShortestPathFinderTests
{
    [TestFixture]
    public class DijkstraShortestPathFinderFluentAssertionsTests
    {
        private DijkstraShortestPathFinder _finder;

        [SetUp]
        public void SetUp()
        {
            _finder = new DijkstraShortestPathFinder();
        }

        [Test]
        public void Dijkstra_WithNullGraph_ThrowsArgumentNullException()
        {
            AdjacencyMatrixGraph graph = null;
            Action act = () => _finder.Dijkstra(graph, 0);
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Dijkstra_WithNegativeSource_ThrowsArgumentOutOfRangeException()
        {
            var edges = new List<Edge> { new Edge(0, 1, 5) };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            Action act = () => _finder.Dijkstra(graph, -1);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Dijkstra_WithSourceOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            var edges = new List<Edge> { new Edge(0, 1, 5) };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            Action act = () => _finder.Dijkstra(graph, 5);
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void Dijkstra_WithSingleVertex_ReturnsDistanceZeroForSource()
        {
            var graph = new AdjacencyMatrixGraph(1);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist.Should().HaveCount(1);
            dist[0].Should().Be(0);
            prev[0].Should().BeNull();
        }

        [Test]
        public void Dijkstra_WithTwoConnectedVertices_ReturnsCorrectDistances()
        {
            var edges = new List<Edge> { new Edge(0, 1, 5) };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(5);
            prev[0].Should().BeNull();
            prev[1].Should().Be(0);
        }

        [Test]
        public void Dijkstra_WithSimplePath_ReturnsCorrectDistances()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(1, 2, 2)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(1);
            dist[2].Should().Be(3);
            prev[1].Should().Be(0);
            prev[2].Should().Be(1);
        }

        [Test]
        public void Dijkstra_WithMultiplePaths_ChoosesShortest()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 4),
                new Edge(0, 2, 2),
                new Edge(1, 2, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(3);
            dist[2].Should().Be(2);
            prev[2].Should().Be(0);
        }

        [Test]
        public void Dijkstra_WithComplexGraph_CalculatesAllDistances()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 10),
                new Edge(0, 2, 5),
                new Edge(1, 2, 2),
                new Edge(1, 3, 1),
                new Edge(2, 3, 9)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(7);
            dist[2].Should().Be(5);
            dist[3].Should().Be(8);
        }

        [Test]
        public void Dijkstra_WithDisconnectedVertex_ReturnsNullDistance()
        {
            var graph = new AdjacencyMatrixGraph(3);
            graph.SetEdge(0, 1, 5);
            var (dist, prev) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(5);
            dist[2].Should().BeNull();
        }

        [Test]
        public void Dijkstra_FromDifferentSources_ProducesDifferentResults()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 5),
                new Edge(1, 2, 3)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist0, _) = _finder.Dijkstra(graph, 0);
            var (dist1, _) = _finder.Dijkstra(graph, 1);

            dist0[0].Should().Be(0);
            dist0[1].Should().Be(5);
            dist0[2].Should().Be(8);
            dist1[0].Should().Be(5);
            dist1[1].Should().Be(0);
            dist1[2].Should().Be(3);
        }

        [Test]
        public void Dijkstra_ReturnsCorrectPredecessorPath()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(1, 2, 1),
                new Edge(2, 3, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);

            prev[0].Should().BeNull();
            prev[1].Should().Be(0);
            prev[2].Should().Be(1);
            prev[3].Should().Be(2);
        }

        [Test]
        public void AllPairsShortestPaths_WithNullGraph_ThrowsArgumentNullException()
        {
            AdjacencyMatrixGraph graph = null;
            Action act = () => _finder.AllPairsShortestPaths(graph);
            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void AllPairsShortestPaths_ReturnsMatrixOfCorrectSize()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 5),
                new Edge(1, 2, 3)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = _finder.AllPairsShortestPaths(graph);

            matrix.GetLength(0).Should().Be(3);
            matrix.GetLength(1).Should().Be(3);
        }

        [Test]
        public void AllPairsShortestPaths_DiagonalIsZero()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(1, 2, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = _finder.AllPairsShortestPaths(graph);

            matrix[0, 0].Should().Be(0);
            matrix[1, 1].Should().Be(0);
            matrix[2, 2].Should().Be(0);
        }

        [Test]
        public void AllPairsShortestPaths_SymmetricGraph_ReturnsSymmetricMatrix()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 5),
                new Edge(1, 2, 3)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = _finder.AllPairsShortestPaths(graph);

            matrix[0, 1].Should().Be(matrix[1, 0]);
            matrix[1, 2].Should().Be(matrix[2, 1]);
            matrix[0, 2].Should().Be(matrix[2, 0]);
        }

        [Test]
        public void AllPairsShortestPaths_CompleteGraph()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(0, 2, 4),
                new Edge(1, 2, 2)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var matrix = _finder.AllPairsShortestPaths(graph);

            matrix[0, 1].Should().Be(1);
            matrix[0, 2].Should().Be(3);
            matrix[1, 2].Should().Be(2);
        }

        [Test]
        public void ReconstructPath_SimpleCase_ReturnsCorrectPath()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(1, 2, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);
            var path = _finder.ReconstructPath(0, 2, prev);

            path.Should().HaveCount(3);
            path.Should().ContainInOrder(0, 1, 2);
        }

        [Test]
        public void ReconstructPath_SourceEqualsTarget_ReturnsOnlySource()
        {
            var edges = new List<Edge> { new Edge(0, 1, 5) };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);
            var path = _finder.ReconstructPath(0, 0, prev);

            path.Should().ContainSingle();
            path[0].Should().Be(0);
        }

        [Test]
        public void ReconstructPath_NoPathExists_ReturnsEmpty()
        {
            var graph = new AdjacencyMatrixGraph(3);
            graph.SetEdge(0, 1, 5);
            var predecessors = new int?[3] { null, 0, null };
            var path = _finder.ReconstructPath(0, 2, predecessors);

            path.Should().BeEmpty();
        }

        [Test]
        public void ReconstructPath_ComplexPath()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(1, 2, 1),
                new Edge(2, 3, 1),
                new Edge(3, 4, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);
            var path = _finder.ReconstructPath(0, 4, prev);

            path.Should().HaveCount(5);
            path.Should().ContainInOrder(0, 1, 2, 3, 4);
        }

        [Test]
        public void Dijkstra_WithLargeWeights_CalculatesCorrectly()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1000),
                new Edge(1, 2, 1000)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, _) = _finder.Dijkstra(graph, 0);

            dist[0].Should().Be(0);
            dist[1].Should().Be(1000);
            dist[2].Should().Be(2000);
        }

        [Test]
        public void Dijkstra_AlternatePathWithSmallerWeight_IsPreferred()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 100),
                new Edge(0, 2, 10),
                new Edge(1, 2, 1),
                new Edge(2, 3, 10)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (dist, _) = _finder.Dijkstra(graph, 0);

            dist[2].Should().Be(10);
            dist[3].Should().Be(20);
        }

        [Test]
        public void AllPairsShortestPaths_WithSingleVertex()
        {
            var graph = new AdjacencyMatrixGraph(1);
            var matrix = _finder.AllPairsShortestPaths(graph);

            matrix.GetLength(0).Should().Be(1);
            matrix[0, 0].Should().Be(0);
        }

        [Test]
        public void ReconstructPath_TargetEqualsSource_ReturnsPathWithOneElement()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 5),
                new Edge(1, 2, 3)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);
            var path = _finder.ReconstructPath(0, 0, prev);

            path.Should().HaveCount(1);
            path[0].Should().Be(0);
        }

        [Test]
        public void Dijkstra_PredecessorsPointToCorrectVertices()
        {
            var edges = new List<Edge>
            {
                new Edge(0, 1, 1),
                new Edge(0, 2, 1),
                new Edge(1, 3, 1),
                new Edge(2, 3, 1)
            };
            var graph = AdjacencyMatrixGraph.FromEdges(edges);
            var (_, prev) = _finder.Dijkstra(graph, 0);

            prev[0].Should().BeNull();
            prev[1].Should().Be(0);
            prev[2].Should().Be(0);
            prev[3].Should().NotBeNull();
        }

        [TearDown]
        public void TearDown()
        {
            _finder = null;
        }
    }
}
