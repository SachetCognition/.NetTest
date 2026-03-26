namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Communicator
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class CommunicatorComponent : ComponentBase
    {
        public CommunicatorComponent() { }
        public CommunicatorComponent(IHtmlHelper htmlHelper) : base(htmlHelper) { }

        public string Text { get; set; }
        public string CssClass { get; set; }
        public string Channel { get; set; }

        public override IHtmlContent BuildHtml()
        {
            return new CommunicatorHtmlBuilder(this).Build();
        }

        public override string BuildInitScript()
        {
            if (string.IsNullOrEmpty(this.Id))
                return string.Empty;
            return string.Format("$('#{0}').communicator();", this.Id);
        }
    }
}
