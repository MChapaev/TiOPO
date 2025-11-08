using System.Numerics;

namespace LR3_Black_Box_Testing.Extensions
{
    public static class Vector2Extensions
    {
        public static bool TryParse(this Vector2 vector, string? input, out Vector2 result, out Exception? exception)
        {
            try
            {
                var p = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                result = new Vector2(
                    int.Parse(p[0]),
                    int.Parse(p[1])
                );
                exception = null;
                return true;
            }
            catch (Exception e)
            {
                result = Vector2.Zero;
                exception = e;
                return false;
            }
        }
    }
}
