// -------------------------------------------------------------------------------------------------
// <copyright file="CustomLabelHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 04/04/2014
//    Author:  Sharma Siddharth (54626) 
//    Description: Contains methods to build HTML for the CustomLabel component
// </summary>
// -------------------------------------------------------------------------------------------------
namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
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
    public class CustomLabelHtmlBuilder : HtmlBuilderBase<CustomLabelComponent>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomLabelHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public CustomLabelHtmlBuilder(CustomLabelComponent component)
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
#if DEBUG
                if (this.Component.HtmlAttributes["for"] == null)
                {
                   
                    throw new ArgumentException("The attribute 'for' is mandatory to add with each label.");

                }
#endif
                var tagBuilderCustomLabel = new TagBuilder("label");
                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderCustomLabel.Attributes.Add(attr.Key, attr.Value?.ToString() ?? string.Empty);
                }

                var sbCustomLabelInnerHtml = new StringBuilder();
                
                var textToDisplay = !string.IsNullOrEmpty(this.Component.Text) ? this.Component.Text : string.Empty;
                if (this.Component.IsHtmlEncode)
                {
                    textToDisplay = System.Net.WebUtility.HtmlEncode(textToDisplay);
                }
                sbCustomLabelInnerHtml.Append($"<span>{textToDisplay}</span>");

                if (!this.Component.IsOnlyForAccess)
                {
                    if (string.IsNullOrEmpty(this.Component.CssClassLabel))
                    {
                        this.Component.CssClassLabel = "pull-right";
                    }

                    if (!string.IsNullOrEmpty(this.Component.CssClassLabel))
                    {
                        tagBuilderCustomLabel.AddCssClass(this.Component.CssClassLabel);
                    }

                    if (!string.IsNullOrEmpty(this.Component.SuperscriptText))
                    {
                        var superScriptCss = !string.IsNullOrEmpty(this.Component.SuperscriptCssClass) ? this.Component.SuperscriptCssClass : "importantfield";
                        var tooltip = !string.IsNullOrEmpty(this.Component.SuperscriptToolTip) ? $" title=\"{System.Net.WebUtility.HtmlEncode(this.Component.SuperscriptToolTip)}\"" : string.Empty;
                        sbCustomLabelInnerHtml.Append($"<span class=\"{superScriptCss}\"{tooltip}>{this.Component.SuperscriptText}</span>");
                    }

                    if (this.Component.DisplayStar)
                    {
                        var asteriskTitle = string.Format(CultureInfo.CurrentCulture, "Required field: {0}", this.Component.Text);
                        sbCustomLabelInnerHtml.Append($"<abbr title=\"{System.Net.WebUtility.HtmlEncode(asteriskTitle)}\">*</abbr>");
                    }

                    if (this.Component.DisplayColon)
                    {
                        sbCustomLabelInnerHtml.Append("<span class=\"paddingColon\">:</span>");
                    }
                    if (!string.IsNullOrEmpty(this.Component.AccessText))
                    {
                        sbCustomLabelInnerHtml.Append($"<span class=\"hide-access\">{this.Component.AccessText}</span>");
                    }
                }
                else
                {
                    tagBuilderCustomLabel.AddCssClass("hide-access");
                }

                using (var stringWriter = new StringWriter())
                {
                    tagBuilderCustomLabel.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    var labelHtml = stringWriter.ToString();
                    var closingTag = labelHtml.IndexOf(">");
                    writer.Write(labelHtml.Substring(0, closingTag + 1));
                    writer.Write(sbCustomLabelInnerHtml.ToString());
                    writer.Write("</label>");
                }
            }
        }
    }
}
