using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CourseProjectGraphics;

public class Cone
{
    private float radius;
    private float height;
    private int segments;
    public Cone(float radius, float height, int segments)
    {
        this.radius = radius;
        this.height = height;
        this.segments = segments;
        
        float transparency = 0.5f;
    }

    public void Draw()
    {
        GL.PushMatrix();
        
        GL.Translate(0, -height / 2, 0);
        GL.Color4(Color.Indigo);
        GL.Begin(PrimitiveType.TriangleFan);
            GL.Vertex3(0, height, 0);
            for (int i = 0; i <= segments; i++)
            {
                float angle = i * 2.0f * (float)Math.PI / segments;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius;
                GL.Vertex3(x, 0, z);
            }
            GL.End();

            GL.Begin(PrimitiveType.TriangleStrip);
            for (int i = 0; i <= segments; i++)
            {
                float angle = i * 2.0f * (float)Math.PI / segments;
                float x = (float)Math.Cos(angle) * radius;
                float z = (float)Math.Sin(angle) * radius;
                GL.Vertex3(x, 0, z);
                GL.Vertex3(0,height, 0 );
            }
        GL.End();
        GL.PopMatrix();
    }
}