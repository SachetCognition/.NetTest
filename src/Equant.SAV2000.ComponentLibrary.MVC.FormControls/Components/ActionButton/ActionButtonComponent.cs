namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ActionButtonComponent : ComponentBase
    {
        public ActionButtonComponent() : base()
        {
            this.OnClick = "null";
            this.Title = string.Empty;
            this.Text = string.Empty;
        }

        public ActionButtonComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.OnClick = "null";
            this.Title = string.Empty;
            this.Text = string.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource("JsActionButton", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.ActionButton.js", 200, typeof(ActionButtonComponent))
                    });
            }
        }

        public string CssClass { get; set; }
        public string DialogBoxId { get; set; }
        public string CssClassReadOnly { get; set; }
        public string CssSpan { get; set; }
        public string OnClick { get; set; }
        public string Text { get; set; }

        public new bool IsDisabled
        {
            get { return this.HtmlAttributes.ContainsKey("disabled"); }
            set
            {
                if (value) { this.HtmlAttributes["disabled"] = "disabled"; }
                else { if (this.HtmlAttributes.ContainsKey("disabled")) this.HtmlAttributes.Remove("disabled"); }
            }
        }

        public string Title
        {
            get { return this.HtmlAttributes.ContainsKey("title") ? this.HtmlAttributes["title"].ToString() : null; }
            set { if (value != null) this.HtmlAttributes["title"] = value; }
        }

        public string Value
        {
            get
            {
                if (this.HtmlAttributes.ContainsKey("value"))
                    return this.HtmlAttributes["value"].ToString();
                return null;
            }
            set { this.HtmlAttributes["value"] = value; }
        }

        public override IHtmlContent WriteHtml()
        {
            return new ActionButtonHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick) });
            return string.Format("$('#{0}').actionButton({1});", this.Id, options);
        }
    }
}
