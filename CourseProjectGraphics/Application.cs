using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace CourseProjectGraphics;

public class Application : GameWindow
{
    private float changeFact = 0.0f;
    private float frameTime;
    private int fps;
    private int VertexBufferObject;
    private float speed = 1.5f;
    
    private float angle;
    private float angleRot;
    private float distance = 2;
    private Vector3 target = Vector3.Zero;

    public Application(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) :
        base(gameWindowSettings, nativeWindowSettings)
    {
        base.VSync = VSyncMode.On;
        GL.LineWidth(2.5f);
        Console.WriteLine(GL.GetString(StringName.Renderer));
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        GL.Enable(EnableCap.CullFace);
        GL.CullFace(CullFaceMode.Front);
        GL.PolygonMode(MaterialFace.Back, PolygonMode.Fill);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit| ClearBufferMask.DepthBufferBit);
        GL.ClearColor(Color.Silver);

        Sphere sphere = new Sphere(1, 128, 64);
        sphere.Draw();
        sphere.Dispose();
        
        Octahedron octahedron = new Octahedron();
        octahedron.Draw();
        
        Vector3 cameraPosition = new Vector3(
            target.X + distance * (float)Math.Sin(angle), 
            target.Y + distance * (float)Math.Sin(MathHelper.DegreesToRadians(angleRot)), 
            target.Z + distance * (float)Math.Cos(angle))
            ;

        Matrix4 viewMatrix = Matrix4.LookAt(cameraPosition, target, Vector3.UnitY);
        
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref viewMatrix);

        SwapBuffers();
        base.OnRenderFrame(args);
    }


    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        frameTime += (float) args.Time;
        fps++;
        
        if (frameTime >= 1.0f)
        {
            Title = $"Курсовой проект  FPS-{fps}";
            frameTime = 0.0f;
            fps = 0;
        }

        if (!IsFocused) // check to see if the window is focused
        {
            return;
        }
        
        KeyboardState keyboard = KeyboardState;
        
        if (keyboard.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (keyboard.IsKeyDown(Keys.Space))
        {
            target.Y +=  speed * (float)args.Time; //Up 
        }

        if (keyboard.IsKeyDown(Keys.LeftShift))
        {
            target.Y -=  speed * (float)args.Time; //Down
        } 
        
        if (keyboard.IsKeyDown(Keys.E))
        {
            angleRot += speed;
        }
        
        if (keyboard.IsKeyDown(Keys.Q))
        {
            angleRot -= speed;
        }
        
        if (keyboard.IsKeyDown(Keys.W))
        {
            distance -= speed * (float)args.Time;
        }
        
        if (keyboard.IsKeyDown(Keys.S))
        {
            distance += speed * (float)args.Time;
        }

        if (keyboard.IsKeyDown(Keys.A))
        {
            angle -= speed * (float)args.Time;
        }
        if (keyboard.IsKeyDown(Keys.D))
        {
            angle += speed * (float)args.Time;
        }

        base.OnUpdateFrame(args);
    }
    
    protected override void OnResize(ResizeEventArgs args)
    {
        base.OnResize(args);

        
        
        // Настройте матрицу проекции для 3D-перспективы
        Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(100.0f),
            (float) args.Width / args.Height, 0.1f, 100.0f);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref perspective);
        
        GL.Viewport(0, 0, args.Width, args.Height);
    }
}

