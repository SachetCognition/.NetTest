namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class FileUploadBuilder : ComponentBuilderBase<FileUploadComponent, FileUploadBuilder>
    {
        public FileUploadBuilder(FileUploadComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public FileUploadBuilder CssClass(string value) { Component.CssClass = value; return this; }
        public FileUploadBuilder Accept(string value) { Component.Accept = value; return this; }
        public FileUploadBuilder AllowMultiple(bool value) { Component.AllowMultiple = value; return this; }
        public FileUploadBuilder Disabled(bool value) { Component.IsDisabled = value; return this; }
    }
}
