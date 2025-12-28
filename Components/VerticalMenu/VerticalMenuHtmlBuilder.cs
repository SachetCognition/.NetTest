using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Equant.SAV2000.ComponentLibrary.Common.Resources;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;
using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu;

public class VerticalMenuHtmlBuilder : HtmlBuilderBase<VerticalMenuComponent>
{
    private bool _isMenuSelected;

    public VerticalMenuHtmlBuilder(VerticalMenuComponent component)
    {
        Component = component;
    }

    public override IHtmlContent Build()
    {
        var tagBuilderOuterDiv = new TagBuilder("div");
        tagBuilderOuterDiv.AddCssClass(VerticalMenuComponent.OuterMenuDivCssClass);
        tagBuilderOuterDiv.MergeAttribute("id", Component.Id);

        var tagBuilderSubOuterDiv = new TagBuilder("div");
        tagBuilderSubOuterDiv.AddCssClass("innernavbar");

        var tagBuilderMenuDiv = new TagBuilder("div");
        tagBuilderMenuDiv.AddCssClass("in");

        tagBuilderMenuDiv.InnerHtml.SetHtmlContent(CreateHeaderMenuTemplate());

        var copyRightDiv = CreateCopyRightDiv();

        var subOuterContent = new StringBuilder();
        subOuterContent.Append(RenderTagBuilder(tagBuilderMenuDiv));
        subOuterContent.Append(copyRightDiv);
        subOuterContent.Append(string.Format(CultureInfo.CurrentCulture, "<input type = 'hidden' name='{0}'>", Component.Name));
        tagBuilderSubOuterDiv.InnerHtml.SetHtmlContent(subOuterContent.ToString());

        tagBuilderOuterDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(tagBuilderSubOuterDiv));

