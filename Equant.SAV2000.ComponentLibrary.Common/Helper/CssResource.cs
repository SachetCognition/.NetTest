namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    using System;

    public class CssResource
    {
        public CssResource(string name, string resourceName, int priority, Type type)
        {
            this.Name = name;
            this.ResourceName = resourceName;
            this.Priority = priority;
            this.Type = type;
        }

        public string Name { get; set; }

        public string ResourceName { get; set; }

        public int Priority { get; set; }

        public Type Type { get; set; }
    }
}
