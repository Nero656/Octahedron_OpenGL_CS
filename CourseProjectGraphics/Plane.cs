using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace CourseProjectGraphics;

public class Plane
{
    private Vector3[] vertices;

    private int[][] indices;

    private Color[] faceColors;

    public Plane()
    {
        float a = 5f;

        vertices = new Vector3[]
        {
            new Vector3(a, 0, 0),
            new Vector3(0, 0, a),
            new Vector3(-a, 0, 0),
            new Vector3(0, 0, -a),
        };

        indices = new int[][]
        {
            new int[] {0, 1, 2},
            new int[] {0, 2, 3},
        };


        float transparency = 1f;

        faceColors = new Color[]
        {
            Color.Aqua,
            Color.Red,
            Color.Yellow,
            Color.Lime,
        };
    }

    public void Draw()
    {
        GL.Begin(BeginMode.Triangles);
        for (int i = 0; i < indices.Length; i++)
        {
            
            foreach (int vertexIndex in indices[i])
            {   
                GL.Color4(faceColors[vertexIndex]);
                GL.Vertex3(vertices[vertexIndex]);
            }
        }

        GL.End();
    }
}