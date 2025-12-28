using Newtonsoft.Json.Linq;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;

public class VerticalMenuItemInfo
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? ActionUrl { get; set; }
    public JRaw? OnClick { get; set; }
    public bool CausesValidation { get; set; }
}