        return new HtmlString(RenderTagBuilder(tagBuilderOuterDiv));
    }

    private static string RenderTagBuilder(TagBuilder tagBuilder)
    {
        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, HtmlEncoder.Default);
        return writer.ToString();
    }

    private string CreateCopyRightDiv()
    {
        var copyRightDiv = new TagBuilder("div");
        copyRightDiv.AddCssClass("copyright");
        var innerCopyDIv = new TagBuilder("div");
        innerCopyDIv.AddCssClass("innercopy");

        var copyRightTextDiv = new TagBuilder("div");
        var copyRightPara = new TagBuilder("p");
        copyRightPara.InnerHtml.SetContent(Component.CopyRightText ?? string.Empty);
        copyRightTextDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(copyRightPara));

        var copyRightImgAndText = new StringBuilder(string.Empty);
        copyRightImgAndText.Append(Component.CopyRightImage.ToHtmlString());

        copyRightImgAndText.Append(RenderTagBuilder(copyRightTextDiv));
        innerCopyDIv.InnerHtml.SetHtmlContent(copyRightImgAndText.ToString());
        copyRightDiv.InnerHtml.SetHtmlContent(RenderTagBuilder(innerCopyDIv));

        return RenderTagBuilder(copyRightDiv);
    }

    private string CreateHeaderMenuTemplate()
    {
        var headerTemplateUl = new TagBuilder("ul");
        var idValue = new StringBuilder(string.Empty).Append(Component.Id).Append("_menu");
        headerTemplateUl.MergeAttribute("id", idValue.ToString());
        headerTemplateUl.AddCssClass("nav nav-vertical");

        headerTemplateUl.InnerHtml.SetHtmlContent(CreateMenuItems());
        return RenderTagBuilder(headerTemplateUl);
    }

    private string CreateMenuItems()
    {
        var menuBuilder = new StringBuilder();
        int itemIndex = 1;

        foreach (MenuItem item in Component.MenuItems)
        {
            _isMenuSelected = false;
            if (item.Hidden)
            {
                continue;
            }

            var liBuilder = new TagBuilder("li");

            var anchorBuilder1Html = CreateInnerMenuHtml(item, itemIndex.ToString(CultureInfo.InvariantCulture), 1, "dropdown-toggle");

            var childMenuTagsUl = ChildMenuTagsUl(item, itemIndex);
            if (_isMenuSelected)
            {
                liBuilder.AddCssClass("active");
            }

            var liContent = anchorBuilder1Html + (childMenuTagsUl == null ? string.Empty : RenderTagBuilder(childMenuTagsUl));
            liBuilder.InnerHtml.SetHtmlContent(liContent);

            if (Array.IndexOf(liContent.Split('"'), Component.SelectedMenu) > -1)
            {
                liBuilder.AddCssClass("first active");
                _isMenuSelected = true;
            }
            else
            {
                liBuilder.AddCssClass("first");
            }

            itemIndex++;
            menuBuilder.Append(RenderTagBuilder(liBuilder));
        }

        return menuBuilder.ToString();
    }

    private TagBuilder? ChildMenuTagsUl(MenuItem item, int itemIndex)
    {
        TagBuilder? childMenuTagsUl = null;
        var childMenus = item.ReturnChildMenu();
        if (childMenus != null && childMenus.Count > 0)
        {
            childMenuTagsUl = new TagBuilder("ul");

            childMenuTagsUl.MergeAttribute("style", "display: none");
            childMenuTagsUl.AddCssClass("dropdown-menu dropDownMenuAdjustment");

            var childMenuItems = new StringBuilder();
            int index = 1;

            foreach (var childItem in childMenus)
            {
                var indexLevel = new StringBuilder(string.Empty);
                if (childItem.Hidden)
                {
                    continue;
                }

                indexLevel.Append(itemIndex).Append(".").Append(index);
                var childLiTag = ChildLiTag(childItem, indexLevel.ToString(), 2);

                if (_isMenuSelected)
                {
                    childMenuTagsUl.Attributes["style"] = "display:block";
                }

                childMenuItems.Append(RenderTagBuilder(childLiTag));
                index++;
            }

            childMenuTagsUl.InnerHtml.SetHtmlContent(childMenuItems.ToString());
        }

        return childMenuTagsUl;
    }

    private TagBuilder ChildLiTag(MenuItem childItem, string indexLevel, int menuLevel)
    {
        var childLiTag = new TagBuilder("li");
        string? selectedMenuName = Component.SelectedMenu;

        var menuCtrlHtml = CreateInnerMenuHtml(childItem, indexLevel, menuLevel, "first");
        childLiTag.InnerHtml.SetHtmlContent(menuCtrlHtml);

        if (Array.IndexOf(menuCtrlHtml.Split('"'), selectedMenuName) > -1)
        {
            childLiTag.AddCssClass("first active");
            _isMenuSelected = true;
        }
        else
        {
            childLiTag.AddCssClass("first");
        }

        return childLiTag;
    }

    private string CreateInnerMenuHtml(MenuItem childItem, string indexLevel, int menuLevel, string cssclass)
    {
        string? accessText = null;
        if (menuLevel == 1 && childItem.ReturnChildMenu() != null && childItem.ReturnChildMenu()!.Count > 0)
        {
            accessText = ApplicationStrings.LBL000011;
        }

        var id = GetMenuItemId(childItem, indexLevel, menuLevel);

        string actionUrl;
        string linkHtml;
        switch (childItem.MenuType)
        {
            case EMenuCtrlType.Linkbutton:
                actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl;
                linkHtml = CreateLinkHtml(id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                var onClick = string.IsNullOrEmpty(childItem.OnclickEvent) ? "null" : childItem.OnclickEvent;
                if (Component.MenuItemsInformation == null)
                {
                    Component.MenuItemsInformation = new List<VerticalMenuItemInfo>();
                }
                Component.MenuItemsInformation.Add(
                    new VerticalMenuItemInfo
                    {
                        Id = id,
                        ActionUrl = actionUrl,
                        OnClick = new JRaw(onClick),
                        CausesValidation = Component.CauseValidation,
                        Name = string.IsNullOrEmpty(childItem.ActionName) ? childItem.MenuName : childItem.ActionName
                    });
                break;
            default:
                actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl + "&" + Component.Name + "=" + id;
                linkHtml = CreateLinkHtml(id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                break;
        }

        return linkHtml;
    }

    public string CreateLinkHtml(string id, EMenuCtrlType menuType, string? cssClass, string? text, string? accessText, string? actionUrl, string? accessCss)
    {
        var tagBuilderAnchor = new TagBuilder("a");
        tagBuilderAnchor.MergeAttribute("id", id);
        if (Component.MenuItemsInformation == null)
        {
            Component.MenuItemsInformation = new List<VerticalMenuItemInfo>();
        }

        switch (menuType)
        {
            case EMenuCtrlType.Linkbutton:
                tagBuilderAnchor.MergeAttribute("href", "###");
                break;
            default:
                tagBuilderAnchor.MergeAttribute("href", !string.IsNullOrEmpty(actionUrl) ? actionUrl : "###");
                break;
        }

        if (!string.IsNullOrEmpty(cssClass))
        {
            tagBuilderAnchor.AddCssClass(cssClass);
        }

        tagBuilderAnchor.MergeAttribute("title", text ?? string.Empty);

        var sbInnerHtml = new StringBuilder();
        sbInnerHtml.Append(text ?? string.Empty);

        if (!string.IsNullOrEmpty(accessText))
        {
            var tbAccSpan = new TagBuilder("span");
            if (!string.IsNullOrEmpty(accessCss))
            {
                tbAccSpan.AddCssClass("hide-access" + " " + accessCss);
            }
            else
            {
                tbAccSpan.AddCssClass("hide-access");
            }

            tbAccSpan.InnerHtml.SetHtmlContent(accessText);
            sbInnerHtml.Append(RenderTagBuilder(tbAccSpan));
        }

        tagBuilderAnchor.InnerHtml.SetHtmlContent(sbInnerHtml.ToString());
        return RenderTagBuilder(tagBuilderAnchor);
    }

    private static string GetMenuItemId(MenuItem childItem, string indexLevel, int menuLevel)
    {
        const string Idseperator = "_";
        string id;
        const string Startvalue = "menu";

        switch (childItem.MenuType)
        {
            case EMenuCtrlType.Redirect:
            case EMenuCtrlType.RedirectJs:
                id = Startvalue.AppendWithBuilder(
                    Idseperator, "hlnk", Idseperator, "I", indexLevel, Idseperator, "L", menuLevel.ToString(CultureInfo.InvariantCulture));
                break;
            case EMenuCtrlType.Linkbutton:
            case EMenuCtrlType.LinkbuttonJs:
                id = Startvalue.AppendWithBuilder(
                    Idseperator, "lbtn", Idseperator, "I", indexLevel, Idseperator, "L", menuLevel.ToString(CultureInfo.InvariantCulture));
                break;
            case EMenuCtrlType.Imagebutton:
            case EMenuCtrlType.ImagebuttonJs:
                id = Startvalue.AppendWithBuilder(
                    Idseperator, "imgbtn", Idseperator, "I", indexLevel, Idseperator, "L", menuLevel.ToString(CultureInfo.InvariantCulture));
                break;
            default:
                id = Startvalue.AppendWithBuilder(
                    Idseperator, "hlnk", Idseperator, "I", indexLevel, Idseperator, "L", menuLevel.ToString(CultureInfo.InvariantCulture));
                break;
        }

        return TagBuilder.CreateSanitizedId(id, "_");
    }
}
