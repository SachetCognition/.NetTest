// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Collections.ObjectModel;
    using System.IO;

    /// <summary>
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// </summary>
        ReadOnlyCollection<JsResource> JsResources { get; }

        /// <summary>
        /// </summary>
        void WriteHtml(TextWriter writer);

        /// <summary>
        /// </summary>
        void WriteInitScript(TextWriter writer);
    }
}
