// -------------------------------------------------------------------------------------------------
// <copyright file="ClickToVoiceHtmlBuilder.cs" company="OBS">
//   OBS
// </copyright>
// <summary>
//    
//    Creation Date: 23/09/2014
//    Author:  Arun Kumar (060644)
//    Description: The HTML builder class for the ClickToVoice component
// </summary>
// -------------------------------------------------------------------------------------------------

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    /// <summary>
    /// The ClickToVoice HTML builder class
    /// </summary>
    public class ClickToVoiceHtmlBuilder : HtmlBuilderBase<ClickToVoiceComponent>
    {



        /// <summary>
        /// Default ClickToVoice ImageUrl Path
        /// </summary>
        private const string ClickToVoiceImageUrl = "/Images/picto-tel.png";
        /// <summary>
        /// Initializes a new instance of the <see cref="ClickToVoiceHtmlBuilder"/> class.
        /// </summary>
        /// <param name="component">
        /// The component.
        /// </param>
        public ClickToVoiceHtmlBuilder(ClickToVoiceComponent component)
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
                var tagBuilderAnchor = new TagBuilder("a");
                tagBuilderAnchor.Attributes.Add("id", this.Component.Id);
                if (string.IsNullOrEmpty(this.Component.Title))
                {
                    this.Component.Title = string.Format(CultureInfo.CurrentCulture, "Click to call {0}", this.Component.TelephoneNumber);
                }

                tagBuilderAnchor.Attributes.Add("title", this.Component.Title);
           
                tagBuilderAnchor.Attributes.Add("href", "###");

                foreach (var attr in this.Component.HtmlAttributes)
                {
                    tagBuilderAnchor.Attributes.Add(attr.Key, attr.Value?.ToString() ?? string.Empty);
                }

                if (!string.IsNullOrEmpty(this.Component.CssAnchorTag))
                {
                    tagBuilderAnchor.AddCssClass(this.Component.CssAnchorTag);
                }

                var sbInnerHtml = new StringBuilder();

                if (string.IsNullOrEmpty(this.Component.ImageUrl))
                {
                    this.Component.ImageUrl = ClickToVoiceImageUrl;
                }

                sbInnerHtml.Append(this.CreateImageTag());

                if (!string.IsNullOrEmpty(this.Component.TelephoneNumber) && (this.Component.IsNumberSpanVisible))
                {
                    var tbtelnoSpan = new TagBuilder("span");
                    if (!(String.IsNullOrEmpty(this.Component.CssTelephoneNumberSpan)))
                    {
                        tbtelnoSpan.AddCssClass(this.Component.CssTelephoneNumberSpan);
                    }

                    sbInnerHtml.Append($"<span class=\"{this.Component.CssTelephoneNumberSpan}\">{this.Component.TelephoneNumber}</span>");
                }

                using (var stringWriter = new StringWriter())
                {
                    tagBuilderAnchor.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                    var anchorHtml = stringWriter.ToString();
                    var closingTag = anchorHtml.IndexOf(">");
                    writer.Write(anchorHtml.Substring(0, closingTag + 1));
                    writer.Write(sbInnerHtml.ToString());
                    writer.Write("</a>");
                }
            }
        }

        /// <summary>
        /// This returns HTML string for the image tag inside the component.
        /// </summary>
        /// <returns>
        /// The Image Tag <see cref="string"/>.
        /// </returns>
        private string CreateImageTag()
        {
            var tagBuilderImage = new TagBuilder("img");

            if (string.IsNullOrEmpty(this.Component.AlternateText))
            {
                this.Component.AlternateText = this.Component.Title;
            }

            if (!string.IsNullOrEmpty(this.Component.ImageUrl))
            {
                tagBuilderImage.Attributes.Add("src", this.Component.ImageUrl);
            }

            tagBuilderImage.Attributes.Add("alt",this.Component.AlternateText);

            if (!string.IsNullOrEmpty(this.Component.CssClassImage))
            {
                tagBuilderImage.AddCssClass(this.Component.CssClassImage);
            }

            using (var stringWriter = new StringWriter())
            {
                tagBuilderImage.WriteTo(stringWriter, System.Text.Encodings.Web.HtmlEncoder.Default);
                return stringWriter.ToString();
            }
        }
    }
}
