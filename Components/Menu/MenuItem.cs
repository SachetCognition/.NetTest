namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

public class MenuItem
{
    public MenuItem()
    {
        Text = string.Empty;
        Url = "#";
        Children = new List<MenuItem>();
        MenuType = EMenuCtrlType.Redirect;
    }

    public string Text { get; set; }
    public string? MenuName { get; set; }
    public string? ActionName { get; set; }
    public string? Url { get; set; }
    public string? ActionUrl { get; set; }
    public string? CssClass { get; set; }
    public string? Id { get; set; }
    public bool IsSelected { get; set; }
    public bool IsDisabled { get; set; }
    public string? Target { get; set; }
    public string? OnClick { get; set; }
    public string? OnclickEvent { get; set; }
    public EMenuCtrlType MenuType { get; set; }
    public bool Hidden { get; set; }
    public List<MenuItem> Children { get; set; }

    public List<MenuItem>? ReturnChildMenu()
    {
        return Children?.Count > 0 ? Children : null;
    }
}
