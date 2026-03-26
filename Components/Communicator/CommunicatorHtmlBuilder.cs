namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorHtmlBuilder : HtmlBuilderBase<CommunicatorComponent>
    {
        public CommunicatorHtmlBuilder(CommunicatorComponent component) : base(component) { }
        public override void Build(HtmlTextWriter writer) { }
    }
}
