namespace Equant.SAV2000.ComponentLibrary.MVC.Components.RadioButton
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RadioButtonComponent : ComponentBase
    {
        public RadioButtonComponent() : base() { }
        public RadioButtonComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string CssClass { get; set; }
        public string GroupName { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
        public string OnChange { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new RadioButtonHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }
}
