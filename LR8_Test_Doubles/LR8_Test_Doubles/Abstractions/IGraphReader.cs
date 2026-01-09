using LR8_Test_Doubles.Data.Models;

namespace LR8_Test_Doubles.Abstractions
{
    public interface IGraphReader
    {
        IEnumerable<Edge> ReadEdges(TextReader reader);
    }
}
