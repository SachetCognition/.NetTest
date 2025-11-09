namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new System.Collections.Generic.List<JsResource>()); }
        }

        public bool IsDivNeeded { get; set; }

        public string ToHtmlString()
        {
            using (var writer = new StringWriter())
            {
                WriteHtml(writer);
                return writer.ToString();
            }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (IsVisible)
            {
                writer.Write($"<select id=\"{Id}\"></select>");
            }
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
