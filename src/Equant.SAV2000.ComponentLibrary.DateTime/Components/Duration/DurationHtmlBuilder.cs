namespace Equant.SAV2000.ComponentLibrary.MVC.Components.Duration
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DurationHtmlBuilder : HtmlBuilderBase<DurationComponent>
    {
        public DurationHtmlBuilder(DurationComponent component)
        {
            this.Component = component;
        }

        public override IHtmlContent Build()
        {
            if (!this.Component.IsVisible)
            {
                return HtmlString.Empty;
            }

            var mainDiv = new TagBuilder("div");
            mainDiv.Attributes["id"] = this.Component.MainDivId;
            if (!string.IsNullOrEmpty(this.Component.CssMainDiv))
            {
                mainDiv.AddCssClass(this.Component.CssMainDiv);
            }

            var sb = new StringBuilder();

            // Custom label
            if (!string.IsNullOrEmpty(this.Component.CustomLabel.Text))
            {
                sb.Append(this.Component.CustomLabel.ToHtmlString());
            }

            // Hours
            sb.Append(CreateTimeInput(this.Component.HoursInputId, this.Component.HoursInputName,
                this.Component.Value.Hours, ApplicationStrings.DurationHours ?? "H"));

            sb.Append(CreateSeparator(":"));

            // Minutes
            sb.Append(CreateTimeInput(this.Component.MinutesInputId, this.Component.MinutesInputName,
                this.Component.Value.Minutes, ApplicationStrings.DurationMinutes ?? "M"));

            // Seconds
            if (this.Component.DisplaySeconds)
            {
                sb.Append(CreateSeparator(":"));
                sb.Append(CreateTimeInput(this.Component.SecondsInputId, this.Component.SecondsInputName,
                    this.Component.Value.Seconds, ApplicationStrings.DurationSeconds ?? "S"));
            }

            // Milliseconds
            if (this.Component.DisplayMilliseconds)
            {
                sb.Append(CreateSeparator("."));
                sb.Append(CreateTimeInput(this.Component.MillisecondsInputId, this.Component.MillisecondsInputName,
                    this.Component.Value.Milliseconds, ApplicationStrings.DurationMilliseconds ?? "MS"));
            }

            mainDiv.InnerHtml.AppendHtml(sb.ToString());

            return mainDiv;
        }

        private string CreateTimeInput(string id, string name, int value, string label)
        {
            var container = new StringBuilder();

            var labelTag = new TagBuilder("label");
            labelTag.Attributes["for"] = id;
            labelTag.InnerHtml.Append(label);
            container.Append(GetHtml(labelTag));

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = id;
            input.Attributes["name"] = name;
            input.AddCssClass("durationTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassInput))
            {
                input.AddCssClass(this.Component.CssClassInput);
            }
            input.Attributes["value"] = value.ToString(CultureInfo.InvariantCulture);

            if (!this.Component.IsUpdatable)
            {
                input.AddCssClass("readonly");
                input.Attributes["readonly"] = "readonly";
            }

            input.TagRenderMode = TagRenderMode.SelfClosing;
            container.Append(GetHtml(input));

            return container.ToString();
        }

        private string CreateSeparator(string text)
        {
            var span = new TagBuilder("span");
            span.AddCssClass("duration-separator");
            span.InnerHtml.Append(text);
            return GetHtml(span);
        }

        private static string GetHtml(TagBuilder tagBuilder)
        {
            using (var writer = new StringWriter())
            {
                tagBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}
