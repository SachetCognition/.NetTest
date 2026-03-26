namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDuration
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.Encodings.Web;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class DateDurationHtmlBuilder : HtmlBuilderBase<DateDurationComponent>
    {
        public DateDurationHtmlBuilder(DateDurationComponent component)
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

            // Date container
            sb.Append(CreateDateContainer());

            // Duration container
            sb.Append(CreateDurationContainer());

            mainDiv.InnerHtml.AppendHtml(sb.ToString());

            return mainDiv;
        }

        private string CreateDateContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.DateContainerId;
            div.AddCssClass("date-container");

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = this.Component.Id.AppendWithBuilder("TxtDate");
            input.Attributes["name"] = this.Component.Id.AppendWithBuilder("TxtDate");
            input.AddCssClass("dateTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                input.AddCssClass(this.Component.CssClassDateInput);
            }
            if (this.Component.Value?.DateValue != null)
            {
                input.Attributes["value"] = this.Component.Value.DateValue.DateText ?? string.Empty;
            }
            input.TagRenderMode = TagRenderMode.SelfClosing;

            // Calendar icon
            var calIcon = new TagBuilder("span");
            calIcon.AddCssClass("calendar-icon");
            calIcon.Attributes["id"] = this.Component.Id.AppendWithBuilder("CalendarIcon");

            div.InnerHtml.AppendHtml(GetHtml(input));
            div.InnerHtml.AppendHtml(GetHtml(calIcon));
            return GetHtml(div);
        }

        private string CreateDurationContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.DurationContainerId;
            div.AddCssClass("duration-container");

            // Days input
            var daysLabel = new TagBuilder("label");
            daysLabel.InnerHtml.Append(ApplicationStrings.DurationDays ?? "Days");
            div.InnerHtml.AppendHtml(GetHtml(daysLabel));

            var daysInput = new TagBuilder("input");
            daysInput.Attributes["type"] = "text";
            daysInput.Attributes["id"] = this.Component.DurationDaysId;
            daysInput.Attributes["name"] = this.Component.DurationDaysId;
            daysInput.AddCssClass("durationTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassDurationInput))
            {
                daysInput.AddCssClass(this.Component.CssClassDurationInput);
            }
            daysInput.Attributes["value"] = this.Component.Value?.DurationDays?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            daysInput.TagRenderMode = TagRenderMode.SelfClosing;
            div.InnerHtml.AppendHtml(GetHtml(daysInput));

            // Hours input
            var hoursLabel = new TagBuilder("label");
            hoursLabel.InnerHtml.Append(ApplicationStrings.DurationHours ?? "Hours");
            div.InnerHtml.AppendHtml(GetHtml(hoursLabel));

            var hoursInput = new TagBuilder("input");
            hoursInput.Attributes["type"] = "text";
            hoursInput.Attributes["id"] = this.Component.DurationHoursId;
            hoursInput.Attributes["name"] = this.Component.DurationHoursId;
            hoursInput.AddCssClass("durationTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassDurationInput))
            {
                hoursInput.AddCssClass(this.Component.CssClassDurationInput);
            }
            hoursInput.Attributes["value"] = this.Component.Value?.DurationHours?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            hoursInput.TagRenderMode = TagRenderMode.SelfClosing;
            div.InnerHtml.AppendHtml(GetHtml(hoursInput));

            // Minutes input
            var minutesLabel = new TagBuilder("label");
            minutesLabel.InnerHtml.Append(ApplicationStrings.DurationMinutes ?? "Minutes");
            div.InnerHtml.AppendHtml(GetHtml(minutesLabel));

            var minutesInput = new TagBuilder("input");
            minutesInput.Attributes["type"] = "text";
            minutesInput.Attributes["id"] = this.Component.DurationMinutesId;
            minutesInput.Attributes["name"] = this.Component.DurationMinutesId;
            minutesInput.AddCssClass("durationTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassDurationInput))
            {
                minutesInput.AddCssClass(this.Component.CssClassDurationInput);
            }
            minutesInput.Attributes["value"] = this.Component.Value?.DurationMinutes?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            minutesInput.TagRenderMode = TagRenderMode.SelfClosing;
            div.InnerHtml.AppendHtml(GetHtml(minutesInput));

            return GetHtml(div);
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
