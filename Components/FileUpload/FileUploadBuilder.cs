namespace Equant.SAV2000.ComponentLibrary.MVC.Components.FileUpload
{
    using System.Web.Mvc;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class FileUploadBuilder : ComponentBuilderBase<FileUploadComponent, FileUploadBuilder>
    {
        public FileUploadBuilder(FileUploadComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }
    }
}
