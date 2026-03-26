namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextBox
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Helper;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextBoxComponent : ComponentBase
    {
        public TextBoxComponent() : base() { }
        public TextBoxComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string CssClass { get; set; }
        public string CssClassReadOnly { get; set; }
        public string Value { get; set; }
        public bool IsReadOnly { get; set; }
        public int? MaxLength { get; set; }
        public string Placeholder { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new TextBoxHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }
}
