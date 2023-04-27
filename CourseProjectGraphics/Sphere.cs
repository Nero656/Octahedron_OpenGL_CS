using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace CourseProjectGraphics;

public class Sphere
{
        private readonly int _vao;
        private readonly int _vertexBuffer;
        private readonly int _indexBuffer;
        private readonly int _vertexCount;

        public Sphere(float radius, int slices, int stacks)
        {
            var vertices = new Vector3[(slices + 1) * (stacks + 1)];
            var indices = new int[slices * stacks * 6];

            for (int i = 0; i <= stacks; i++)
            {
                float v = 1.0f - (float)i / stacks;
                float phi = v * MathHelper.Pi;

                for (int j = 0; j <= slices; j++)
                {
                    float u = (float)j / slices;
                    float theta = u * MathHelper.TwoPi;

                    float x = radius * MathF.Sin(phi) * MathF.Cos(theta);
                    float y = radius * MathF.Sin(phi) * MathF.Sin(theta);
                    float z = radius * MathF.Cos(phi);

                    vertices[i * (slices + 1) + j] = new Vector3(x, y, z);
                }
            }

            int index = 0;

            for (int i = 0; i < stacks; i++)
            {
                for (int j = 0; j < slices; j++)
                {
                    indices[index++] = i * (slices + 1) + j;
                    indices[index++] = i * (slices + 1) + j + 1;
                    indices[index++] = (i + 1) * (slices + 1) + j;

                    indices[index++] = (i + 1) * (slices + 1) + j;
                    indices[index++] = i * (slices + 1) + j + 1;
                    indices[index++] = (i + 1) * (slices + 1) + j + 1;
                }
            }

            GL.GenVertexArrays(1, out _vao);
            GL.BindVertexArray(_vao);

            GL.GenBuffers(1, out _vertexBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * vertices.Length * 3, vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);

            GL.GenBuffers(1, out _indexBuffer);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(int) * indices.Length, indices, BufferUsageHint.StaticDraw);

            _vertexCount = indices.Length;
        }
    
        public void Draw()
        {
            GL.Enable(EnableCap.Blend);
            GL.BindVertexArray(_vao);
            GL.Color4(.0f, 0f, .5f, 1);
            GL.DrawElements(BeginMode.Triangles, _vertexCount, DrawElementsType.UnsignedInt, 0);
            GL.Disable(EnableCap.Blend);
        }
        
        public void Dispose()
        {
            GL.DeleteBuffer(_vertexBuffer);
            GL.DeleteBuffer(_indexBuffer);
            GL.DeleteVertexArray(_vao);
        }
}