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
        float a = 0.5f;
        float b = 0.15f;
        
         vertices = new Vector3[] {
            new Vector3(0, a, 0),
            new Vector3(b, 0, 0),
            new Vector3(0, 0, b),
            new Vector3(-b, 0, 0),
            new Vector3(0, 0, -b),
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

        float transparency = 0.5f;
        
        faceColors = new Color4[] {
            new Color4(1.0f, 0.0f, 0.0f, transparency), // Red
            new Color4(1.0f, 0.5f, 0.0f, transparency), // Orange
            new Color4(0.0f, 0.0f, 1.0f, transparency), // Blue
            new Color4(0.0f, 1.0f, 1.0f, transparency), // Cyan
            new Color4(0.0f, 1.0f, 0.0f, transparency), // Lime
            new Color4(0.5f, 0.0f, 0.5f, transparency), // Purple
            new Color4(0.5f, 0.0f, 1.0f, transparency), // Magenta
            new Color4(1.0f, 0.843f, 0.0f, transparency), // Gold
        };
    }

    public void Draw()
    {
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        
        GL.Begin(BeginMode.Triangles);
        
        for (int i = 0; i < indices.Length; i++)
        {
            GL.Color4(new Color4(1.0f, 1.0f, 1.0f, 0.0f));
            GL.Color4(faceColors[i]);
            foreach (int vertexIndex in indices[i])
            {
                GL.Vertex3(vertices[vertexIndex]);
            }
        }
        GL.End();
        GL.Disable(EnableCap.Blend);
    }
}