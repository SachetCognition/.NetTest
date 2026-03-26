namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomLabelComponent : ComponentBase
    {
        public CustomLabelComponent()
        {
            this.DisplayColon = true;
        }

        public CustomLabelComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.DisplayColon = true;
        }

        public override string Id { get; set; }
        public string Text { get; set; }
        public string CssClass { get; set; }
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
        public bool IsMandatory { get; set; }
        public bool IsOnlyForAccess { get; set; }
        public string CssClassLabel { get; set; }
        public string SuperscriptText { get; set; }
        public string SuperscriptCssClass { get; set; }
        public string SuperscriptToolTip { get; set; }
        public bool DisplayStar { get; set; }
        public bool DisplayColon { get; set; }
        public bool IsHtmlEncode { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new CustomLabelHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            return string.Empty;
        }
    }
}
