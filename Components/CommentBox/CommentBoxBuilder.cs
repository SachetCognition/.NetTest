namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommentBoxBuilder : ComponentBuilderBase<CommentBoxComponent, CommentBoxBuilder>
    {
        public CommentBoxBuilder(CommentBoxComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
