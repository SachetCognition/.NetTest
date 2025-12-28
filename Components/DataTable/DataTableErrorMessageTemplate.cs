using Microsoft.AspNetCore.Html;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

public class DataTableErrorMessageTemplate : HtmlBuilderBase<DataTableComponent>
{
    private const string HtmlTemplate =
        "<div id='{0}_ErrorId' class='form alert error' style='display:none'>\n" +
        "   <span id='{0}_ErrorIdInfoIcon'>\n"+
        "       <a title='{1}' href='###' tooltipid='{0}_InfoIconToolTip' class=''><span role='alert' id='{0}_Message'>{2}</span></a>\n" +
        "       <span style='display:none' role='alert' id='{0}_InfoIconToolTip' class='tooltip'>{3}</span>\n" +
        "   </span>\n" +
        "</div>\n";

    public DataTableErrorMessageTemplate(DataTableComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (Component.IsVisible)
        {
            var html = string.Format(
                HtmlTemplate,
                Component.Id,
                Component.RecordLimitMessageTitle,
                Component.RecordsLimitMessage,
                Component.RecordsLimitMessageLong
            );
            return new HtmlString(html);
        }
        return HtmlString.Empty;
    }
}
