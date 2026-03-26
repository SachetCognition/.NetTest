namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.IO;

    public abstract class HtmlBuilderBase<TComponent> where TComponent : ComponentBase
    {
        protected TComponent Component { get; set; }
        protected HtmlBuilderBase() { }
        protected HtmlBuilderBase(TComponent component) { this.Component = component; }
        public virtual void Build(TextWriter writer) { }
    }
}
