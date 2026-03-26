namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    using System;

    public class JsResource
    {
        public JsResource() { }

        public JsResource(string name, string resourcePath, int priority, Type ownerType)
        {
            this.Name = name;
            this.ResourcePath = resourcePath;
            this.Priority = priority;
            this.OwnerType = ownerType;
        }

        public string Name { get; set; }
        public string ResourcePath { get; set; }
        public int Priority { get; set; }
        public Type OwnerType { get; set; }
    }

    public class CssResource
    {
        public CssResource() { }

        public CssResource(string name, string resourcePath, int priority, Type ownerType)
        {
            this.Name = name;
            this.ResourcePath = resourcePath;
            this.Priority = priority;
            this.OwnerType = ownerType;
        }

        public string Name { get; set; }
        public string ResourcePath { get; set; }
        public int Priority { get; set; }
        public Type OwnerType { get; set; }
    }
}
