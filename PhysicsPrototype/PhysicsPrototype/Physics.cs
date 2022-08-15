using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PhysicsPrototype
{
    public static class Physics
    {
        public enum Vertices
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight,
        }


        // - - - - - Properties - - - - -

        public static readonly Vector2 Gravity = new Vector2(0, 6f);

        private const int _verticesCount = 4,
            A = 0,
            B = 1;

        /// <summary>
        /// Checks if two objects intersect.
        /// </summary>
        /// <param name="entities">The two <paramref name="entities"/> to be tested for collision.</param>
        /// <returns></returns>
        public static bool Intersect(params Vector2[][] entities) // This is the Separating Axis Theorem
        {
            float min, max;
            List<float> mins = new List<float>(entities.Length);
            List<float> maxs = new List<float>(entities.Length);

            Vector2 a, b, edge, axis = new Vector2();
            Vector2[] currentVertices;

            for (int s = 0; s < entities.Length; s++)
            {
                currentVertices = entities[s];
                for (int v = 0; v < _verticesCount; v++)
                {
                    // Obtain axis using edge
                    a = currentVertices[v];
                    b = currentVertices[(v + 1) % _verticesCount];
                    edge = b - a; // edge AB

                    axis.X = -edge.Y;
                    axis.Y = edge.X;

                    // Project vertices onto axis
                    foreach (var vertices in entities)
                    {
                        ProjectVertices(vertices, axis, out min, out max);
                        mins.Add(min);
                        maxs.Add(max);
                    }

                    // Check for intersection on this axis
                    if (mins[A] >= maxs[B] || mins[B] >= maxs[A])
                    {
                        return false; // can't possibly intersect on this axis
                    }
                }
            }
            return true; // must intersect
        }
        private static void ProjectVertices(Vector2[] vertices, Vector2 axis, out float min, out float max)
        {
            float projection;
            min = float.MaxValue;
            max = float.MinValue;

            foreach (var vertex in vertices)
            {
                projection = Vector2.Dot(vertex, axis);
                if (projection < min)
                {
                    min = projection;
                }
                if (projection > max)
                {
                    max = projection;
                }
            }
        }
    }
}
