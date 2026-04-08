namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    using System;

    public class CssResource
    {
        public CssResource(string name, string resourcePath, int priority, Type componentType)
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
    }
}
