namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DropdownMenus
{
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class DropDownMenuHtmlBuilder : HtmlBuilderBase<DropDownMenuComponent>
    {
        public DropDownMenuHtmlBuilder(DropDownMenuComponent component) : base(component) { }

        public override void Build(TextWriter writer)
        {
            var nav = new TagBuilder("nav");
            nav.Attributes["id"] = this.Component.Id ?? string.Empty;
            if (!string.IsNullOrEmpty(this.Component.CssClass))
            {
                nav.AddCssClass(this.Component.CssClass);
            }
            nav.AddCssClass("dropdown-menu-container");

            var ul = new TagBuilder("ul");
            ul.AddCssClass("nav dropdown-nav");

            var innerHtml = new StringBuilder();

            foreach (var menuItem in this.Component.MenuItems)
            {
                var li = CreateMenuItemLi(menuItem);
                innerHtml.Append(TagBuilderToString(li));
            }

            ul.InnerHtml.AppendHtml(innerHtml.ToString());

            using (var sw = new StringWriter())
            {
                ul.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                nav.InnerHtml.AppendHtml(sw.ToString());
            }

            nav.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        }

        private TagBuilder CreateMenuItemLi(DropdownMenu menuItem)
        {
            var li = new TagBuilder("li");
            li.AddCssClass("nav-item");

            bool hasChildren = menuItem.Children != null && menuItem.Children.Count > 0;
            if (hasChildren)
            {
                li.AddCssClass("dropdown");
            }

            var anchor = new TagBuilder("a");
            anchor.AddCssClass("nav-link");
            anchor.Attributes["href"] = menuItem.Url ?? "#";

            if (!string.IsNullOrEmpty(menuItem.CssClass))
            {
                anchor.AddCssClass(menuItem.CssClass);
            }

            if (hasChildren)
            {
                anchor.AddCssClass("dropdown-toggle");
                anchor.Attributes["data-toggle"] = "dropdown";
            }

            anchor.InnerHtml.Append(menuItem.Text ?? string.Empty);

            li.InnerHtml.AppendHtml(TagBuilderToString(anchor));

            if (hasChildren)
            {
                var childUl = new TagBuilder("ul");
                childUl.AddCssClass("dropdown-menu");

                var childHtml = new StringBuilder();
                foreach (var child in menuItem.Children)
                {
                    var childLi = CreateChildMenuLi(child);
                    childHtml.Append(TagBuilderToString(childLi));
                }

                childUl.InnerHtml.AppendHtml(childHtml.ToString());
                li.InnerHtml.AppendHtml(TagBuilderToString(childUl));
            }

            return li;
        }

        private TagBuilder CreateChildMenuLi(ChildMenu childMenu)
        {
            var li = new TagBuilder("li");

            bool hasChildren = childMenu.Children != null && childMenu.Children.Count > 0;
            if (hasChildren)
            {
                li.AddCssClass("dropdown-submenu");
            }

            var anchor = new TagBuilder("a");
            anchor.AddCssClass("dropdown-item");
            anchor.Attributes["href"] = childMenu.Url ?? "#";

            if (!string.IsNullOrEmpty(childMenu.CssClass))
            {
                anchor.AddCssClass(childMenu.CssClass);
            }

            if (!string.IsNullOrEmpty(childMenu.OnClick))
            {
                anchor.Attributes["onclick"] = childMenu.OnClick;
            }

            anchor.InnerHtml.Append(childMenu.Text ?? string.Empty);

            li.InnerHtml.AppendHtml(TagBuilderToString(anchor));

            if (hasChildren)
            {
                var childUl = new TagBuilder("ul");
                childUl.AddCssClass("dropdown-menu");

                var childHtml = new StringBuilder();
                foreach (var child in childMenu.Children)
                {
                    var childLi = CreateChildMenuLi(child);
                    childHtml.Append(TagBuilderToString(childLi));
                }

                childUl.InnerHtml.AppendHtml(childHtml.ToString());
                li.InnerHtml.AppendHtml(TagBuilderToString(childUl));
            }

            return li;
        }

        private static string TagBuilderToString(TagBuilder tag)
        {
            using (var sw = new StringWriter())
            {
                tag.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                return sw.ToString();
            }
        }
    }
}
