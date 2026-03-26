namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class CommentBoxHtmlBuilder : HtmlBuilderBase<CommentBoxComponent>
    {
        public CommentBoxHtmlBuilder(CommentBoxComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
