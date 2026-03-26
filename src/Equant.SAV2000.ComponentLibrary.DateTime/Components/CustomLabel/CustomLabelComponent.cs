namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Html;

    public class CustomLabelComponent : ComponentBase
    {
        public CustomLabelComponent()
        {
            this.DisplayColon = true;
        }

        public override string Id { get; set; }
        public string Text { get; set; }
        public new string CssClass { get; set; }
        public string ForId { get; set; }
        public string AssociatedControlId
        {
            get
            {
                if (this.HtmlAttributes.ContainsKey("for"))
                {
                    return this.HtmlAttributes["for"].ToString();
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.HtmlAttributes["for"] = value;
                }
            }
        }
        public new bool IsMandatory { get; set; }
        public bool IsOnlyForAccess { get; set; }
        public string CssClassLabel { get; set; }
        public string SuperscriptText { get; set; }
        public string SuperscriptCssClass { get; set; }
        public string SuperscriptToolTip { get; set; }
        public bool DisplayStar { get; set; }
        public bool DisplayColon { get; set; }
        public bool IsHtmlEncode { get; set; }

        public override IHtmlContent RenderHtml() { return HtmlString.Empty; }
        public override string RenderInitScript() { return string.Empty; }
    }
}
