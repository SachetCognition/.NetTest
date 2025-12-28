namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

public class CssResource
{
    public CssResource(string name, string path, int order, Type componentType)
    {
        Name = name;
        Path = path;
        Order = order;
        ComponentType = componentType;
    }

    public string Name { get; }
    public string Path { get; }
    public int Order { get; }
    public Type ComponentType { get; }
}
