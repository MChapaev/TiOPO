using System.Numerics;

namespace LR3_Black_Box_Testing.Core
{
    public class Vector2Factory
    {
        public static List<Vector2> Data(int size)
        {
            return new List<Vector2>()
            {
                // Outside the shape and the boundary
                new Vector2((float)Math.Sqrt(size) + 0.001f, 0f),
                new Vector2((float)Math.Sqrt(size) + 1f, 1f),

                // Outside the shape, inside the boundary
                new Vector2((float)Math.Sqrt(size) - 0.001f, 0f),
                new Vector2((float)Math.Sqrt(size / 2f), -(float)Math.Sqrt(size / 2f)),

                // Inside the shape and the boundary
                new Vector2((float)Math.Sqrt(size / 2f) / 2f, (float)Math.Sqrt(size / 2f) / 2f),
                new Vector2(0.5f, 1f),
                new Vector2(-(float)Math.Sqrt(size / 2f), -(float)Math.Sqrt(size / 2f)),
                new Vector2(-1f, -1.5f),
            };
        }
    }
}
