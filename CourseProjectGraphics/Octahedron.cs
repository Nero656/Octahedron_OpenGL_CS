using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace CourseProjectGraphics;

public class Octahedron
{
    private Vector3[] vertices;

    private int[][] indices;

    private Color4[] faceColors;

    public Octahedron()
    {
        float a = 1.0f;
        
         vertices = new Vector3[] {
            new Vector3(0, a, 0),
            new Vector3(a, 0, 0),
            new Vector3(0, 0, a),
            new Vector3(-a, 0, 0),
            new Vector3(0, 0, -a),
            new Vector3(0, -a, 0),
        };
        
        indices = new int[][]
        {
            new int[] { 0, 1, 2 },
            new int[] { 0, 2, 3 },
            new int[] { 0, 3, 4 },
            new int[] { 0, 4, 1 },
            new int[] { 5, 2, 1 },
            new int[] { 5, 3, 2 },
            new int[] { 5, 4, 3 },
            new int[] { 5, 1, 4 },
        };

        faceColors = new Color4[] {
            Color4.Red,
            Color4.Orange,
            Color4.Blue,
            Color4.Gold,
            Color4.Lime,
            Color4.Purple,
            Color4.Cyan,
            Color4.Magenta,
        };
    }

    public void Draw()
    {
        GL.Begin(PrimitiveType.Triangles);
        for (int i = 0; i < indices.Length; i++)
        {
            GL.Color4(faceColors[i]);
            foreach (int vertexIndex in indices[i])
            {
                GL.Vertex3(vertices[vertexIndex]);
            }
        }
        GL.End();
    }
}