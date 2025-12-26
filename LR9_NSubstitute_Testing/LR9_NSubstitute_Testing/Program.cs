using LR9_NSubstitute_Testing.Core;
using LR9_NSubstitute_Testing.Data.Factories;

const int _defaultMaxRowsToDisplay = 3;

var _inputFile = args.Length > 0 ? args[0] : Path.Combine(AppContext.BaseDirectory, "sample_edges.txt");
if (!File.Exists(_inputFile))
{
    Console.WriteLine($"Input file not found: {_inputFile}");
    return;
}

var _processor = new FileGraphProcessor();
using var _reader = File.OpenText(_inputFile);
var _graph = GraphFactory.CreateFrom(_reader, _processor);

var _finder = new DijkstraShortestPathFinder();
var _allPairs = _finder.AllPairsShortestPaths(_graph);

using var _writer = Console.Out;
_processor.WriteAdjacencyMatrix(_allPairs, _writer, _defaultMaxRowsToDisplay);

Console.WriteLine();
Console.WriteLine("Example: shortest path from 0 to 2 (if exists):");
var (dist, prev) = _finder.Dijkstra(_graph, 0);
if (dist.Length > 2 && dist[2] != null)
{
    var path = _finder.ReconstructPath(0, 2, prev);
    Console.WriteLine($"Distance: {dist[2]}, Path: {string.Join(' ', path)}");
}
else
{
    Console.WriteLine("No path found or vertex out of range.");
}