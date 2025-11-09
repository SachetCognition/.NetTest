// -------------------------------------------------------------------------------------------------
// <copyright file="ButtonHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 07/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: The Html Builder class for the Button component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Button
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// The Html Builder class for the Button component
    /// </summary>
    public class ButtonHtmlBuilder : HtmlBuilderBase<ButtonComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public ButtonHtmlBuilder(ButtonComponent component)
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
                var tagBuilderButton = new TagBuilder("input");
                tagBuilderButton.Attributes.Add("id", this.Component.Id);

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tagBuilderButton.Attributes.Add("name", this.Component.Name);
                }

                tagBuilderButton.Attributes.Add("type", "submit");
                if (!string.IsNullOrWhiteSpace(this.Component.DialogDivId))
                {
                    tagBuilderButton.Attributes.Add("data-toggle", "modal"); 
                    tagBuilderButton.Attributes.Add("data-target", "#"+this.Component.DialogDivId);
                }

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderButton.Attributes.Add(attr.Key, attr.Value?.ToString() ?? string.Empty);
                }

                tagBuilderButton.AddCssClass(this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass);

                using (var stringWriter = new StringWriter())
                {
                    tagBuilderButton.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    writer.Write(stringWriter.ToString());
                }
            }
        }
    }
}
