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
    public class CssResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssResource"/> class.
        /// </summary>
        public CssResource(string name, string path, int order, Type componentType)
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
