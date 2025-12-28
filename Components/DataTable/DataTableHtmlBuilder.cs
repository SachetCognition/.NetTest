using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

public class DataTableHtmlBuilder : HtmlBuilderBase<DataTableComponent>
{
    public DataTableHtmlBuilder(DataTableComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var result = new StringBuilder();
        
        var errorTemplate = new DataTableErrorMessageTemplate(Component).Build();
        result.Append(RenderHtmlContent(errorTemplate));

        var tagBuilderTable = new TagBuilder("table");
        var stringBuilderTable = new StringBuilder();
        tagBuilderTable.MergeAttribute("id", Component.Id);

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilderTable.MergeAttribute("name", Component.Name);
        }

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilderTable.AddCssClass(Component.CssClass);
        }

        if (!Component.IsShowHeader)
        {
            tagBuilderTable.AddCssClass("NoHeader");
        }
        else
        {
            tagBuilderTable.AddCssClass("HeaderSpace");
        }

        tagBuilderTable.MergeAttributes(Component.HtmlAttributes);

        var tagBuilderCaption = new TagBuilder("caption");
        tagBuilderCaption.InnerHtml.Append(Component.Caption);
        tagBuilderCaption.AddCssClass("hide-access");

        var tagBuilderThead = new TagBuilder("thead");
        var tagBuilderTr = new TagBuilder("tr");
        var stringBuilderTr = new StringBuilder();

        var tagBuilderTrFilter = new TagBuilder("tr");
        tagBuilderTrFilter.AddCssClass("filter-search");
        var stringBuilderTrFilter = new StringBuilder();
        
        foreach (var column in Component.Columns)
        {
            var tagBuilderTh = new TagBuilder("th");
            tagBuilderTh.MergeAttribute("scope", "col");
            
            if (column.HeaderTemplate != null)
            {
                var stringBuilderTh = new StringBuilder();
                column.HeaderTemplate.HtmlHelper = Component.HtmlHelper;
                column.HeaderTemplate.Title = column.HeaderText;
                column.HeaderTemplate.BuildHtml(stringBuilderTh);
                tagBuilderTh.InnerHtml.AppendHtml(stringBuilderTh.ToString());
                stringBuilderTr.Append(RenderTagBuilder(tagBuilderTh));
            }
            else
            {
                tagBuilderTh.AddCssClass(column.CssClassHeader);

                if (column.ShowHeaderText)
                {
                    tagBuilderTh.InnerHtml.Append(column.HeaderText);
                }
                else
                {
                    tagBuilderTh.InnerHtml.AppendHtml(string.Format(CultureInfo.CurrentUICulture, "<span class=\"hide-access\">{0}</span>", column.HeaderText));
                }

                stringBuilderTr.Append(RenderTagBuilder(tagBuilderTh));
            }

            if (Component.IsFilter && Component.IsShowHeader)
            {
                var tagBuilderThFilter = new TagBuilder("td");
                tagBuilderThFilter.AddCssClass(column.CssClass);
                stringBuilderTrFilter.Append(RenderTagBuilder(tagBuilderThFilter));
            }
        }
        
        tagBuilderTr.InnerHtml.AppendHtml(stringBuilderTr.ToString());
        tagBuilderThead.InnerHtml.AppendHtml(RenderTagBuilder(tagBuilderTr));

        if (Component.IsFilter && Component.IsShowHeader)
        {
            tagBuilderTrFilter.InnerHtml.AppendHtml(stringBuilderTrFilter.ToString());
            tagBuilderThead.InnerHtml.AppendHtml(RenderTagBuilder(tagBuilderTrFilter));
        }

        stringBuilderTable.Append(RenderTagBuilder(tagBuilderCaption));
        stringBuilderTable.Append(RenderTagBuilder(tagBuilderThead));

        var tagBuilderTbody = new TagBuilder("tbody");
        stringBuilderTable.Append(RenderTagBuilder(tagBuilderTbody));

        tagBuilderTable.InnerHtml.AppendHtml(stringBuilderTable.ToString());
        result.Append(RenderTagBuilder(tagBuilderTable));

        return new HtmlString(result.ToString());
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private static string RenderHtmlContent(IHtmlContent content)
    {
        using var writer = new StringWriter();
        content.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
