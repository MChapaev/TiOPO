using LR10_Fluent_Assertions.Abstractions;
using LR10_Fluent_Assertions.Data.Models;

namespace LR10_Fluent_Assertions.Core
{
    public class FileGraphProcessor : IGraphReader, IGraphWriter
    {
        public IEnumerable<Edge> ReadEdges(TextReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));

            string? line;
            int lineNo = 0;
            var edges = new List<Edge>();

            while ((line = reader.ReadLine()) != null)
            {
                lineNo++;
                line = line.Trim();
                if (string.IsNullOrEmpty(line)) continue;
                if (line.StartsWith('#')) continue;

                var parts = line.Split(new[] {' ', '\t', ','}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 3)
                    throw new FormatException($"Invalid format at line {lineNo}: '{line}'. Expected 'from to weight'.");

                if (!int.TryParse(parts[0], out var u) ||
                    !int.TryParse(parts[1], out var v) ||
                    !int.TryParse(parts[2], out var w))
                {
                    throw new FormatException($"Invalid integers at line {lineNo}: '{line}'.");
                }

                if (u == v) throw new InvalidOperationException($"Loops are not allowed (line {lineNo}).");
                if (w < 0) throw new InvalidOperationException($"Negative weights are not allowed (line {lineNo}).");

                edges.Add(new Edge(u, v, w));
            }

            // Validate parallel edges: ensure at most one edge between pair (for undirected graph we treat {u,v} same)
            var duplicates = edges.GroupBy(e => e.From < e.To ? (e.From, e.To) : (e.To, e.From))
                                   .Where(g => g.Count() > 1)
                                   .ToList();
            if (duplicates.Any())
            {
                var pair = duplicates.First().Key;
                throw new InvalidOperationException($"Parallel edges are not allowed between {pair.Item1} and {pair.Item2}."); 
            }

            return edges;
        }

        public void WriteAdjacencyMatrix(int?[,] matrix, TextWriter writer, int maxRowsToDisplay)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            if (maxRowsToDisplay <= 0) throw new ArgumentOutOfRangeException(nameof(maxRowsToDisplay));

            int n = matrix.GetLength(0);
            int rows = Math.Min(n, maxRowsToDisplay);

            for (int i = 0; i < rows; i++)
            {
                var row = new List<string>();
                for (int j = 0; j < n; j++)
                {
                    var cell = matrix[i, j] is int v ? v.ToString() : "*";
                    row.Add(cell);
                }
                writer.WriteLine(string.Join(' ', row));
            }
        }
    }
}
