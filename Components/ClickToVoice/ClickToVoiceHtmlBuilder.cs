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
    using System.Text;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using System.IO;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Html;

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

            if (this.Component.IsVisible)
            {
                // Note that in this component, AccessText is not taken into account.
                // As per Accessibility requirements, Anchor Title and Image alt are set as per ApplicationStrings.TIP000022
                var tagBuilderAnchor = new TagBuilder("a");
                tagBuilderAnchor.MergeAttribute("id", this.Component.Id);
                if (string.IsNullOrEmpty(this.Component.Title))
                {
                    this.Component.Title = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000022, this.Component.TelephoneNumber);
                }

                tagBuilderAnchor.MergeAttribute("title", this.Component.Title);
           
                tagBuilderAnchor.MergeAttribute("href", "###");

                tagBuilderAnchor.MergeAttributes(this.Component.HtmlAttributes);

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

                //Span to show the telephone number if phone number is not null and "IsNumberSpanVisibal" is true.
                if (!string.IsNullOrEmpty(this.Component.TelephoneNumber) && (this.Component.IsNumberSpanVisible))
                {
                    var tbtelnoSpan = new TagBuilder("span");
                    if (!(String.IsNullOrEmpty(this.Component.CssTelephoneNumberSpan)))
                    {

                        tbtelnoSpan.AddCssClass(this.Component.CssTelephoneNumberSpan);
                    }

                    tbtelnoSpan.InnerHtml.SetHtmlContent(this.Component.TelephoneNumber);
                    sbInnerHtml.Append(tbtelnoSpan);
                }

                tagBuilderAnchor.InnerHtml.SetHtmlContent(sbInnerHtml.ToString());
                writer.Write(tagBuilderAnchor.ToString());
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

            // As per accessibility requirement, unless the alt has been provided, it is set to the same as
            // anchor title
            if (string.IsNullOrEmpty(this.Component.AlternateText))
            {
                this.Component.AlternateText = this.Component.Title;
            }

            if (!string.IsNullOrEmpty(this.Component.ImageUrl))
            {
                tagBuilderImage.MergeAttribute("src", this.Component.ImageUrl);
            }

            tagBuilderImage.MergeAttribute("alt",this.Component.AlternateText);

            if (!string.IsNullOrEmpty(this.Component.CssClassImage))
            {
                tagBuilderImage.AddCssClass(this.Component.CssClassImage);
            }

            return tagBuilderImage.ToHtmlString(TagRenderMode.StartTag);
        }
    }
}
