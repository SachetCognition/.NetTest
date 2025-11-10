namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Label
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class LabelComponent : ComponentBase
    {
        public LabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.Text = string.Empty;
            this.For = string.Empty;
        }

        public string Text { get; set; }
        public string For { get; set; }
        public string CssClass { get; set; }
        public string CssClassLabel { get; set; }= string.Empty;

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }

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
            if (writer == null) throw new ArgumentException("writer cannot be null");
            if (!this.IsVisible) return;

            var label = new TagBuilder("label");
            label.MergeAttribute("id", this.Id);
            if (!string.IsNullOrEmpty(this.For))
            {
                label.MergeAttribute("for", this.For);
            }
            if (!string.IsNullOrEmpty(this.CssClass))
            {
                label.AddCssClass(this.CssClass);
            }
            label.MergeAttributes(this.HtmlAttributes);
            label.InnerHtml.Append(System.Net.WebUtility.HtmlEncode(this.Text));

            writer.Write(label.ToString());
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
