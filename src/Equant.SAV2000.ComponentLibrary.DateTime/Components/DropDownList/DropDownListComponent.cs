namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent() { }

        public bool IsDivNeeded { get; set; }

        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }
}
