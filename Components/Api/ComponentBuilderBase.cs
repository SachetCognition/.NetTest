// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// </summary>
    public abstract class ComponentBuilderBase<TComponent, TBuilder>
        where TComponent : ComponentBase
        where TBuilder : ComponentBuilderBase<TComponent, TBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentBuilderBase{TComponent, TBuilder}"/> class.
        /// </summary>
        protected ComponentBuilderBase(TComponent component, ModelMetadata? modelMetadata)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
            ModelMetadata = modelMetadata;
        }

        /// <summary>
        /// </summary>
        protected TComponent Component { get; }

        /// <summary>
        /// </summary>
        protected ModelMetadata? ModelMetadata { get; }

        /// <summary>
        /// </summary>
        public TBuilder Id(string id)
        {
            Component.Id = id;
            return (TBuilder)this;
        }

        /// <summary>
        /// </summary>
        public TBuilder Name(string name)
        {
            Component.Name = name;
            return (TBuilder)this;
        }

        /// <summary>
        /// </summary>
        public TBuilder Visible(bool isVisible)
        {
            Component.IsVisible = isVisible;
            return (TBuilder)this;
        }

        /// <summary>
        /// </summary>
        public TBuilder HtmlAttribute(string key, object value)
        {
            Component.HtmlAttributes[key] = value;
            return (TBuilder)this;
        }
    }
}
