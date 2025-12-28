using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable;

public class SelectAllDataTableHeaderTemplate : IDataTableHeaderTemplate
{
    public IHtmlHelper? HtmlHelper { get; set; }
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ToolTip { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string CssClass { get; set; } = string.Empty;

    public void BuildHtml(StringBuilder builder)
    {
        if (builder != null)
        {
            var tbAccSpan = new TagBuilder("label");
            tbAccSpan.MergeAttribute("for", Id);

            if (!string.IsNullOrEmpty(Title))
            {
                tbAccSpan.InnerHtml.Append(Title);
            }
            else
            {
                tbAccSpan.AddCssClass("hide-access");
                tbAccSpan.InnerHtml.Append(ToolTip);
            }

            var tagBuilder = new TagBuilder("input");
            tagBuilder.MergeAttribute("type", "checkbox");
            tagBuilder.MergeAttribute("title", ToolTip);
            tagBuilder.MergeAttribute("name", Name);
            tagBuilder.MergeAttribute("id", Id);
            tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;

            builder.Append(RenderTagBuilder(tagBuilder));
            builder.AppendLine(RenderTagBuilder(tbAccSpan));
        }
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }
}
