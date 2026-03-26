namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
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

    public class CheckBoxComponent : ComponentBase
    {
        public CheckBoxComponent() : base()
        {
            this.IsChecked = false;
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
        }

        public CheckBoxComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.IsChecked = false;
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource("JsCheckBox", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBox.js", 200, typeof(CheckBoxComponent))
                    });
            }
        }

        public string CssClass { get; set; }
        public string CssClassDisabled { get; set; }
        public string OnClick { get; set; }
        public string OnChange { get; set; }
        public string Title { get; set; }

        public bool IsChecked
        {
            get { return this.HtmlAttributes.ContainsKey("checked"); }
            set
            {
                if (value) { this.HtmlAttributes["checked"] = "checked"; }
                else { if (this.HtmlAttributes.ContainsKey("checked")) this.HtmlAttributes.Remove("checked"); }
            }
        }

        public override IHtmlContent WriteHtml()
        {
            return new CheckBoxHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick), onChange = new JRaw(this.OnChange) });
            return string.Format("$('#{0}').checkBox({1});", this.Id, options);
        }
    }
}
