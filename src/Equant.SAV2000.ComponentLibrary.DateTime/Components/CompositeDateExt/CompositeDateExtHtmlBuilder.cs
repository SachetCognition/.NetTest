namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDateExt
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CompositeDateExtHtmlBuilder : HtmlBuilderBase<CompositeDateExtComponent>
    {
        public CompositeDateExtHtmlBuilder(CompositeDateExtComponent component)
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
            mainDiv.Attributes["id"] = this.Component.Id.AppendWithBuilder("MainDiv");
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

            // Date type dropdown
            sb.Append(CreateDateTypeDropDown());

            // Date from container
            sb.Append(CreateDateContainer(this.Component.DateFromContainerId, "date-from-container",
                this.Component.Id.AppendWithBuilder("DateFrom"), this.Component.ViewModel.DateFrom));

            // Date to container
            sb.Append(CreateDateContainer(this.Component.DateToContainerId, "date-to-container",
                this.Component.Id.AppendWithBuilder("DateTo"), this.Component.ViewModel.DateTo));

            // Week from container
            sb.Append(CreateWeekContainer(this.Component.WeekFromContainerId, "week-from-container",
                this.Component.Id.AppendWithBuilder("WeekFrom"), this.Component.ViewModel.WeekFrom,
                this.Component.Id.AppendWithBuilder("YearFrom"), this.Component.ViewModel.YearFrom));

            // Week to container
            sb.Append(CreateWeekContainer(this.Component.WeekToContainerId, "week-to-container",
                this.Component.Id.AppendWithBuilder("WeekTo"), this.Component.ViewModel.WeekTo,
                this.Component.Id.AppendWithBuilder("YearTo"), this.Component.ViewModel.YearTo));

            // Model container
            sb.Append(CreateModelContainer());

            // Depth container
            sb.Append(CreateDepthContainer());

            mainDiv.InnerHtml.AppendHtml(sb.ToString());

            return mainDiv;
        }

        private string CreateDateTypeDropDown()
        {
            var tag = new TagBuilder("select");
            tag.Attributes["id"] = this.Component.DateTypeDropDownId;
            tag.Attributes["name"] = this.Component.DateTypeDropDownId;
            if (!string.IsNullOrEmpty(this.Component.CssClassDropDown))
            {
                tag.AddCssClass(this.Component.CssClassDropDown);
            }

            var sb = new StringBuilder();
            var selectedValue = ((int)this.Component.ViewModel.SelectedDateType).ToString(CultureInfo.InvariantCulture);
            var items = this.Component.AvailableDateTypes != null && this.Component.AvailableDateTypes.Count > 0
                ? this.Component.DateTypeItems.Where(i => this.Component.AvailableDateTypes.Contains((EnumDateTypes)int.Parse(i.Value, CultureInfo.InvariantCulture))).ToList()
                : this.Component.DateTypeItems;

            foreach (var item in items)
            {
                var option = new TagBuilder("option");
                option.Attributes["value"] = item.Value;
                if (item.Value == selectedValue)
                {
                    option.Attributes["selected"] = "selected";
                }
                option.InnerHtml.Append(item.Text);
                sb.Append(GetHtml(option));
            }

            tag.InnerHtml.AppendHtml(sb.ToString());
            return GetHtml(tag);
        }

        private string CreateDateContainer(string containerId, string cssClass, string inputId, DateTimeControl.DateTimeWithFormat dateValue)
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = containerId;
            div.AddCssClass(cssClass);

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = inputId;
            input.Attributes["name"] = inputId;
            input.AddCssClass("dateTextbox");
            if (!string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                input.AddCssClass(this.Component.CssClassDateInput);
            }
            if (dateValue != null)
            {
                input.Attributes["value"] = dateValue.DateText ?? string.Empty;
            }
            input.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(input));
            return GetHtml(div);
        }

        private string CreateWeekContainer(string containerId, string cssClass, string weekInputId, string weekValue, string yearInputId, string yearValue)
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = containerId;
            div.AddCssClass(cssClass);

            var weekInput = new TagBuilder("input");
            weekInput.Attributes["type"] = "text";
            weekInput.Attributes["id"] = weekInputId;
            weekInput.Attributes["name"] = weekInputId;
            weekInput.AddCssClass("weekTextbox");
            weekInput.Attributes["value"] = weekValue ?? string.Empty;
            weekInput.TagRenderMode = TagRenderMode.SelfClosing;

            var yearInput = new TagBuilder("input");
            yearInput.Attributes["type"] = "text";
            yearInput.Attributes["id"] = yearInputId;
            yearInput.Attributes["name"] = yearInputId;
            yearInput.AddCssClass("yearTextbox");
            yearInput.Attributes["value"] = yearValue ?? string.Empty;
            yearInput.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(weekInput));
            div.InnerHtml.AppendHtml(GetHtml(yearInput));
            return GetHtml(div);
        }

        private string CreateModelContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.ModelContainerId;
            div.AddCssClass("model-container");

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = this.Component.Id.AppendWithBuilder("ModelValue");
            input.Attributes["name"] = this.Component.Id.AppendWithBuilder("ModelValue");
            input.AddCssClass("modelTextbox");
            input.Attributes["value"] = this.Component.ViewModel.ModelValue ?? string.Empty;
            input.Attributes["maxlength"] = "5";
            input.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(input));
            return GetHtml(div);
        }

        private string CreateDepthContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.DepthContainerId;
            div.AddCssClass("depth-container");

            // Depth Add
            var addLabel = new TagBuilder("label");
            addLabel.InnerHtml.Append(ApplicationStrings.DepthAdd ?? "Add");
            div.InnerHtml.AppendHtml(GetHtml(addLabel));

            var addInput = new TagBuilder("input");
            addInput.Attributes["type"] = "text";
            addInput.Attributes["id"] = this.Component.DepthAddId;
            addInput.Attributes["name"] = this.Component.DepthAddId;
            addInput.AddCssClass("depthTextbox");
            addInput.Attributes["value"] = this.Component.ViewModel.DepthAdd?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            addInput.TagRenderMode = TagRenderMode.SelfClosing;
            div.InnerHtml.AppendHtml(GetHtml(addInput));

            // Depth Subtract
            var subLabel = new TagBuilder("label");
            subLabel.InnerHtml.Append(ApplicationStrings.DepthSubtract ?? "Subtract");
            div.InnerHtml.AppendHtml(GetHtml(subLabel));

            var subInput = new TagBuilder("input");
            subInput.Attributes["type"] = "text";
            subInput.Attributes["id"] = this.Component.DepthSubtractId;
            subInput.Attributes["name"] = this.Component.DepthSubtractId;
            subInput.AddCssClass("depthTextbox");
            subInput.Attributes["value"] = this.Component.ViewModel.DepthSubtract?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            subInput.TagRenderMode = TagRenderMode.SelfClosing;
            div.InnerHtml.AppendHtml(GetHtml(subInput));

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
