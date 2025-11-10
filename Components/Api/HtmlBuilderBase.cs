// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.IO;

    /// <summary>
    /// </summary>
    public abstract class HtmlBuilderBase<TComponent> : IHtmlBuilder
        where TComponent : ComponentBase
    {
        /// <summary>
        /// </summary>
        protected TComponent? Component { get; set; }

        /// <summary>
        /// </summary>
        public abstract void Build(TextWriter writer);
    }
}
