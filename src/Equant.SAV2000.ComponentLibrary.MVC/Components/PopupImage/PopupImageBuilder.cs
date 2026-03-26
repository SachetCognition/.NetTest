namespace Equant.SAV2000.ComponentLibrary.MVC.Components.PopupImage
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class PopupImageBuilder : ComponentBuilderBase<PopupImageComponent, PopupImageBuilder>
    {
        public PopupImageBuilder(PopupImageComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public PopupImageBuilder(PopupImageComponent component)
            : base(component)
        {
        }

        public PopupImageBuilder Src(string value) { this.Component.Src = value; return this; }
        public PopupImageBuilder Alt(string value) { this.Component.Alt = value; return this; }
        public PopupImageBuilder Title(string value) { this.Component.Title = value; return this; }
        public PopupImageBuilder PopupUrl(string value) { this.Component.PopupUrl = value; return this; }
        public PopupImageBuilder PopupTitle(string value) { this.Component.PopupTitle = value; return this; }
        public PopupImageBuilder Width(int value) { this.Component.Width = value; return this; }
        public PopupImageBuilder Height(int value) { this.Component.Height = value; return this; }
        public PopupImageBuilder PopupWidth(int value) { this.Component.PopupWidth = value; return this; }
        public PopupImageBuilder PopupHeight(int value) { this.Component.PopupHeight = value; return this; }
    }
}
