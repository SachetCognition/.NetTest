namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

public class MenuItem
{
    public MenuItem()
    {
        Text = string.Empty;
        Url = "#";
        Children = new List<MenuItem>();
    }

    public string Text { get; set; }
    public string? Url { get; set; }
    public string? CssClass { get; set; }
    public string? Id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsDisabled { get; set; }
    public string? Target { get; set; }
    public string? OnClick { get; set; }
    public List<MenuItem> Children { get; set; }
}
