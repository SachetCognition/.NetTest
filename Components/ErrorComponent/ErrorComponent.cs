namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ErrorComponent
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ErrorComponents : ComponentBase
    {
        public ErrorComponents(HtmlHelper htmlHelper) : base(htmlHelper) { }
        public string DivCssClass { get; set; }
        public string AccessibleText { get; set; }
        public List<ErrorMessageModel> ErrorCollection { get; set; }
        public override void WriteHtml(HtmlTextWriter writer) { new ErrorHtmlBuilder(this).Build(writer); }
        public override void WriteInitScript(HtmlTextWriter writer) { }
    }
}
