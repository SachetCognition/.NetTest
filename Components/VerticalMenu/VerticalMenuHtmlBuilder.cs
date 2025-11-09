// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalMenuHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 26/11/2014
//   Author:  Seema Lal Gulabrani
//   Description: Vertical Menu Html Builder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.VerticalMenu
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.IO;

    using Equant.SAV2000.ComponentLibrary.Common.Components.DataTables;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.HyperLink;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Menu;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Vertical Menu Html Builder
    /// </summary>
    public class VerticalMenuHtmlBuilder : HtmlBuilderBase<VerticalMenuComponent>
    {
        #region Constructors and Destructors

        /// <summary>
        /// The is menu selected.
        /// </summary>
        private bool isMenuSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalMenuHtmlBuilder"/> class. 
        /// </summary>
        /// <param name="component">
        /// The Component.
        /// </param>
        public VerticalMenuHtmlBuilder(VerticalMenuComponent component)
        {
            this.Component = component;
        }

        #endregion

        #region Overrides of HtmlBuilderBase<MenuComponent>

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
          [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0"
    , Justification = "HtmlTextWriter can never be null as it is handled by the base framework.")]
        public override void Build(TextWriter writer)
        {
            var tagBuilderOuterDiv = new TagBuilder("div");
            tagBuilderOuterDiv.AddCssClass(VerticalMenuComponent.OuterMenuDivCssClass);
            tagBuilderOuterDiv.MergeAttribute("id", this.Component.Id);
         
            var tagBuilderSubOuterDiv = new TagBuilder("div");
            tagBuilderSubOuterDiv.AddCssClass("innernavbar");
           
            //<DIV class=in>
            var tagBuilderMenuDiv = new TagBuilder("div");
            tagBuilderMenuDiv.AddCssClass("in");
            
            //Create MenuItem in Div based on item collection
            tagBuilderMenuDiv.InnerHtml.AppendHtml(this.CreateHeaderMenuTemplate());

            var copyRightDiv = this.CreateCopyRightDiv();

            //Add tagBuilderMenuDiv in tagBuilderSubOuterDiv
            tagBuilderSubOuterDiv.InnerHtml.AppendHtml(tagBuilderMenuDiv.ToString()
                .AppendWithBuilder(copyRightDiv, string.Format(CultureInfo.CurrentCulture, "<input type = 'hidden' name='{0}'>", this.Component.Name)));

            //Add tagBuilderSubOuterDiv in outer Div
            tagBuilderOuterDiv.InnerHtml.AppendHtml(tagBuilderSubOuterDiv.ToString());

            //Render menu HTML
            writer.Write(tagBuilderOuterDiv.ToString());
        }

        /// <summary>
        /// The create copy right div.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateCopyRightDiv()
        {
           var copyRightDiv = new TagBuilder("div");
            copyRightDiv.AddCssClass("copyright");
            var innerCopyDIv = new TagBuilder("div");
            innerCopyDIv.AddCssClass("innercopy");
           
            var copyRightTextDiv = new TagBuilder("div");
            var copyRightPara = new TagBuilder("p");
            copyRightPara.InnerHtml.Append(this.Component.CopyRightText);
            copyRightTextDiv.InnerHtml.AppendHtml(copyRightPara.ToString());

            var copyRightImgAndText = new StringBuilder(string.Empty);
            copyRightImgAndText.Append(this.Component.CopyRightImage.ToHtmlString());
         
            copyRightImgAndText.Append(copyRightTextDiv);
            innerCopyDIv.InnerHtml.AppendHtml(copyRightImgAndText.ToString());
            copyRightDiv.InnerHtml.AppendHtml(innerCopyDIv.ToString());

            return copyRightDiv.ToString();
        }

        /// <summary>
        /// The create Header menu template.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        private string CreateHeaderMenuTemplate()
        {
            var headerTemplateUl = new TagBuilder("ul");
            var idValue = new StringBuilder(string.Empty).Append(this.Component.Id).Append("_menu");
            headerTemplateUl.MergeAttribute("id", idValue.ToString());
            headerTemplateUl.AddCssClass("nav nav-vertical");
           
            //Add Other menu Items
            headerTemplateUl.InnerHtml.AppendHtml(this.CreateMenuItems());
            return headerTemplateUl.ToString();
        }

        /// <summary>
        /// The create menu items.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateMenuItems()
        {
            var menuBuilder = new StringBuilder();
            int itemIndex = 1;
           
            foreach(MenuItem item in this.Component.MenuItems)
            {
                this.isMenuSelected = false;
                if (item.Hidden)
                {
                    continue;
                }

                var liBuilder = new TagBuilder("li");

                var anchorBuilder1Html = this.CreateInnerMenuHtml(item, itemIndex.ToString(CultureInfo.InvariantCulture), 1, "dropdown-toggle");

                //anchorBuilder1.AccessText= "ouvert";
                
                var childMenuTagsUl = this.ChildMenuTagsUl(item, itemIndex);
                if ((this.isMenuSelected))
                {
                    liBuilder.AddCssClass("active");
                }

                liBuilder.InnerHtml.AppendHtml(anchorBuilder1Html + (childMenuTagsUl == null ? string.Empty : childMenuTagsUl.ToString()));

                if (Array.IndexOf((liBuilder.InnerHtml.Split('"')), this.Component.SelectedMenu) > -1)
                {
                    liBuilder.AddCssClass("first active");
                    this.isMenuSelected = true;
                }
                else
                {
                    liBuilder.AddCssClass("first");
                }

                itemIndex++;
                menuBuilder.Append(liBuilder);
            }

            return menuBuilder.ToString();
        }

        /// <summary>
        /// The child menu tags .
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="itemIndex">
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        private TagBuilder ChildMenuTagsUl(MenuItem item, int itemIndex)
        {
            TagBuilder childMenuTagsUl = null;
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
                    var childLiTag = this.ChildLiTag(childItem, indexLevel.ToString(), 2);
                    
                    if (this.isMenuSelected)
                    {
                        childMenuTagsUl.Attributes["style"] = "display:block";
                    }

                    childMenuItems.Append(childLiTag);
                    index++;
                }

                childMenuTagsUl.InnerHtml.AppendHtml(childMenuItems.ToString());
            }

            return childMenuTagsUl;
        }

        /// <summary>
        /// The child li tag.
        /// </summary>
        /// <param name="childItem">
        /// The child item.
        /// </param>
        /// <param name="indexLevel">
        /// index level of menu item 
        /// </param>
        /// <param name="menuLevel">
        /// menu level 
        /// </param>
        /// <returns>
        /// The <see cref="TagBuilder"/>.
        /// </returns>
        private TagBuilder ChildLiTag(MenuItem childItem, string indexLevel, int menuLevel)
        {
            var childLiTag = new TagBuilder("li");
            string selectedMenuName = this.Component.SelectedMenu;

            var menuCtrlHtml = this.CreateInnerMenuHtml(childItem, indexLevel, menuLevel, "first");
            childLiTag.InnerHtml.AppendHtml(menuCtrlHtml);

            //childLiTag.InnerHtml.AppendHtml(childLiTag.InnerHtml.Replace("<a", string.Format(CultureInfo.CurrentCulture, "<a onclick=$.selectedMenuItem(this.id,'{0}')", this.Component.Name)));
           
            if (Array.IndexOf((childLiTag.InnerHtml.Split('"')), selectedMenuName) > -1)
            {
                childLiTag.AddCssClass("first active");
                this.isMenuSelected = true;
            }
            else
            {
                childLiTag.AddCssClass("first");
            }

            return childLiTag;
        }

        /// <summary>
        /// The create inner menu html.
        /// </summary>
        /// <param name="childItem">
        /// The child item.
        /// </param>
        /// <param name="indexLevel">
        /// The index level.
        /// </param>
        /// <param name="menuLevel">
        /// The menu level.
        /// </param>
        /// <param name="cssclass">
        /// The CSS class.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string CreateInnerMenuHtml(MenuItem childItem, string indexLevel, int menuLevel, string cssclass)
        {

            string accessText = null;
            if (menuLevel == 1 && childItem.ReturnChildMenu() != null && childItem.ReturnChildMenu().Count > 0)
            {
                accessText = ApplicationStrings.LBL000011;
            }

            var Id = GetMenuItemId(childItem, indexLevel, menuLevel);

            string actionUrl;
            string linkHtml;
            switch (childItem.MenuType)
            {
                case EMenuCtrlType.Linkbutton:
                    actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl;
                    linkHtml = this.CreateLinkHtml(Id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                    var onClick = string.IsNullOrEmpty(childItem.OnclickEvent) ? "null" : childItem.OnclickEvent;
                    this.Component.MenuItemsInformation.Add(
                        new VerticalMenuItemInfo
                        {
                            Id = Id,
                            ActionUrl = actionUrl,
                            OnClick = new JRaw(onClick),
                            CausesValidation = this.Component.CauseValidation,
                            Name = string.IsNullOrEmpty(childItem.ActionName) ? childItem.MenuName : childItem.ActionName
                        });
                    break;
                //for case EMenuCtrlType.Redirect
                default:
                    actionUrl = string.IsNullOrEmpty(childItem.ActionUrl) ? "#" : childItem.ActionUrl + "&" + this.Component.Name + "=" + Id;
                    linkHtml = this.CreateLinkHtml(Id, childItem.MenuType, cssclass, childItem.MenuName, accessText, actionUrl, "close");
                    break;
            }

            return linkHtml;
        }

        /// <summary>
        /// The create link html.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="menuType">
        /// The menu type.
        /// </param>
        /// <param name="cssClass">
        /// The CSS class.
        /// </param>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <param name="accessText">
        /// The access text.
        /// </param>
        /// <param name="actionUrl">
        /// The action url.
        /// </param>
        /// <param name="accessCss">
        /// The access CSS.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CreateLinkHtml(string id, EMenuCtrlType menuType, string cssClass, string text, string accessText, string actionUrl, string accessCss)
        {
            var tagBuilderAnchor = new TagBuilder("a");
            tagBuilderAnchor.MergeAttribute("id", id);
            if (this.Component.MenuItemsInformation == null)
            {
                this.Component.MenuItemsInformation = new List<VerticalMenuItemInfo>();
            }

            switch (menuType)
            {
                case EMenuCtrlType.Linkbutton:
                    tagBuilderAnchor.MergeAttribute("href", "###");
                    break;
                default:
                    tagBuilderAnchor.MergeAttribute("href", !String.IsNullOrEmpty(actionUrl) ? actionUrl : "###");
                    break;
            }

            if (!string.IsNullOrEmpty(cssClass))
            {
                tagBuilderAnchor.AddCssClass(cssClass);
            }

            tagBuilderAnchor.MergeAttribute("title", text);

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

                tbAccSpan.InnerHtml.AppendHtml(accessText);
                sbInnerHtml.Append(tbAccSpan);
            }

            tagBuilderAnchor.InnerHtml.AppendHtml(sbInnerHtml.ToString());
            return tagBuilderAnchor.ToString();
        }

        /// <summary>
        /// The get menu item id.
        /// </summary>
        /// <param name="childItem">
        /// The child item.
        /// </param>
        /// <param name="indexLevel">
        /// index level of menu item 
        /// </param>
        /// <param name="menuLevel">
        /// menu level 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
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

        #endregion
    }
}
