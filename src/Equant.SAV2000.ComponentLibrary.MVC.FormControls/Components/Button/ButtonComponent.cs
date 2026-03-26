namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
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

    public class ButtonComponent : ComponentBase
    {
        public ButtonComponent() : base()
        {
            this.OnClick = "null";
            this.Title = string.Empty;
            this.Value = string.Empty;
        }

        public ButtonComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.OnClick = "null";
            this.Title = string.Empty;
            this.Value = string.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource("JsButton", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.Button.js", 200, typeof(ButtonComponent))
                    });
            }
        }

        public string CssClass { get; set; }
        public string CssClassReadOnly { get; set; }
        public string OnClick { get; set; }
        public string DialogDivId { get; set; }

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
            get { return this.HtmlAttributes.ContainsKey("value") ? this.HtmlAttributes["value"].ToString() : null; }
            set { this.HtmlAttributes["value"] = value; }
        }

        public override IHtmlContent WriteHtml()
        {
            return new ButtonHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick) });
            return string.Format("$('#{0}').savbutton({1});", this.Id, options);
        }
    }
}
