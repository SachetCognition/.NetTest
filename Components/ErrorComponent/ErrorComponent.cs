namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ErrorComponent : ComponentBase
    {
        public ErrorComponent() { }
        public ErrorComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents() { }
        public ErrorComponents(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public override void WriteHtml(System.IO.TextWriter writer) { }
        public override void WriteInitScript(System.IO.TextWriter writer) { }
    }
}
