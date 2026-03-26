namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Api
{
    using System.Collections.ObjectModel;
    using System.Web.UI;

    using Equant.SAV2000.ComponentLibrary.Common.Helper;

    public interface IComponent
    {
        string Id { get; set; }

        string Name { get; set; }

        bool IsVisible { get; set; }

        bool IsUpdatable { get; set; }

        ReadOnlyCollection<JsResource> JsResources { get; }

        ReadOnlyCollection<CssResource> CssResources { get; }

        void WriteHtml(HtmlTextWriter writer);

        void WriteInitScript(HtmlTextWriter writer);
    }
}
