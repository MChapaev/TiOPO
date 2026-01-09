namespace LR8_Test_Doubles.Abstractions
{
    public interface IGraphWriter
    {
        void WriteAdjacencyMatrix(int?[,] matrix, TextWriter writer, int maxRowsToDisplay);
    }
}
