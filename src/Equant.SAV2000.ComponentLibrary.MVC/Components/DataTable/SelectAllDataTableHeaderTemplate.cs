namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SelectAllDataTableHeaderTemplate : IDataTableHeaderTemplate
    {
        public IHtmlHelper HtmlHelper { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string ToolTip { get; set; }
        public string Name { get; set; }
        public string CssClass { get; set; }

        public void BuildHtml(StringBuilder builder)
        {
            if (builder != null)
            {
                var tbAccSpan = new TagBuilder("label");
                tbAccSpan.Attributes["for"] = this.Id;

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
                tagBuilder.Attributes["type"] = "checkbox";
                tagBuilder.Attributes["title"] = this.ToolTip;
                tagBuilder.Attributes["name"] = this.Name;
                tagBuilder.Attributes["id"] = this.Id;
                tagBuilder.TagRenderMode = TagRenderMode.SelfClosing;

                using (var sw = new System.IO.StringWriter())
                {
                    tagBuilder.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    builder.Append(sw.ToString());
                }

                using (var sw = new System.IO.StringWriter())
                {
                    tbAccSpan.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    builder.AppendLine(sw.ToString());
                }
            }
        }
    }
}
