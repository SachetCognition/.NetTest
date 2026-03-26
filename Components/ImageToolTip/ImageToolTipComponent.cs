namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipComponent : ComponentBase
    {
        public ImageToolTipComponent(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string ToolTipId { get; set; }
        public string Title { get; set; }
        public string AlternateText { get; set; }
        public string Css { get; set; }
        public string CssClassSpan { get; set; }
        public string CssClassInnerSpan { get; set; }
        public PersistanceMode PersistanceMode { get; set; }
        public string CssClassImage { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }

        public override void WriteHtml(HtmlTextWriter writer) { new ImageToolTipHtmlBuilder(this).Build(writer); }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
