namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IDataTableHeaderTemplate
    {
        IHtmlHelper HtmlHelper { get; set; }
        string Title { get; set; }
        void BuildHtml(StringBuilder builder);
    }
}
