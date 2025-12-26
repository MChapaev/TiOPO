using LR9_NSubstitute_Testing.Data.Models;

namespace LR9_NSubstitute_Testing.Core
{
    public class AdjacencyMatrixGraph
    {
        private readonly int?[,] _matrix;

        public int VertexCount { get; }

        public AdjacencyMatrixGraph(int vertexCount)
        {
            if (vertexCount <= 0) throw new ArgumentOutOfRangeException(nameof(vertexCount));
            VertexCount = vertexCount;
            _matrix = new int?[vertexCount, vertexCount];
        }

        public static AdjacencyMatrixGraph FromEdges(IEnumerable<Edge> edges)
        {
            if (edges == null) throw new ArgumentNullException(nameof(edges));
            var list = edges.ToList();
            if (!list.Any()) return new AdjacencyMatrixGraph(0);

            int maxIndex = list.Max(e => Math.Max(e.From, e.To));
            var graph = new AdjacencyMatrixGraph(maxIndex + 1);

            foreach (var e in list)
            {
                if (e.From == e.To) throw new InvalidOperationException("Loops are not allowed.");

                int u = e.From, v = e.To;
                // undirected: set both [u,v] and [v,u]
                graph._matrix[u, v] = e.Weight;
                graph._matrix[v, u] = e.Weight;
            }

            return graph;
        }

        public int?[,] GetMatrixCopy()
        {
            var copy = new int?[VertexCount, VertexCount];
            Array.Copy(_matrix, copy, _matrix.Length);
            return copy;
        }

        public int? GetEdge(int u, int v)
        {
            ValidateVertex(u);
            ValidateVertex(v);
            return _matrix[u, v];
        }

        public void SetEdge(int u, int v, int weight)
        {
            ValidateVertex(u);
            ValidateVertex(v);
            if (u == v) throw new InvalidOperationException("Loops are not allowed.");
            if (weight < 0) throw new InvalidOperationException("Negative weights are not allowed.");

            _matrix[u, v] = weight;
            _matrix[v, u] = weight;
        }

        private void ValidateVertex(int v)
        {
            if (v < 0 || v >= VertexCount) throw new ArgumentOutOfRangeException(nameof(v));
        }
    }
}
