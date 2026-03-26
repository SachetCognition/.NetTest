namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Globalization;
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Html;

    /// <summary>
    /// The data table html builder.
    /// </summary>
    public class DataTableHtmlBuilder : HtmlBuilderBase<DataTableComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public DataTableHtmlBuilder(DataTableComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(TextWriter writer)
        {
            if (writer !=null && this.Component.IsVisible)
            {
                new DataTableErrorMessageTemplate(this.Component).Build(writer);

                // Generate table HTML tag
                var tagBuilderTable = new TagBuilder("table");
                var stringBuilderTable = new StringBuilder();
                tagBuilderTable.MergeAttribute("id", this.Component.Id);

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderTable.MergeAttribute("name", this.Component.Name);
                }

                if (!string.IsNullOrEmpty(this.Component.CssClass))
                {
                    tagBuilderTable.AddCssClass(this.Component.CssClass);
                }

                if (!this.Component.IsShowHeader)
                {
                    tagBuilderTable.AddCssClass(" NoHeader");
                }
                else
                {
                    tagBuilderTable.AddCssClass(" HeaderSpace");
                }

                tagBuilderTable.MergeAttributes(this.Component.HtmlAttributes);

                // Generate caption
                var tagBuilderCaption = new TagBuilder("caption");
                tagBuilderCaption.InnerHtml.SetHtmlContent(this.Component.Caption);
                tagBuilderCaption.AddCssClass("hide-access");

                // Generate thead HTML tag
                var tagBuilderThead = new TagBuilder("thead");
                
                // Generate tr HTML tag
                var tagBuilderTr = new TagBuilder("tr");
                var stringBuilderTr = new StringBuilder();

                // Generate tr HTML tag for column filter
                var tagBuilderTrFilter = new TagBuilder("tr");
                tagBuilderTrFilter.AddCssClass("filter-search");
                var stringBuilderTrFilter = new StringBuilder();
                
                // Generate the column headers
                foreach (var column in this.Component.Columns)
                {
                    var tagBuilderTh = new TagBuilder("th");
                    //<Change author="Nidhi" version="Iteration1" action = "Modification">
                    //Description : Each header must have its related scope.
                    //</Change>
                    tagBuilderTh.MergeAttribute("scope", "col");
                    if (column.HeaderTemplate != null)
                    {
                        var stringBuilderTh = new StringBuilder();
                        column.HeaderTemplate.IHtmlHelper = this.Component.HtmlHelper;
                        column.HeaderTemplate.Title = column.HeaderText;
                        column.HeaderTemplate.BuildHtml(stringBuilderTh);
                        tagBuilderTh.InnerHtml.SetHtmlContent(stringBuilderTh.ToString());
                        stringBuilderTr.Append(tagBuilderTh);
                    }
                    else
                    {
                        tagBuilderTh.AddCssClass(column.CssClassHeader);

                        if (column.ShowHeaderText)
                        {
                            tagBuilderTh.InnerHtml.SetHtmlContent(column.HeaderText);
                        }
                        else
                        {
                            tagBuilderTh.InnerHtml.SetHtmlContent(string.Format(CultureInfo.CurrentUICulture,"<span class=\"hide-access\">{0}</span>",column.HeaderText ));
                        }

                        stringBuilderTr.Append(tagBuilderTh);
                    }

                    // Add filter th tag
                    if (this.Component.IsFilter && this.Component.IsShowHeader)
                    {
                        var tagBuilderThFilter = new TagBuilder("td");
                        tagBuilderThFilter.AddCssClass(column.CssClass);
                        stringBuilderTrFilter.Append(tagBuilderThFilter);
                    }
                }
                
                tagBuilderTr.InnerHtml.SetHtmlContent(stringBuilderTr.ToString());
                tagBuilderThead.InnerHtml.SetHtmlContent(tagBuilderTr.ToString());

                if (this.Component.IsFilter && this.Component.IsShowHeader)
                {
                    tagBuilderTrFilter.InnerHtml.SetHtmlContent(stringBuilderTrFilter.ToString());
                    tagBuilderThead.InnerHtml.SetHtmlContent(string.Format(CultureInfo.InvariantCulture, "{0}{1}", tagBuilderThead.InnerHtml, tagBuilderTrFilter));
                }

                stringBuilderTable.Append(tagBuilderCaption);
                stringBuilderTable.Append(tagBuilderThead);

                var tagBuilderTbody = new TagBuilder("tbody");
                stringBuilderTable.Append(tagBuilderTbody);

                tagBuilderTable.InnerHtml.SetHtmlContent(stringBuilderTable.ToString());

                writer.Write(tagBuilderTable);
            }
        }
    }
}
