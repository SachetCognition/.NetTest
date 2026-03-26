namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    using Newtonsoft.Json.Linq;

    public class VerticalMenuHtmlBuilder : HtmlBuilderBase<VerticalMenuComponent>
    {
        private bool isMenuSelected;

        public VerticalMenuHtmlBuilder(VerticalMenuComponent component)
        {
            this.Component = component;
        }

        public override void Build(TextWriter writer)
        {
            var tagBuilderOuterDiv = new TagBuilder("div");
            tagBuilderOuterDiv.AddCssClass(VerticalMenuComponent.OuterMenuDivCssClass);
            tagBuilderOuterDiv.Attributes["id"] = this.Component.Id;

            var tagBuilderSubOuterDiv = new TagBuilder("div");
            tagBuilderSubOuterDiv.AddCssClass("innernavbar");

            var tagBuilderMenuDiv = new TagBuilder("div");
            tagBuilderMenuDiv.AddCssClass("in");

            var menuHtml = this.CreateHeaderMenuTemplate();
            tagBuilderMenuDiv.InnerHtml.AppendHtml(menuHtml);

            var copyRightDiv = this.CreateCopyRightDiv();

            var subOuterContent = new StringBuilder();
            using (var sw = new StringWriter())
            {
                tagBuilderMenuDiv.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                subOuterContent.Append(sw.ToString());
            }
            subOuterContent.Append(copyRightDiv);
            subOuterContent.AppendFormat(CultureInfo.CurrentCulture, "<input type=\"hidden\" name=\"{0}\">", this.Component.Name);

            tagBuilderSubOuterDiv.InnerHtml.AppendHtml(subOuterContent.ToString());

            using (var sw = new StringWriter())
            {
                tagBuilderSubOuterDiv.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                tagBuilderOuterDiv.InnerHtml.AppendHtml(sw.ToString());
            }

            tagBuilderOuterDiv.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        }

        private string CreateCopyRightDiv()
        {
            var copyRightDiv = new TagBuilder("div");
            copyRightDiv.AddCssClass("copyright");
            var innerCopyDiv = new TagBuilder("div");
            innerCopyDiv.AddCssClass("innercopy");

            var copyRightTextDiv = new TagBuilder("div");
            var copyRightPara = new TagBuilder("p");
            copyRightPara.InnerHtml.Append(this.Component.CopyRightText ?? string.Empty);

            var copyRightImgAndText = new StringBuilder(string.Empty);
            copyRightImgAndText.Append(this.Component.CopyRightImage.ToHtmlString());

            using (var sw = new StringWriter())
            {
                copyRightPara.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                copyRightTextDiv.InnerHtml.AppendHtml(sw.ToString());
            }

            string copyRightTextDivStr;
            using (var sw = new StringWriter())
            {
                copyRightTextDiv.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                copyRightTextDivStr = sw.ToString();
            }

            copyRightImgAndText.Append(copyRightTextDivStr);

            innerCopyDiv.InnerHtml.AppendHtml(copyRightImgAndText.ToString());

            string innerCopyDivStr;
            using (var sw = new StringWriter())
            {
                innerCopyDiv.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                innerCopyDivStr = sw.ToString();
            }

            copyRightDiv.InnerHtml.AppendHtml(innerCopyDivStr);

            using (var sw = new StringWriter())
            {
                copyRightDiv.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                return sw.ToString();
            }
        }

        private string CreateHeaderMenuTemplate()
        {
            var headerTemplateUl = new TagBuilder("ul");
            var idValue = new StringBuilder(string.Empty).Append(this.Component.Id).Append("_menu");
            headerTemplateUl.Attributes["id"] = idValue.ToString();
            headerTemplateUl.AddCssClass("nav nav-vertical");

            headerTemplateUl.InnerHtml.AppendHtml(this.CreateMenuItems());

            using (var sw = new StringWriter())
            {
                headerTemplateUl.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
                return sw.ToString();
            }
        }

        private string CreateMenuItems()
        {
            var menuBuilder = new StringBuilder();
            int itemIndex = 1;

            foreach (MenuItem item in this.Component.MenuItems)
            {
                this.isMenuSelected = false;
                if (item.Hidden)
                {
                    continue;
                }

                var liBuilder = new TagBuilder("li");

                var anchorBuilder1Html = this.CreateInnerMenuHtml(item, itemIndex.ToString(CultureInfo.InvariantCulture), 1, "dropdown-toggle");

                var childMenuTagsUl = this.ChildMenuTagsUl(item, itemIndex);
                if (this.isMenuSelected)
                {
                    liBuilder.AddCssClass("active");
                }

                var childHtml = childMenuTagsUl != null ? TagBuilderToString(childMenuTagsUl) : string.Empty;
                liBuilder.InnerHtml.AppendHtml(anchorBuilder1Html + childHtml);

                string liStr = TagBuilderToString(liBuilder);
                if (Array.IndexOf((liStr.Split('"')), this.Component.SelectedMenu) > -1)
                {
                    liBuilder = new TagBuilder("li");
                    liBuilder.AddCssClass("first active");
                    liBuilder.InnerHtml.AppendHtml(anchorBuilder1Html + childHtml);
                    this.isMenuSelected = true;
                }
                else
                {
                    liBuilder.AddCssClass("first");
                }

                itemIndex++;
                menuBuilder.Append(TagBuilderToString(liBuilder));
            }

            return menuBuilder.ToString();
        }

        private TagBuilder ChildMenuTagsUl(MenuItem item, int itemIndex)
        {
            TagBuilder childMenuTagsUl = null;
            var childMenus = item.ReturnChildMenu();
            if (childMenus != null && childMenus.Count > 0)
            {
                childMenuTagsUl = new TagBuilder("ul");

                childMenuTagsUl.Attributes["style"] = "display: none";
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
                    var childLiTag = this.ChildLiTag(childItem, indexLevel.ToString(), 2);

                    if (this.isMenuSelected)
                    {
                        childMenuTagsUl.Attributes["style"] = "display:block";
                    }

                    childMenuItems.Append(TagBuilderToString(childLiTag));
                    index++;
                }

                childMenuTagsUl.InnerHtml.AppendHtml(childMenuItems.ToString());
            }

            return childMenuTagsUl;
        }

        private TagBuilder ChildLiTag(MenuItem childItem, string indexLevel, int menuLevel)
        {
            var childLiTag = new TagBuilder("li");
            string selectedMenuName = this.Component.SelectedMenu;

            var menuCtrlHtml = this.CreateInnerMenuHtml(childItem, indexLevel, menuLevel, "first");
            childLiTag.InnerHtml.AppendHtml(menuCtrlHtml);

            string liStr = TagBuilderToString(childLiTag);
            if (Array.IndexOf((liStr.Split('"')), selectedMenuName) > -1)
            {
                childLiTag = new TagBuilder("li");
                childLiTag.AddCssClass("first active");
                childLiTag.InnerHtml.AppendHtml(menuCtrlHtml);
                this.isMenuSelected = true;
            }
            else
            {
                childLiTag.AddCssClass("first");
            }

            return childLiTag;
        }

        private string CreateInnerMenuHtml(MenuItem childItem, string indexLevel, int menuLevel, string cssclass)
        {
            string accessText = null;
            if (menuLevel == 1 && childItem.ReturnChildMenu() != null && childItem.ReturnChildMenu().Count > 0)
            {
                accessText = "ouvert";
            }

            var id = GetMenuItemId(childItem, indexLevel, menuLevel);

            string actionUrl;
            string linkHtml;
            switch (childItem.MenuType)
            {
                case EMenuCtrlType.Linkbutton:
                    actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl;
                    linkHtml = this.CreateLinkHtml(id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                    var onClick = string.IsNullOrEmpty(childItem.OnclickEvent) ? "null" : childItem.OnclickEvent;
                    if (this.Component.MenuItemsInformation == null)
                    {
                        this.Component.MenuItemsInformation = new List<VerticalMenuItemInfo>();
                    }
                    this.Component.MenuItemsInformation.Add(
                        new VerticalMenuItemInfo
                        {
                            Id = id,
                            ActionUrl = actionUrl,
                            OnClick = new JRaw(onClick),
                            CausesValidation = this.Component.CauseValidation,
                            Name = string.IsNullOrEmpty(childItem.ActionName) ? childItem.MenuName : childItem.ActionName
                        });
                    break;
                default:
                    actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl + "&" + this.Component.Name + "=" + id;
                    linkHtml = this.CreateLinkHtml(id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                    break;
            }

            return linkHtml;
        }

        public string CreateLinkHtml(string id, EMenuCtrlType menuType, string cssClass, string text, string accessText, string actionUrl, string accessCss)
        {
            var tagBuilderAnchor = new TagBuilder("a");
            tagBuilderAnchor.Attributes["id"] = id;
            if (this.Component.MenuItemsInformation == null)
            {
                this.Component.MenuItemsInformation = new List<VerticalMenuItemInfo>();
            }

            switch (menuType)
            {
                case EMenuCtrlType.Linkbutton:
                    tagBuilderAnchor.Attributes["href"] = "###";
                    break;
                default:
                    tagBuilderAnchor.Attributes["href"] = !string.IsNullOrEmpty(actionUrl) ? actionUrl : "###";
                    break;
            }

            if (!string.IsNullOrEmpty(cssClass))
            {
                tagBuilderAnchor.AddCssClass(cssClass);
            }

            tagBuilderAnchor.Attributes["title"] = text;

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

                tbAccSpan.InnerHtml.Append(accessText);
                sbInnerHtml.Append(TagBuilderToString(tbAccSpan));
            }

            tagBuilderAnchor.InnerHtml.AppendHtml(sbInnerHtml.ToString());
            return TagBuilderToString(tagBuilderAnchor);
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
