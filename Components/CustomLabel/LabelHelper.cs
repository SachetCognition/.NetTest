// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LabelHelper.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//   Creation Date: 14/01/2015
//   Author:  Seema Lal Gulabrani
//   Description: A utility class with methods to generate some HTML elements for the Label-based components
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.IO;

    /// <summary>
    /// The label helper.
    /// </summary>
    public static class LabelHelper
    {
        /// <summary>
        /// This returns HTML string for the superscript tag inside the calling component.
        /// </summary>
        /// <param name="spanText">
        /// The span Text.
        /// </param>
        /// <param name="spanCssClass">
        /// The span CSS Class.
        /// </param>
        /// <param name="spanToolTip">
        /// The span Tool Tip.
        /// </param>
        /// <param name="isHtmlEncode">
        /// The is Html Encode.
        /// </param>
        /// <returns>
        /// The html string for the span <see cref="string"/>.
        /// </returns>
        public static string InnerSpanTag(string spanText, string spanCssClass, string spanToolTip, bool isHtmlEncode)
        {
            var tagBuilderSpan = new TagBuilder("span");

            if (!string.IsNullOrEmpty(spanToolTip))
            {
                tagBuilderSpan.Attributes.Add("title", spanToolTip);
            }

            if (!string.IsNullOrEmpty(spanCssClass))
            {
                tagBuilderSpan.Attributes.Add("class", spanCssClass);
            }

            var innerText = isHtmlEncode ? System.Net.WebUtility.HtmlEncode(spanText) : spanText;
            
            using (var stringWriter = new StringWriter())
            {
                tagBuilderSpan.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                var html = stringWriter.ToString();
                var closingTag = html.IndexOf(">");
                return html.Substring(0, closingTag + 1) + innerText + "</span>";
            }
        }

        /// <summary>
        /// This returns HTML string for the required tag inside the calling component.
        /// </summary>
        /// <param name="abbrText">
        /// The abbreviation Text.
        /// </param>
        /// <param name="spanToolTip">
        /// The span Tool Tip.
        /// </param>
        /// <returns>
        /// The html string for the span <see cref="string"/>.
        /// </returns>
        public static string InnerAbbrTag(string abbrText, string spanToolTip)
        {
            var tagBuilderSpan = new TagBuilder("abbr");

            if (!string.IsNullOrEmpty(spanToolTip))
            {
                tagBuilderSpan.MergeAttribute("title", spanToolTip);
            }

            tagBuilderSpan.Attributes.Add("class", "required");
            
            using (var stringWriter = new StringWriter())
            {
                tagBuilderSpan.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                var html = stringWriter.ToString();
                var closingTag = html.IndexOf(">");
                return html.Substring(0, closingTag + 1) + abbrText + "</abbr>";
            }
        }
    }
}
