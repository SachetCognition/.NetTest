namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System;

    /// <summary>
    /// Represents a JavaScript resource dependency for a component.
    /// Replaces ClientDependency with a simple resource registration pattern.
    /// </summary>
    public class JsResource
    {
        public JsResource(string name, string resourcePath, int priority, Type componentType)
        {
            this.Name = name;
            this.ResourcePath = resourcePath;
            this.Priority = priority;
            this.ComponentType = componentType;
        }

        public string Name { get; private set; }
        public string ResourcePath { get; private set; }
        public int Priority { get; private set; }
        public Type ComponentType { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj is JsResource other)
            {
                return string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.Name != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(this.Name) : 0;
        }
    }
}
