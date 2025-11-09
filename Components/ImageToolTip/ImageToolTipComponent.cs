namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.Text = string.Empty;
            this.ImageUrl = string.Empty;
            this.ToolTipText = string.Empty;
        }

        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string ToolTipText { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }

        public override void WriteHtml(TextWriter writer)
        {
            if (writer == null) throw new ArgumentException("writer cannot be null");
            if (!this.IsVisible) return;

            var img = new TagBuilder("img");
            img.MergeAttribute("src", this.ImageUrl);
            img.MergeAttribute("alt", this.Text);
            img.MergeAttribute("title", this.ToolTipText);
            img.MergeAttribute("id", this.Id);
            img.MergeAttributes(this.HtmlAttributes);

            writer.Write(img.ToString());
        }

        public override void WriteInitScript(TextWriter writer)
        {
        }
    }
}
