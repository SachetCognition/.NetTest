namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CommentBox
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommentBoxBuilder : ComponentBuilderBase<CommentBoxComponent, CommentBoxBuilder>
    {
        public CommentBoxBuilder(CommentBoxComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CommentBoxBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public CommentBoxBuilder Value(string value) { Component.Value = value; return this; }
        public CommentBoxBuilder Rows(int value) { Component.Rows = value; return this; }
        public CommentBoxBuilder Cols(int value) { Component.Cols = value; return this; }
        public CommentBoxBuilder MaxLength(int value) { Component.MaxLength = value; return this; }
        public CommentBoxBuilder ShowCharCount(bool value) { Component.ShowCharCount = value; return this; }
        public CommentBoxBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
    }
}
