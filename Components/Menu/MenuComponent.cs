namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class MenuComponent : ComponentBase
    {
        public MenuComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<nav id=\"{Id}\"></nav>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
