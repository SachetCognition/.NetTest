// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// </summary>
    public static class ComponentFactory
    {
        /// <summary>
        /// </summary>
        public static TComponent Create<TComponent>(IHtmlHelper htmlHelper)
            where TComponent : ComponentBase
        {
            return (TComponent)System.Activator.CreateInstance(typeof(TComponent), htmlHelper)!;
        }
    }
}
