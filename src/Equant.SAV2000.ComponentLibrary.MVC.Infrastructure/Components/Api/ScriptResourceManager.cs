namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Html;

    /// <summary>
    /// Manages JavaScript resource registrations for components.
    /// Replaces ClientDependency with a simple resource registration pattern.
    /// Supports priority ordering, deduplication, and conditional loading.
    /// </summary>
    public class ScriptResourceManager
    {
        private readonly List<JsResource> _registeredScripts = new List<JsResource>();
        private readonly HashSet<string> _registeredNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Registers a JavaScript resource. Duplicates (by name) are ignored.
        /// </summary>
        public void Register(JsResource resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            if (!_registeredNames.Contains(resource.Name))
            {
                _registeredNames.Add(resource.Name);
                _registeredScripts.Add(resource);
            }
        }

        /// <summary>
        /// Registers multiple JavaScript resources. Duplicates (by name) are ignored.
        /// </summary>
        public void RegisterRange(IEnumerable<JsResource> resources)
        {
            if (resources == null) return;
            foreach (var resource in resources)
            {
                Register(resource);
            }
        }

        /// <summary>
        /// Conditionally registers a resource only if the condition is true.
        /// Used for filter scripts that are only needed when IsFilter=true.
        /// </summary>
        public void RegisterConditional(JsResource resource, bool condition)
        {
            if (condition)
            {
                Register(resource);
            }
        }

        /// <summary>
        /// Gets all registered scripts ordered by priority (ascending).
        /// </summary>
        public IReadOnlyList<JsResource> GetOrderedScripts()
        {
            return _registeredScripts.OrderBy(r => r.Priority).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the count of registered scripts.
        /// </summary>
        public int Count => _registeredScripts.Count;

        /// <summary>
        /// Renders all registered scripts as HTML script tags, ordered by priority.
        /// </summary>
        public IHtmlContent RenderScripts()
        {
            var orderedScripts = GetOrderedScripts();
            var writer = new StringWriter();

            foreach (var script in orderedScripts)
            {
                writer.Write($"<script src=\"{System.Text.Encodings.Web.HtmlEncoder.Default.Encode(script.ResourcePath)}\"></script>\n");
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
        /// Clears all registered scripts.
        /// </summary>
        public void Clear()
        {
            _registeredScripts.Clear();
            _registeredNames.Clear();
        }
    }
}
