// -------------------------------------------------------------------------------------------------
//   OBS
// </copyright>
// <summary>
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;

    /// <summary>
    /// </summary>
    public class JsResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsResource"/> class.
        /// </summary>
        public JsResource(string name, string path, int order, Type componentType)
        {
            Name = name;
            Path = path;
            Order = order;
            ComponentType = componentType;
        }

        /// <summary>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// </summary>
        public Type ComponentType { get; }
    }
}
