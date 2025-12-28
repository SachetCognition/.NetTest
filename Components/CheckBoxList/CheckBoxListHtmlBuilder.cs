using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBoxList;

public class CheckBoxListHtmlBuilder : HtmlBuilderBase<CheckBoxListComponent>
{
    public CheckBoxListHtmlBuilder(CheckBoxListComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        if (!Component.IsVisible)
        {
            return HtmlString.Empty;
        }

        using var writer = new StringWriter();

        if (!string.IsNullOrEmpty(Component.CheckBoxListLabel.Text))
        {
            Component.CheckBoxListLabel.ToHtml().WriteTo(writer, HtmlEncoder.Default);
        }

        var tagBuilderFieldSet = new TagBuilder("fieldset");
        if (!string.IsNullOrEmpty(Component.CssClassFieldSet))
        {
            tagBuilderFieldSet.AddCssClass(Component.CssClassFieldSet);
        }

        var tagBuilderUl = new TagBuilder("ul");
        if (!string.IsNullOrEmpty(Component.CssClass))
        {
            tagBuilderUl.AddCssClass("checkboxpadding " + Component.CssClass);
        }

        tagBuilderUl.Attributes["id"] = Component.Id;
        foreach (var attr in Component.HtmlAttributes)
        {
            tagBuilderUl.Attributes[attr.Key] = attr.Value?.ToString() ?? string.Empty;
        }

        if (Component.IsDisabled)
        {
            Component.CssClass = Component.CssClassDisabled;
            tagBuilderFieldSet.Attributes["disabled"] = "disabled";
        }

        foreach (var item in Component.SourceItems)
        {
            var tagBuilderLi = new TagBuilder("li");
            if (!string.IsNullOrEmpty(item.Text))
            {
                GenerateCheckBox(item, tagBuilderLi);
            }
            tagBuilderUl.InnerHtml.AppendHtml(tagBuilderLi);
        }

        if (Component.SourceItems.Count == 0)
        {
            tagBuilderUl.InnerHtml.AppendHtml(new TagBuilder("li"));
        }

        if (!string.IsNullOrEmpty(Component.Title))
        {
            tagBuilderUl.Attributes["title"] = Component.Title;
        }

        var legend = new TagBuilder("legend");
        legend.AddCssClass("hide-access");
        var hiddenSpan = new TagBuilder("span");
        hiddenSpan.InnerHtml.AppendHtml(Component.Title ?? string.Empty);
        legend.InnerHtml.AppendHtml(hiddenSpan);

        var div = new TagBuilder("div");
        var divCssClass = string.IsNullOrEmpty(Component.CssClassCheckBoxDiv) 
            ? "checkbox-list-scroll" 
            : "checkbox-list-scroll " + Component.CssClassCheckBoxDiv;
        div.AddCssClass(divCssClass);
        div.InnerHtml.AppendHtml(tagBuilderUl);

        tagBuilderFieldSet.InnerHtml.AppendHtml(legend);
        tagBuilderFieldSet.InnerHtml.AppendHtml(div);

        if (Component.IsOuterDivNeeded)
        {
            var tagBuilderOuterDiv = new TagBuilder("div");
            if (!string.IsNullOrEmpty(Component.CssClassOuterDiv))
            {
                tagBuilderOuterDiv.AddCssClass(Component.CssClassOuterDiv);
            }
            tagBuilderOuterDiv.InnerHtml.AppendHtml(tagBuilderFieldSet);
            tagBuilderOuterDiv.WriteTo(writer, HtmlEncoder.Default);
        }
        else
        {
            tagBuilderFieldSet.WriteTo(writer, HtmlEncoder.Default);
        }

        return new HtmlString(writer.ToString());
    }

    private void GenerateCheckBox(CheckBoxListItem item, TagBuilder tagBuilderLi)
    {
        var compId = Component.Id + item.Value;
        var tagBuilderCheckBox = new TagBuilder("input");
        tagBuilderCheckBox.Attributes["id"] = compId;
        tagBuilderCheckBox.Attributes["type"] = "checkbox";
        tagBuilderCheckBox.Attributes["value"] = item.Value ?? string.Empty;

        if (!string.IsNullOrEmpty(Component.Name))
        {
            tagBuilderCheckBox.Attributes["name"] = Component.Name;
        }

        if (item.Disabled)
        {
            Component.CssClass = Component.CssClassDisabled;
            tagBuilderCheckBox.Attributes["disabled"] = "disabled";
        }

        if (item.Selected)
        {
            tagBuilderCheckBox.Attributes["checked"] = "checked";
        }

        tagBuilderCheckBox.TagRenderMode = TagRenderMode.SelfClosing;

        var tagBuilderLabel = new TagBuilder("label");
        tagBuilderLabel.Attributes["for"] = compId;
        tagBuilderLabel.InnerHtml.AppendHtml(item.Text ?? string.Empty);

        tagBuilderLi.InnerHtml.AppendHtml(tagBuilderCheckBox);
        tagBuilderLi.InnerHtml.AppendHtml(tagBuilderLabel);
    }
}
