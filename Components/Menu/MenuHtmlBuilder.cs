using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;

public class MenuHtmlBuilder : HtmlBuilderBase<MenuComponent>
{
    public MenuHtmlBuilder(MenuComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        var ul = new TagBuilder("ul");
        ul.Attributes["id"] = Component.Id;

        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            ul.AddCssClass(Component.CssClass);
        }

        foreach (var attr in Component.HtmlAttributes)
        {
            ul.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        foreach (var item in Component.Items)
        {
            var li = BuildMenuItem(item);
            ul.InnerHtml.AppendHtml(li);
        }

        using (var writer = new StringWriter())
        {
            ul.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }

    private TagBuilder BuildMenuItem(MenuItem item)
    {
        var li = new TagBuilder("li");

        if (!string.IsNullOrEmpty(item.Id))
        {
            li.Attributes["id"] = item.Id;
        }

        if (!string.IsNullOrEmpty(Component.CssClassItem))
        {
            li.AddCssClass(Component.CssClassItem);
        }

        if (item.IsSelected && !string.IsNullOrEmpty(Component.CssClassSelected))
        {
            li.AddCssClass(Component.CssClassSelected);
        }

        if (!string.IsNullOrEmpty(item.CssClass))
        {
            li.AddCssClass(item.CssClass);
        }

        var a = new TagBuilder("a");
        a.Attributes["href"] = item.Url ?? "#";

        if (!string.IsNullOrEmpty(item.Target))
        {
            a.Attributes["target"] = item.Target;
        }

        if (item.IsDisabled)
        {
            a.AddCssClass("disabled");
        }

        a.InnerHtml.AppendHtml(item.Text);
        li.InnerHtml.AppendHtml(a);

        if (item.Children.Count > 0)
        {
            var subUl = new TagBuilder("ul");
            foreach (var child in item.Children)
            {
                var childLi = BuildMenuItem(child);
                subUl.InnerHtml.AppendHtml(childLi);
            }
            li.InnerHtml.AppendHtml(subUl);
        }

        return li;
    }
}
