namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ImageToolTipBuilder : ComponentBuilderBase<ImageToolTipComponent, ImageToolTipBuilder>
    {
        public ImageToolTipBuilder(ImageToolTipComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public ImageToolTipBuilder Text(string value)
        {
            Component.Text = value;
            return this;
        }

        public ImageToolTipBuilder ImageUrl(string value)
        {
            Component.ImageUrl = value;
            return this;
        }

        public ImageToolTipBuilder ToolTipText(string value)
        {
            Component.ToolTipText = value;
            return this;
        }
    }
}
