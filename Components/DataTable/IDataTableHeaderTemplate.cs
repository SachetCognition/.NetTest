namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// The DataTableHeaderComponent interface.
    /// </summary>
    public interface IDataTableHeaderTemplate
    {
        /// <summary>
        /// Gets or sets the html helper.
        /// </summary>
        HtmlHelper HtmlHelper { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the build.
        /// </summary>
        void BuildHtml(StringBuilder builder);
    }
}
