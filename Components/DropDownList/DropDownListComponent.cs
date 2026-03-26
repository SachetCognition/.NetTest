namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent() { }
        public DropDownListComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public bool IsDivNeeded { get; set; }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
