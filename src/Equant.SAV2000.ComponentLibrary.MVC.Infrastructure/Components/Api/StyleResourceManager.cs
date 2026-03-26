namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Html;

    /// <summary>
    /// Manages CSS resource registrations for components.
    /// Replaces ClientDependency with a simple resource registration pattern.
    /// </summary>
    public class StyleResourceManager
    {
        private readonly List<CssResource> _registeredStyles = new List<CssResource>();
        private readonly HashSet<string> _registeredNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Registers a CSS resource. Duplicates (by name) are ignored.
        /// </summary>
        public void Register(CssResource resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            if (!_registeredNames.Contains(resource.Name))
            {
                _registeredNames.Add(resource.Name);
                _registeredStyles.Add(resource);
            }
        }

        /// <summary>
        /// Registers multiple CSS resources. Duplicates (by name) are ignored.
        /// </summary>
        public void RegisterRange(IEnumerable<CssResource> resources)
        {
            if (resources == null) return;
            foreach (var resource in resources)
            {
                Register(resource);
            }
        }

        /// <summary>
        /// Gets all registered styles ordered by priority (ascending).
        /// </summary>
        public IReadOnlyList<CssResource> GetOrderedStyles()
        {
            return _registeredStyles.OrderBy(r => r.Priority).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the count of registered styles.
        /// </summary>
        public int Count => _registeredStyles.Count;

        /// <summary>
        /// Renders all registered styles as HTML link tags, ordered by priority.
        /// </summary>
        public IHtmlContent RenderStyles()
        {
            var orderedStyles = GetOrderedStyles();
            var writer = new StringWriter();

            foreach (var style in orderedStyles)
            {
                writer.Write($"<link rel=\"stylesheet\" href=\"{style.ResourcePath}\" />\n");
            }

            return new HtmlString(writer.ToString());
        }

        /// <summary>
        /// Checks if a resource with the given name is already registered.
        /// </summary>
        public bool IsRegistered(string name)
        {
            return _registeredNames.Contains(name);
        }

        /// <summary>
        /// Clears all registered styles.
        /// </summary>
        public void Clear()
        {
            _registeredStyles.Clear();
            _registeredNames.Clear();
        }
    }
}
