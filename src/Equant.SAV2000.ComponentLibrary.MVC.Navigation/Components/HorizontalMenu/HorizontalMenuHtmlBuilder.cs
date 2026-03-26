namespace Equant.SAV2000.ComponentLibrary.MVC.Components.HorizontalMenu
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class HorizontalMenuHtmlBuilder : HtmlBuilderBase<HorizontalMenuComponent>
    {
        public HorizontalMenuHtmlBuilder(HorizontalMenuComponent component) : base(component) { }

        public override void Build(TextWriter writer)
        {
            var nav = new TagBuilder("nav");
            nav.Attributes["id"] = this.Component.Id ?? string.Empty;
            if (!string.IsNullOrEmpty(this.Component.CssClass))
            {
                nav.AddCssClass(this.Component.CssClass);
            }
            nav.AddCssClass("horizontal-menu");

            var ul = new TagBuilder("ul");
            ul.AddCssClass("nav nav-horizontal");

            var innerHtml = new StringBuilder();

            foreach (var item in this.Component.MenuItems)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("nav-item");

                bool isActive = item.IsActive ||
                    (!string.IsNullOrEmpty(this.Component.ActiveItem) &&
                     this.Component.ActiveItem == item.Text);

                if (isActive)
                {
                    li.AddCssClass("active");
                }

                var anchor = new TagBuilder("a");
                anchor.AddCssClass("nav-link");
                anchor.Attributes["href"] = item.Url ?? "#";

                if (!string.IsNullOrEmpty(item.CssClass))
                {
                    anchor.AddCssClass(item.CssClass);
                }

                if (!string.IsNullOrEmpty(item.OnClick))
                {
                    anchor.Attributes["onclick"] = item.OnClick;
                }

                anchor.InnerHtml.Append(item.Text ?? string.Empty);

                using (var sw = new StringWriter())
                {
                    anchor.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    li.InnerHtml.AppendHtml(sw.ToString());
                }

                using (var sw = new StringWriter())
                {
                    li.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                    innerHtml.Append(sw.ToString());
                }
            }

            ul.InnerHtml.AppendHtml(innerHtml.ToString());

            using (var sw = new StringWriter())
            {
                ul.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                nav.InnerHtml.AppendHtml(sw.ToString());
            }

            nav.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        }
    }
}
