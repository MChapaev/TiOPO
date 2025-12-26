using LR10_Fluent_Assertions.Data.Models;

namespace LR10_Fluent_Assertions.Abstractions
{
    public interface IGraphReader
    {
        IEnumerable<Edge> ReadEdges(TextReader reader);
    }
}
