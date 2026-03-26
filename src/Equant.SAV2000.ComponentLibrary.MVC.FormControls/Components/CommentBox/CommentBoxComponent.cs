namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommentBoxComponent : ComponentBase
    {
        public CommentBoxComponent() : base() { }
        public CommentBoxComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string CssClass { get; set; }
        public string Value { get; set; }
        public int Rows { get; set; } = 5;
        public int Cols { get; set; } = 40;
        public int? MaxLength { get; set; }
        public bool ShowCharCount { get; set; } = true;

        public override IHtmlContent WriteHtml()
        {
            return new CommentBoxHtmlBuilder(this).Build();
        }

        public override string WriteInitScript()
        {
            if (this.ShowCharCount && this.MaxLength.HasValue)
                return string.Format("$('#{0}').savCommentBox({{maxLength: {1}}});", this.Id, this.MaxLength.Value);
            return string.Empty;
        }
    }
}
