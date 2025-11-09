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
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
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
        public override void Build(TextWriter writer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }

            if (this.Component != null && this.Component.IsVisible)
            {
                var tbActionButton = new TagBuilder("button");
                tbActionButton.Attributes.Add("id", this.Component.Id);

                if (!string.IsNullOrEmpty(this.Component.Name))
                {
                    tbActionButton.Attributes.Add("name", this.Component.Name);
                }

                if (!string.IsNullOrWhiteSpace(this.Component.DialogBoxId))
                {
                    tbActionButton.Attributes.Add("data-toggle","modal");
                    tbActionButton.Attributes.Add("data-target", "#"+ this.Component.DialogBoxId);
                }

                tbActionButton.Attributes.Add("type", "submit");
                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tbActionButton.Attributes.Add(attr.Key, attr.Value?.ToString() ?? string.Empty);
                }
                tbActionButton.AddCssClass(this.Component.IsDisabled ? this.Component.CssClassReadOnly : this.Component.CssClass);

                var sbInnerHtml = new StringBuilder();
                if (!string.IsNullOrEmpty(this.Component.AccessText))
                {
                    var tbAccSpan = new TagBuilder("span");
                    tbAccSpan.AddCssClass("hide-access");
                    using (var spanWriter = new StringWriter())
                    {
                        tbAccSpan.WriteTo(spanWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                        var spanHtml = spanWriter.ToString();
                        var closingTag = spanHtml.IndexOf(">");
                        sbInnerHtml.Append(spanHtml.Substring(0, closingTag + 1));
                        sbInnerHtml.Append(this.Component.AccessText);
                        sbInnerHtml.Append("</span>");
                    }
                }
                
                var tbTextSpan = new TagBuilder("span");
                if (!string.IsNullOrEmpty(this.Component.CssSpan))
                {
                    tbTextSpan.AddCssClass(this.Component.CssSpan);
                }

                using (var spanWriter = new StringWriter())
                {
                    tbTextSpan.WriteTo(spanWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    var spanHtml = spanWriter.ToString();
                    var closingTag = spanHtml.IndexOf(">");
                    sbInnerHtml.Append(spanHtml.Substring(0, closingTag + 1));
                    sbInnerHtml.Append(this.Component.Text);
                    sbInnerHtml.Append("</span>");
                }

                using (var buttonWriter = new StringWriter())
                {
                    tbActionButton.WriteTo(buttonWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    var buttonHtml = buttonWriter.ToString();
                    var closingTag = buttonHtml.IndexOf(">");
                    writer.Write(buttonHtml.Substring(0, closingTag + 1));
                    writer.Write(sbInnerHtml.ToString());
                    writer.Write("</button>");
                }
            }
        }
    }
}
