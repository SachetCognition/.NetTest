namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    public class HyperLinkComponent : ComponentBase
    {

        public HyperLinkComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
