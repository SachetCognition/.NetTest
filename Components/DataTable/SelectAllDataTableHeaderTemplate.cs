namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Text;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// The select all data table header template.
    /// </summary>
    public class SelectAllDataTableHeaderTemplate : IDataTableHeaderTemplate
    {
        /// <summary>
        /// Gets or sets the html helper.
        /// </summary>
        public IHtmlHelper HtmlHelper { get; set; }

        /// <summary>
        /// Gets or sets the client identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the css class.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// The build html.
        /// </summary>
        /// <param name="builder">
        /// The string builder.
        /// </param>
        public void BuildHtml(StringBuilder builder)
        {
            if (builder != null)
            {
                var tbAccSpan = new TagBuilder("label");
                tbAccSpan.MergeAttribute("for", this.Id);

                if (!string.IsNullOrEmpty(this.Title))
                {
                    tbAccSpan.InnerHtml.Append(this.Title);

                }
                else
                {
                     tbAccSpan.AddCssClass("hide-access");
                    tbAccSpan.InnerHtml.Append(this.ToolTip);
                }

                var tagBuilder = new TagBuilder("input");
                tagBuilder.MergeAttribute("type", "checkbox");
                tagBuilder.MergeAttribute("title", this.ToolTip);
                tagBuilder.MergeAttribute("name", this.Name);
                tagBuilder.MergeAttribute("id", this.Id);

                builder.Append(tagBuilder.ToString());

                builder.AppendLine(tbAccSpan.ToString());


            }
        }
    }
}
