// See https://aka.ms/new-console-template for more information


using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using CourseProjectGraphics;

var nativeWindowSettings = new NativeWindowSettings()
{
    //Form
    Size = new Vector2i(1280, 800),
    //OpenGL settings
    APIVersion = new Version(3,3),
    Flags = ContextFlags.Default,
    Profile = ContextProfile.Compatability,
    API = ContextAPI.OpenGL,
    NumberOfSamples = 0,
    
};

using (Application app = new Application(GameWindowSettings.Default,nativeWindowSettings))
{
    app.Run();
}