namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomHeader
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CustomHeaderBuilder : ComponentBuilderBase<CustomHeaderComponent, CustomHeaderBuilder>
    {
        public CustomHeaderBuilder(CustomHeaderComponent component) : base(component) { }
        public CustomHeaderBuilder(CustomHeaderComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

        public CustomHeaderBuilder Title(string title)
        {
            this.Component.Title = title;
            return this;
        }

        public CustomHeaderBuilder SubTitle(string subTitle)
        {
            this.Component.SubTitle = subTitle;
            return this;
        }

        public CustomHeaderBuilder CssClass(string cssClass)
        {
            this.Component.CssClass = cssClass;
            return this;
        }

        public CustomHeaderBuilder IconCssClass(string iconCssClass)
        {
            this.Component.IconCssClass = iconCssClass;
            return this;
        }
    }
}
