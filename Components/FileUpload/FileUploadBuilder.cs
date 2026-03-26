namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class FileUploadBuilder : ComponentBuilderBase<FileUploadComponent, FileUploadBuilder>
    {
        public FileUploadBuilder(FileUploadComponent component) : base(component) { }
        public FileUploadBuilder(FileUploadComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
