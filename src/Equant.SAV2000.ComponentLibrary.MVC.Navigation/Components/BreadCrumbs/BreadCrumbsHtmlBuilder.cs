namespace Equant.SAV2000.ComponentLibrary.MVC.Components.BreadCrumbs
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class BreadCrumbsHtmlBuilder : HtmlBuilderBase<BreadCrumbsComponent>
    {
        public BreadCrumbsHtmlBuilder(BreadCrumbsComponent component) : base(component) { }

        public override void Build(TextWriter writer)
        {
            var nav = new TagBuilder("nav");
            nav.Attributes["id"] = this.Component.Id ?? string.Empty;
            nav.Attributes["aria-label"] = "breadcrumb";
            if (!string.IsNullOrEmpty(this.Component.CssClass))
            {
                nav.AddCssClass(this.Component.CssClass);
            }

            var ol = new TagBuilder("ol");
            ol.AddCssClass("breadcrumb");

            var innerHtml = new StringBuilder();

            for (int i = 0; i < this.Component.Items.Count; i++)
            {
                var item = this.Component.Items[i];
                bool isLast = (i == this.Component.Items.Count - 1);

                var li = new TagBuilder("li");
                li.AddCssClass("breadcrumb-item");

                if (isLast || item.IsCurrentPage)
                {
                    li.AddCssClass("active");
                    li.Attributes["aria-current"] = "page";
                    li.InnerHtml.Append(item.Text ?? string.Empty);
                }
                else
                {
                    var anchor = new TagBuilder("a");
                    anchor.Attributes["href"] = item.Url ?? "#";
                    anchor.InnerHtml.Append(item.Text ?? string.Empty);

                    using (var sw = new StringWriter())
                    {
                        anchor.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                        li.InnerHtml.AppendHtml(sw.ToString());
                    }
                }

                using (var sw = new StringWriter())
                {
                    li.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    innerHtml.Append(sw.ToString());
                }
            }

            ol.InnerHtml.AppendHtml(innerHtml.ToString());

            using (var sw = new StringWriter())
            {
                ol.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                nav.InnerHtml.AppendHtml(sw.ToString());
            }

            nav.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        }
    }
}
