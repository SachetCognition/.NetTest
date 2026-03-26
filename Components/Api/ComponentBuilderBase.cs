namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Web.Mvc;

    public abstract class ComponentBuilderBase<TComponent, TBuilder>
        where TComponent : ComponentBase
        where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
    {
        protected ComponentBuilderBase(TComponent component, ModelMetadata modelMetadata)
        {
            this.Component = component;
            this.ModelMetadata = modelMetadata;
        }

        internal TComponent Component { get; set; }
        protected ModelMetadata ModelMetadata { get; set; }

        public virtual TBuilder Id(string id)
        {
            Component.Id = id;
            return (TBuilder)this;
        }

        public virtual TBuilder Name(string name)
        {
            Component.Name = name;
            return (TBuilder)this;
        }

        public virtual TBuilder Visible(bool isVisible)
        {
            Component.IsVisible = isVisible;
            return (TBuilder)this;
        }

        public virtual TBuilder HtmlAttributes(object attributes)
        {
            return (TBuilder)this;
        }
    }
}
