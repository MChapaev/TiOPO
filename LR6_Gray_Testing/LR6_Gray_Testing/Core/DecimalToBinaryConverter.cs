using System.Buffers;
using System.Globalization;
using LR6_Gray_Testing.Abstractions;

namespace LR6_Gray_Testing.Core
{

    public class DecimalToBinaryConverter : Convertable
    {
        public string Convert(string input)
        {
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value))
                throw new ArgumentException("Input is not a valid decimal integer.", nameof(input));

            if (value == 0)
                return "0";

            // Buffer pool for minimizing GC pressure
            var buffer = ArrayPool<char>.Shared.Rent(32);
            int index = buffer.Length;

            uint unsigned = (uint)value;

            // Build the binary representation from the end of the buffer
            while (unsigned > 0)
            {
                buffer[--index] = (unsigned & 1) == 1 ? '1' : '0';
                unsigned >>= 1;
            }

            var result = new string(buffer, index, buffer.Length - index);
            ArrayPool<char>.Shared.Return(buffer, clearArray: true);

            return result;
        }
    }
}
