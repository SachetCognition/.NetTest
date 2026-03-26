// -------------------------------------------------------------------------------------------------
// <copyright file="CheckBoxHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 13/03/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains methods to build HTML for the checkbox component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CheckBox
{
    using System;
    using System.Globalization;
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// The check box html builder.
    /// </summary>
    public class CheckBoxHtmlBuilder : HtmlBuilderBase<CheckBoxComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public CheckBoxHtmlBuilder(CheckBoxComponent component)
        {
            this.Component = component;
        }

        /// <summary>
        /// The build.
        /// </summary>
        /// <param name="writer">
        /// The writer.
        /// </param>
        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentException("The parameter writer cannot be null");
            }

            if (this.Component.IsVisible)
            {
                // This hidden field is used to bind the value of the checkbox to a boolean
                // It handles unchecked and disabled states
                var tagBuilderHidden = new TagBuilder("input");
                tagBuilderHidden.MergeAttribute("id", string.Format(CultureInfo.InvariantCulture, "{0}Hidden", this.Component.Id));

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderHidden.MergeAttribute("name", this.Component.Name);
                }


                tagBuilderHidden.MergeAttribute("type", "hidden");
                tagBuilderHidden.MergeAttribute("value", this.Component.IsChecked ? "true" : "false");

                var tagBuilderCheckBox = new TagBuilder("input");
                tagBuilderCheckBox.MergeAttribute("id", this.Component.Id);
                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderCheckBox.MergeAttribute("name", this.Component.Name);
                }
                tagBuilderCheckBox.MergeAttribute("type", "checkbox");
                tagBuilderCheckBox.MergeAttribute("value", this.Component.IsChecked ? "true" : "false");
               
                
                tagBuilderCheckBox.MergeAttributes(this.Component.HtmlAttributes);

                if (this.Component.IsDisabled)
                {
                    this.Component.CssClass = this.Component.CssClassDisabled;
                    tagBuilderCheckBox.MergeAttribute("disabled", "disabled");
                }

                if (!string.IsNullOrEmpty(this.Component.CssClass))
                {
                    tagBuilderCheckBox.AddCssClass(this.Component.CssClass);
                }

                if (!string.IsNullOrEmpty(this.Component.Title))
                {
                    tagBuilderCheckBox.MergeAttribute("title", this.Component.Title);
                }

                var sbHtml = new StringBuilder();
                sbHtml.Append(tagBuilderHidden.ToHtmlString(TagRenderMode.StartTag));
                sbHtml.Append(tagBuilderCheckBox.ToHtmlString(TagRenderMode.StartTag));
                writer.Write(sbHtml);
            }
        }
    }
}
