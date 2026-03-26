namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    public abstract class ComponentBuilderBase<TComponent, TBuilder>
        where TComponent : ComponentBase
        where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
    {
        public TComponent Component { get; set; }
        public ModelMetadata ModelMetadata { get; set; }

        protected ComponentBuilderBase(TComponent component)
        {
            this.Component = component;
        }

        protected ComponentBuilderBase(TComponent component, ModelMetadata modelMetadata)
        {
            this.Component = component;
            this.ModelMetadata = modelMetadata;
        }

        public virtual TBuilder Id(string id)
        {
            this.Component.Id = id;
            return (TBuilder)this;
        }

        public virtual TBuilder Name(string name)
        {
            this.Component.Name = name;
            return (TBuilder)this;
        }
    }
}
