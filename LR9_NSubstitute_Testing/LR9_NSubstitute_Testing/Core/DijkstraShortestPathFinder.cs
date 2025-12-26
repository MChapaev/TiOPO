using LR9_NSubstitute_Testing.Abstractions;

namespace LR9_NSubstitute_Testing.Core
{
    public class DijkstraShortestPathFinder : IShortestPathFinder
    {
        public (int?[] distances, int?[] predecessors) Dijkstra(AdjacencyMatrixGraph graph, int source)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (source < 0 || source >= graph.VertexCount) throw new ArgumentOutOfRangeException(nameof(source));

            int n = graph.VertexCount;
            var dist = new int?[n];
            var prev = new int?[n];
            var visited = new bool[n];

            for (int i = 0; i < n; i++) dist[i] = null;

            dist[source] = 0;

            for (int iter = 0; iter < n; iter++)
            {
                int? u = null;
                int? best = null;
                for (int i = 0; i < n; i++)
                {
                    if (visited[i]) continue;
                    if (dist[i] == null) continue;
                    if (best == null || dist[i] < best)
                    {
                        best = dist[i];
                        u = i;
                    }
                }

                if (u == null) break;
                int ui = u.Value;
                visited[ui] = true;

                for (int v = 0; v < n; v++)
                {
                    var w = graph.GetEdge(ui, v);
                    if (w == null) continue;
                    int alt = dist[ui].Value + w.Value;
                    if (dist[v] == null || alt < dist[v].Value)
                    {
                        dist[v] = alt;
                        prev[v] = ui;
                    }
                }
            }

            return (dist, prev);
        }

        public int?[,] AllPairsShortestPaths(AdjacencyMatrixGraph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            int n = graph.VertexCount;
            var matrix = new int?[n, n];
            for (int s = 0; s < n; s++)
            {
                var (dist, _) = Dijkstra(graph, s);
                for (int t = 0; t < n; t++)
                    matrix[s, t] = dist[t];
            }
            return matrix;
        }

        public IList<int> ReconstructPath(int source, int target, int?[] predecessors)
        {
            var path = new List<int>();
            int? cur = target;
            while (cur != null && cur != source)
            {
                path.Add(cur.Value);
                cur = predecessors[cur.Value];
            }
            if (cur == null) return Array.Empty<int>();
            path.Add(source);
            path.Reverse();
            return path;
        }
    }
}
