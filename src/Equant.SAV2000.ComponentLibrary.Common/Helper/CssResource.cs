namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    using System;

    /// <summary>
    /// Represents a CSS resource dependency for a component.
    /// </summary>
    public class CssResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CssResource"/> class.
        /// </summary>
        /// <param name="name">The unique name of the resource.</param>
        /// <param name="resourcePath">The embedded resource path.</param>
        /// <param name="priority">The load priority order.</param>
        /// <param name="componentType">The type that contains the embedded resource.</param>
        public CssResource(string name, string resourcePath, int priority, Type componentType)
        {
            this.Name = name;
            this.ResourcePath = resourcePath;
            this.Priority = priority;
            this.ComponentType = componentType;
        }

        /// <summary>
        /// Gets the unique name of the resource.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the embedded resource path.
        /// </summary>
        public string ResourcePath { get; private set; }

        /// <summary>
        /// Gets the load priority order.
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Gets the type that contains the embedded resource.
        /// </summary>
        public Type ComponentType { get; private set; }
    }
}
