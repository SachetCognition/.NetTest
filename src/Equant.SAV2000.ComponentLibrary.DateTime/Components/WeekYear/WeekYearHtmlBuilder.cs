namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
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

    public class WeekYearHtmlBuilder : HtmlBuilderBase<WeekYearComponent>
    {
        public WeekYearHtmlBuilder(WeekYearComponent component)
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

            // Week input
            sb.Append(CreateWeekInput());

            // Week label
            var weekLabel = new TagBuilder("span");
            weekLabel.AddCssClass("week-label");
            weekLabel.InnerHtml.Append(ApplicationStrings.WeekLabel ?? "W");
            sb.Append(GetHtml(weekLabel));

            // Year input
            sb.Append(CreateYearInput());

            // Hidden format field
            sb.Append(CreateHiddenFormat());

            // Validation span
            sb.Append(GetValidationSpan());

            mainDiv.InnerHtml.AppendHtml(sb.ToString());

            return mainDiv;
        }

        private string CreateWeekInput()
        {
            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "text";
            tag.Attributes["id"] = this.Component.WeekInputId;
            tag.Attributes["name"] = this.Component.WeekInputName;
            tag.AddCssClass("weekTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassWeekInput))
            {
                tag.AddCssClass(this.Component.CssClassWeekInput);
            }

            if (this.Component.Value != null && this.Component.Value.Value != null && this.Component.Value.Value.Week.HasValue)
            {
                tag.Attributes["value"] = this.Component.Value.Value.Week.Value.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                tag.Attributes["value"] = string.Empty;
            }

            tag.Attributes["maxlength"] = "2";

            if (!this.Component.IsUpdatable)
            {
                tag.AddCssClass("readonly");
                tag.Attributes["readonly"] = "readonly";
            }

            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return GetHtml(tag);
        }

        private string CreateYearInput()
        {
            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "text";
            tag.Attributes["id"] = this.Component.YearInputId;
            tag.Attributes["name"] = this.Component.YearInputName;
            tag.AddCssClass("yearTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassYearInput))
            {
                tag.AddCssClass(this.Component.CssClassYearInput);
            }

            if (this.Component.Value != null && this.Component.Value.Value != null && this.Component.Value.Value.Year.HasValue)
            {
                tag.Attributes["value"] = this.Component.Value.Value.Year.Value.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                tag.Attributes["value"] = string.Empty;
            }

            tag.Attributes["maxlength"] = "4";

            if (!this.Component.IsUpdatable)
            {
                tag.AddCssClass("readonly");
                tag.Attributes["readonly"] = "readonly";
            }

            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return GetHtml(tag);
        }

        private string CreateHiddenFormat()
        {
            var tag = new TagBuilder("input");
            tag.Attributes["type"] = "hidden";
            tag.Attributes["id"] = this.Component.Id.AppendWithBuilder("HdnFormat");
            tag.Attributes["name"] = this.Component.FormatHiddenName;
            tag.Attributes["value"] = this.Component.Value?.Format ?? "English";
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            return GetHtml(tag);
        }

        private string GetValidationSpan()
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("field-validation-valid");
            tag.Attributes["data-valmsg-for"] = this.Component.WeekInputName;
            tag.Attributes["data-valmsg-replace"] = "true";
            return GetHtml(tag);
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
