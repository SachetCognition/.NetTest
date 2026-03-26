namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class CommentBoxBuilder : ComponentBuilderBase<CommentBoxComponent, CommentBoxBuilder>
    {
        public CommentBoxBuilder(CommentBoxComponent component) : base(component) { }
        public CommentBoxBuilder(CommentBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
