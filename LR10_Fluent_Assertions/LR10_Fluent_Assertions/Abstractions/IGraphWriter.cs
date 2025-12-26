namespace LR10_Fluent_Assertions.Abstractions
{
    public interface IGraphWriter
    {
        void WriteAdjacencyMatrix(int?[,] matrix, TextWriter writer, int maxRowsToDisplay);
    }
}
