using LR9_NSubstitute_Testing.Data.Models;

namespace LR9_NSubstitute_Testing.Abstractions
{
    public interface IGraphReader
    {
        IEnumerable<Edge> ReadEdges(TextReader reader);
    }
}
