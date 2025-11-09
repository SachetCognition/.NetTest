// -------------------------------------------------------------------------------------------------
// <copyright file="ActionButtonHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Ankur Kumar
//    Description: The Html Builder class for the ActionButton component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ActionButton
{
    using System;
    using System.Web.Mvc;
    using System.Web.UI;
    using System.Text;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// The Html Builder class for the ActionButton component
    /// </summary>
    public class ActionButtonHtmlBuilder : HtmlBuilderBase<ActionButtonComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionButtonHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public ActionButtonHtmlBuilder(ActionButtonComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(HtmlTextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }

            if (this.Component.IsVisible)
            {
                var tbActionButton = new TagBuilder("button");
                tbActionButton.MergeAttribute("id", this.Component.Id);

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tbActionButton.MergeAttribute("name", this.Component.Name);
                }

                if (!string.IsNullOrWhiteSpace(this.Component.DialogBoxId))
                {
                    tbActionButton.MergeAttribute("data-toggle","modal");
                    tbActionButton.MergeAttribute("data-target", "#"+ this.Component.DialogBoxId);
                }

                tbActionButton.MergeAttribute("type", "submit");
                tbActionButton.MergeAttributes(this.Component.HtmlAttributes);
                tbActionButton.AddCssClass(this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass);

                var sbInnerHtml = new StringBuilder();
                if (!string.IsNullOrEmpty(this.Component.AccessText))
                {
                    var tbAccSpan = new TagBuilder("span");
                    tbAccSpan.AddCssClass("hide-access");
                    tbAccSpan.InnerHtml = this.Component.AccessText;
                    sbInnerHtml.Append(tbAccSpan);
                }
                
                var tbTextSpan = new TagBuilder("span");
                if (!string.IsNullOrEmpty(this.Component.CssSpan))
                {
                    tbTextSpan.AddCssClass(this.Component.CssSpan);
                }

                tbTextSpan.InnerHtml = this.Component.Text;
                sbInnerHtml.Append(tbTextSpan);
                tbActionButton.InnerHtml = sbInnerHtml.ToString();
                writer.Write(tbActionButton.ToString());
            }
        }
    }
}
