namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropDownList
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class DropDownListComponent : ComponentBase
    {
        public DropDownListComponent() : base()
        {
            this.DataSource = new DataCollection();
            this.OnChange = "null";
        }

        public DropDownListComponent(IHtmlHelper htmlHelper) : base(htmlHelper)
        {
            this.DataSource = new DataCollection();
            this.OnChange = "null";
        }

        public override ReadOnlyCollection<JsResource> JsResources
        {
            get
            {
                return new ReadOnlyCollection<JsResource>(
                    new List<JsResource>
                    {
                        new JsResource("JsDropDownList", "Equant.SAV2000.ComponentLibrary.MVC.Resources.Javascripts.DropDownList.js", 200, typeof(DropDownListComponent))
                    });
            }
        }

        public string CssClass { get; set; }
        public string CssClassReadOnly { get; set; }
        public DataCollection DataSource { get; set; }
        public string SelectedValue { get; set; }
        public string PlaceholderText { get; set; }
        public string OnChange { get; set; }
        public string CascadeFrom { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new DropDownListHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            var options = JsonConvert.SerializeObject(new { onChange = new JRaw(this.OnChange) });
            return string.Format("$('#{0}').savDropDownList({1});", this.Id, options);
        }
    }
}
