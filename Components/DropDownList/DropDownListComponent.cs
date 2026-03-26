namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Web.Mvc;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent(HtmlHelper htmlHelper)
            : base(htmlHelper)
        {
            this.Items = new List<SelectListItem>();
        }

        public List<SelectListItem> Items { get; set; }
        public string SelectedValue { get; set; }
        public string CssClass { get; set; }
        public bool IsDivNeeded { get; set; }
        public string OnChange { get; set; }
        public string DefaultText { get; set; }
        public string CssClassSelectDiv { get; set; }
        public CustomLabelComponent CustomLabel { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get { return new ReadOnlyCollection<JsResource>(new List<JsResource>()); }
        }

        public override void WriteHtml(HtmlTextWriter writer)
        {
            new DropDownListHtmlBuilder(this).Build(writer);
        }

        public override void WriteInitScript(HtmlTextWriter writer)
        {
        }
    }
}
