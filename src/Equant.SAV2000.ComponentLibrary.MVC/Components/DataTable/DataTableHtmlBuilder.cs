namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DataTableHtmlBuilder : HtmlBuilderBase<DataTableComponent>
    {
        public DataTableHtmlBuilder(DataTableComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
        {
            if (writer != null && this.Component.IsVisible)
            {
                new DataTableErrorMessageTemplate(this.Component).Build(writer);

                var tagBuilderTable = new TagBuilder("table");
                tagBuilderTable.Attributes["id"] = this.Component.Id;

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderTable.Attributes["name"] = this.Component.Name;
                }

                if (!string.IsNullOrEmpty(this.Component.CssClass))
                {
                    tagBuilderTable.AddCssClass(this.Component.CssClass);
                }

                if (!this.Component.IsShowHeader)
                {
                    tagBuilderTable.AddCssClass("NoHeader");
                }
                else
                {
                    tagBuilderTable.AddCssClass("HeaderSpace");
                }

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderTable.MergeAttribute(attr.Key, attr.Value?.ToString());
                }

                // Generate caption
                var tagBuilderCaption = new TagBuilder("caption");
                tagBuilderCaption.AddCssClass("hide-access");
                tagBuilderCaption.InnerHtml.Append(this.Component.Caption);

                // Generate thead
                var theadSb = new StringBuilder();

                // Generate tr for headers
                var trSb = new StringBuilder();

                // Generate tr for column filter
                var trFilterSb = new StringBuilder();

                foreach (var column in this.Component.Columns)
                {
                    var tagBuilderTh = new TagBuilder("th");
                    tagBuilderTh.Attributes["scope"] = "col";

                    if (column.HeaderTemplate != null)
                    {
                        var thSb = new StringBuilder();
                        column.HeaderTemplate.HtmlHelper = this.Component.HtmlHelper;
                        column.HeaderTemplate.Title = column.HeaderText;
                        column.HeaderTemplate.BuildHtml(thSb);
                        tagBuilderTh.InnerHtml.AppendHtml(thSb.ToString());
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(column.CssClassHeader))
                        {
                            tagBuilderTh.AddCssClass(column.CssClassHeader);
                        }

                        if (column.ShowHeaderText)
                        {
                            tagBuilderTh.InnerHtml.Append(column.HeaderText);
                        }
                        else
                        {
                            tagBuilderTh.InnerHtml.AppendHtml(string.Format(CultureInfo.CurrentUICulture, "<span class=\"hide-access\">{0}</span>", column.HeaderText));
                        }
                    }

                    using (var sw = new StringWriter())
                    {
                        tagBuilderTh.WriteTo(sw, HtmlEncoder.Default);
                        trSb.Append(sw.ToString());
                    }

                    if (this.Component.IsFilter && this.Component.IsShowHeader)
                    {
                        var tagBuilderThFilter = new TagBuilder("td");
                        if (!string.IsNullOrEmpty(column.CssClass))
                        {
                            tagBuilderThFilter.AddCssClass(column.CssClass);
                        }
                        using (var sw = new StringWriter())
                        {
                            tagBuilderThFilter.WriteTo(sw, HtmlEncoder.Default);
                            trFilterSb.Append(sw.ToString());
                        }
                    }
                }

                // Build the tr
                var tagBuilderTr = new TagBuilder("tr");
                tagBuilderTr.InnerHtml.AppendHtml(trSb.ToString());

                // Build thead content
                using (var sw = new StringWriter())
                {
                    tagBuilderTr.WriteTo(sw, HtmlEncoder.Default);
                    theadSb.Append(sw.ToString());
                }

                if (this.Component.IsFilter && this.Component.IsShowHeader)
                {
                    var tagBuilderTrFilter = new TagBuilder("tr");
                    tagBuilderTrFilter.AddCssClass("filter-search");
                    tagBuilderTrFilter.InnerHtml.AppendHtml(trFilterSb.ToString());
                    using (var sw = new StringWriter())
                    {
                        tagBuilderTrFilter.WriteTo(sw, HtmlEncoder.Default);
                        theadSb.Append(sw.ToString());
                    }
                }

                var tagBuilderThead = new TagBuilder("thead");
                tagBuilderThead.InnerHtml.AppendHtml(theadSb.ToString());

                // Build table content
                var tableSb = new StringBuilder();
                using (var sw = new StringWriter())
                {
                    tagBuilderCaption.WriteTo(sw, HtmlEncoder.Default);
                    tableSb.Append(sw.ToString());
                }
                using (var sw = new StringWriter())
                {
                    tagBuilderThead.WriteTo(sw, HtmlEncoder.Default);
                    tableSb.Append(sw.ToString());
                }

                var tagBuilderTbody = new TagBuilder("tbody");
                using (var sw = new StringWriter())
                {
                    tagBuilderTbody.WriteTo(sw, HtmlEncoder.Default);
                    tableSb.Append(sw.ToString());
                }

                tagBuilderTable.InnerHtml.AppendHtml(tableSb.ToString());
                tagBuilderTable.WriteTo(writer, HtmlEncoder.Default);
            }
        }
    }
}
