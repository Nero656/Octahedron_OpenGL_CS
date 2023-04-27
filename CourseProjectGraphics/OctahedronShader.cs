using OpenTK.Graphics.OpenGL4;

namespace CourseProjectGraphics;

public class OctahedronShader
{
    private readonly int _vertexShader = 0;
    private readonly int _fragmentShader = 0;
    private readonly int _program = 0;
    

    public OctahedronShader(string vertexStr, string fragmentStr)
    {
        _vertexShader = CreateShader(ShaderType.VertexShader, vertexStr);
        _fragmentShader = CreateShader(ShaderType.FragmentShader, fragmentStr);

        _program = GL.CreateProgram();
        
        GL.AttachShader(_program, _vertexShader);
        GL.AttachShader(_program, _fragmentShader);
        
        GL.LinkProgram(_program);
        GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out var code);

        if (code != (int) All.True)
            throw new Exception($"Ошибка линковки № {_program} \n {GL.GetProgramInfoLog(_program)}");
        
        DeleteShader(_vertexShader);
        DeleteShader(_fragmentShader);
    }

    public void Active() => GL.UseProgram(_program);
    

    public void DeActive() => GL.UseProgram(0);
    
    public void DeleteActive() => GL.DeleteProgram(_program);

    public void DeleteShader(int shader)
    {
        GL.DetachShader(_program, shader);
        GL.DeleteShader(shader);
    }



    private int CreateShader(ShaderType shaderType, string shaderStr)
    {
        int shaderId = GL.CreateShader(shaderType);
        GL.ShaderSource(shaderId, shaderStr);
        GL.CompileShader(shaderId);
    
        GL.GetShader(shaderId, ShaderParameter.CompileStatus, out var code);
        if (code != (int) All.True)
        {
            throw new Exception($"Ошибка компиляции №{shaderId} \n {GL.GetShaderInfoLog(shaderId)}");
        }
    
        return shaderId;
    }
}