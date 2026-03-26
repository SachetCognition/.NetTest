namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;

    public class CommunicatorHtmlBuilder : HtmlBuilderBase<CommunicatorComponent>
    {
        public CommunicatorHtmlBuilder(CommunicatorComponent component) : base(component) { }
        public override void Build(TextWriter writer) { }
    }
}
