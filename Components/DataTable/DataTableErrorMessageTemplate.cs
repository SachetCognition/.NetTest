namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// Am error message block for DataTable
    /// </summary>
    public class DataTableErrorMessageTemplate : HtmlBuilderBase<DataTableComponent>
    {
        private const string htmlTemplate =
            "<div id='{0}_ErrorId' class='form alert error' style='display:none'>\n" +
            "   <span id='{0}_ErrorIdInfoIcon'>\n"+
            "       <a title='{1}' href='###' tooltipid='{0}_InfoIconToolTip' class=''><span role='alert' id='{0}_Message'>{2}</span></a>\n" +
            "       <span style='display:none' role='alert' id='{0}_InfoIconToolTip' class='tooltip'>{3}</span>\n" +
            "   </span>\n" +
            "</div>\n";

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableErrorMessageTemplate"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public DataTableErrorMessageTemplate(DataTableComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build of the component
        /// </summary>
        /// <param name="writer">
        /// The HTML writer.
        /// </param>
        public override void Build(HtmlTextWriter writer)
        {
            if (writer != null && this.Component.IsVisible)
            {
                writer.Write(
                    htmlTemplate,
                    this.Component.Id,
                    this.Component.RecordLimitMessageTitle,
                    this.Component.RecordsLimitMessage,
                    this.Component.RecordsLimitMessageLong
                );
            }
        }
    }
}
