namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.SpanLabel;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class CheckBoxListComponent : ComponentBase
    {
        public CheckBoxListComponent() : base()
        {
            this.SourceItems = new List<CheckBoxListItem>();
            this.CheckBoxListLabel = new SpanLabelComponent();
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
        }

        public CheckBoxListComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.SourceItems = new List<CheckBoxListItem>();
            this.CheckBoxListLabel = new SpanLabelComponent(htmlHelper);
            this.OnClick = "null";
            this.OnChange = "null";
            this.Title = string.Empty;
        }

        public string CssClassFieldSet { get; set; }
        public string CssClass { get; set; }
        public string CssClassDisabled { get; set; }
        public string OnClick { get; set; }
        public string OnChange { get; set; }
        public string Title { get; set; }
        public string CssClassOuterDiv { get; set; }
        public bool IsOuterDivNeeded { get; set; }
        public string CssClassCheckBoxDiv { get; set; }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource("JsCheckBoxList", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.CheckBoxList.js", 200, typeof(CheckBoxListComponent))
                    });
            }
        }

        public List<CheckBoxListItem> SourceItems { get; set; }
        public SpanLabelComponent CheckBoxListLabel { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new CheckBoxListHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            var options = JsonConvert.SerializeObject(new { onClick = new JRaw(this.OnClick), onChange = new JRaw(this.OnChange) });
            return string.Format("$('#{0}').checkBoxList({1});", this.Id, options);
        }
    }
}
