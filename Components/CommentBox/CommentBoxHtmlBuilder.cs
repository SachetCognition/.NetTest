namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommentBoxHtmlBuilder : HtmlBuilderBase<CommentBoxComponent>
    {
        public CommentBoxHtmlBuilder(CommentBoxComponent component)
        {
            this.Component = component;
        }

        public override void Build(HtmlTextWriter writer)
        {
        }
    }
}
