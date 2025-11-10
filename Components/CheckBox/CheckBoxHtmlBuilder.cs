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
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

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

            if (this.Component != null && this.Component.IsVisible)
            {
                // This hidden field is used to bind the value of the checkbox to a boolean
                // It handles unchecked and disabled states
                var tagBuilderHidden = new TagBuilder("input");
                tagBuilderHidden.Attributes.Add("id", string.Format(CultureInfo.InvariantCulture, "{0}Hidden", this.Component.Id));

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderHidden.Attributes.Add("name", this.Component.Name);
                }

                tagBuilderHidden.Attributes.Add("type", "hidden");
                tagBuilderHidden.Attributes.Add("value", this.Component.IsChecked ? "true" : "false");

                var tagBuilderCheckBox = new TagBuilder("input");
                tagBuilderCheckBox.Attributes.Add("id", this.Component.Id);
                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderCheckBox.Attributes.Add("name", this.Component.Name);
                }
                tagBuilderCheckBox.Attributes.Add("type", "checkbox");
                tagBuilderCheckBox.Attributes.Add("value", this.Component.IsChecked ? "true" : "false");
               
                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderCheckBox.Attributes.Add(attr.Key, attr.Value?.ToString() ?? string.Empty);
                }

                if (this.Component.IsDisabled)
                {
                    this.Component.CssClass = this.Component.CssClassDisabled;
                    tagBuilderCheckBox.Attributes.Add("disabled", "disabled");
                }

                if (!string.IsNullOrEmpty(this.Component.CssClass))
                {
                    tagBuilderCheckBox.AddCssClass(this.Component.CssClass);
                }

                if (!string.IsNullOrEmpty(this.Component.Title))
                {
                    tagBuilderCheckBox.Attributes.Add("title", this.Component.Title);
                }

                var sbHtml = new StringBuilder();
                using (var stringWriter = new StringWriter())
                {
                    tagBuilderHidden.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    sbHtml.Append(stringWriter.ToString());
                }
                using (var stringWriter = new StringWriter())
                {
                    tagBuilderCheckBox.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    sbHtml.Append(stringWriter.ToString());
                }
                writer.Write(sbHtml);
            }
        }
    }
}
