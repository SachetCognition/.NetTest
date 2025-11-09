// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// </summary>
    public abstract class ComponentBase : IComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentBase"/> class.
        /// </summary>
        protected ComponentBase(IHtmlHelper htmlHelper)
        {
            HtmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            HtmlAttributes = new Dictionary<string, object>();
            Id = string.Empty;
            Name = string.Empty;
            IsVisible = true;
        }

        /// <summary>
        /// </summary>
        public IHtmlHelper HtmlHelper { get; }

        /// <summary>
        /// </summary>
        public Dictionary<string, object> HtmlAttributes { get; }

        /// <summary>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public bool IsVisible { get; set; }

        /// <summary>
        /// </summary>
        public System.Text.StringBuilder ValidationString { get; set; } = new System.Text.StringBuilder();

        /// <summary>
        /// </summary>
        public Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata? ModelMetadata { get; set; }

        /// <summary>
        /// </summary>
        public abstract ReadOnlyCollection<JsResource> JsResources { get; }

        /// <summary>
        /// </summary>
        public virtual ReadOnlyCollection<CssResource> CssResources
        {
            get { return new ReadOnlyCollection<CssResource>(new List<CssResource>()); }
        }

        /// <summary>
        /// </summary>
        public abstract void WriteHtml(TextWriter writer);

        /// <summary>
        /// </summary>
        public abstract void WriteInitScript(TextWriter writer);
    }
}
