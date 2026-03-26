namespace Equant.SAV2000.ComponentLibrary.MVC.Components.TextArea
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class TextAreaComponent : ComponentBase
    {
        public TextAreaComponent() : base() { }
        public TextAreaComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string CssClass { get; set; }
        public string Value { get; set; }
        public int Rows { get; set; } = 3;
        public int Cols { get; set; } = 20;
        public int? MaxLength { get; set; }
        public bool IsReadOnly { get; set; }

        public override IHtmlContent WriteHtml()
        {
            return new TextAreaHtmlBuilder(this).Build();
        }

        public override string WriteInitScript() { return string.Empty; }
    }
}
