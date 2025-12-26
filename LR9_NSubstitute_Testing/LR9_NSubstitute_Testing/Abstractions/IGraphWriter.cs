namespace LR9_NSubstitute_Testing.Abstractions
{
    public interface IGraphWriter
    {
        void WriteAdjacencyMatrix(int?[,] matrix, TextWriter writer, int maxRowsToDisplay);
    }
}
