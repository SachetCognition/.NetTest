namespace Equant.SAV2000.ComponentLibrary.MVC.Components.ClickToVoice
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text.Encodings.Web;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class ClickToVoiceHtmlBuilder : HtmlBuilderBase<ClickToVoiceComponent>
    {
        private const string ClickToVoiceImageUrl = "/Images/picto-tel.png";

        public ClickToVoiceHtmlBuilder(ClickToVoiceComponent component) : base(component) { }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible)
            {
                return HtmlString.Empty;
            }

            var tagBuilderAnchor = new TagBuilder("a");
            tagBuilderAnchor.MergeAttribute("id", this.Component.Id);

            if (string.IsNullOrEmpty(this.Component.Title))
            {
                this.Component.Title = string.Format(CultureInfo.CurrentCulture, ApplicationStrings.TIP000022, this.Component.TelephoneNumber);
            }

            tagBuilderAnchor.MergeAttribute("title", this.Component.Title);
            tagBuilderAnchor.MergeAttribute("href", "###");

            foreach (var attr in this.Component.HtmlAttributes)
            {
                tagBuilderAnchor.MergeAttribute(attr.Key, attr.Value?.ToString());
            }

            if (!string.IsNullOrEmpty(this.Component.CssAnchorTag))
            {
                tagBuilderAnchor.AddCssClass(this.Component.CssAnchorTag);
            }

            if (string.IsNullOrEmpty(this.Component.ImageUrl))
            {
                this.Component.ImageUrl = ClickToVoiceImageUrl;
            }

            tagBuilderAnchor.InnerHtml.AppendHtml(this.CreateImageTag());

            // Span to show the telephone number if phone number is not null and "IsNumberSpanVisible" is true.
            if (!string.IsNullOrEmpty(this.Component.TelephoneNumber) && this.Component.IsNumberSpanVisible)
            {
                var tbtelnoSpan = new TagBuilder("span");
                if (!string.IsNullOrEmpty(this.Component.CssTelephoneNumberSpan))
                {
                    tbtelnoSpan.AddCssClass(this.Component.CssTelephoneNumberSpan);
                }
                tbtelnoSpan.InnerHtml.Append(this.Component.TelephoneNumber);
                tagBuilderAnchor.InnerHtml.AppendHtml(tbtelnoSpan);
            }

            return tagBuilderAnchor;
        }

        private IHtmlContent CreateImageTag()
        {
            var tagBuilderImage = new TagBuilder("img");
            tagBuilderImage.TagRenderMode = TagRenderMode.SelfClosing;

            if (string.IsNullOrEmpty(this.Component.AlternateText))
            {
                this.Component.AlternateText = this.Component.Title;
            }

            if (!string.IsNullOrEmpty(this.Component.ImageUrl))
            {
                tagBuilderImage.MergeAttribute("src", this.Component.ImageUrl);
            }

            tagBuilderImage.MergeAttribute("alt", this.Component.AlternateText);

            if (!string.IsNullOrEmpty(this.Component.CssClassImage))
            {
                tagBuilderImage.AddCssClass(this.Component.CssClassImage);
            }

            return tagBuilderImage;
        }
    }
}
