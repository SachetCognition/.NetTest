namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.IO;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DataTableErrorMessageTemplate : HtmlBuilderBase<DataTableComponent>
    {
        private const string htmlTemplate =
            "<div id='{0}_ErrorId' class='form alert error' style='display:none'>\n" +
            "   <span id='{0}_ErrorIdInfoIcon'>\n" +
            "       <a title='{1}' href='###' tooltipid='{0}_InfoIconToolTip' class=''><span role='alert' id='{0}_Message'>{2}</span></a>\n" +
            "       <span style='display:none' role='alert' id='{0}_InfoIconToolTip' class='tooltip'>{3}</span>\n" +
            "   </span>\n" +
            "</div>\n";

        public DataTableErrorMessageTemplate(DataTableComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
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
