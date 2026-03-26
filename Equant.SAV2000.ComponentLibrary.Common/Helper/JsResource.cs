namespace Equant.SAV2000.ComponentLibrary.Common.Helper
{
    public class JsResource
    {
        public JsResource(string name, string resourcePath, int priority, System.Type assemblyType)
        {
            this.Name = name;
            this.ResourcePath = resourcePath;
            this.Priority = priority;
            this.AssemblyType = assemblyType;
        }

        public string Name { get; set; }
        public string ResourcePath { get; set; }
        public int Priority { get; set; }
        public System.Type AssemblyType { get; set; }
    }
}
