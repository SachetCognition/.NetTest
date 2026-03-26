namespace Equant.SAV2000.ComponentLibrary.MVC.Components.CompositeDate
{
    using System.Collections.Generic;
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

    public class CompositeDateHtmlBuilder : HtmlBuilderBase<CompositeDateComponent>
    {
        public CompositeDateHtmlBuilder(CompositeDateComponent component)
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

            // Date containers
            sb.Append(CreateDateFromContainer());
            sb.Append(CreateDateToContainer());

            // Week containers
            sb.Append(CreateWeekFromContainer());
            sb.Append(CreateWeekToContainer());

            // Model container
            sb.Append(CreateModelContainer());

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
            var items = this.Component.DateTypeItems;
            var selectedValue = ((int)this.Component.ViewModel.SelectedDateType).ToString(CultureInfo.InvariantCulture);

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

        private string CreateDateFromContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.DateFromContainerId;
            div.AddCssClass("date-from-container");

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = this.Component.DateFromId;
            input.Attributes["name"] = this.Component.DateFromId;
            if (!string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                input.AddCssClass(this.Component.CssClassDateInput);
            }
            input.AddCssClass("dateTextbox");
            if (this.Component.ViewModel.DateFrom != null)
            {
                input.Attributes["value"] = this.Component.ViewModel.DateFrom.DateText ?? string.Empty;
            }
            input.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(input));
            return GetHtml(div);
        }

        private string CreateDateToContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.DateToContainerId;
            div.AddCssClass("date-to-container");

            var input = new TagBuilder("input");
            input.Attributes["type"] = "text";
            input.Attributes["id"] = this.Component.DateToId;
            input.Attributes["name"] = this.Component.DateToId;
            if (!string.IsNullOrEmpty(this.Component.CssClassDateInput))
            {
                input.AddCssClass(this.Component.CssClassDateInput);
            }
            input.AddCssClass("dateTextbox");
            if (this.Component.ViewModel.DateTo != null)
            {
                input.Attributes["value"] = this.Component.ViewModel.DateTo.DateText ?? string.Empty;
            }
            input.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(input));
            return GetHtml(div);
        }

        private string CreateWeekFromContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.WeekFromContainerId;
            div.AddCssClass("week-from-container");

            var weekInput = new TagBuilder("input");
            weekInput.Attributes["type"] = "text";
            weekInput.Attributes["id"] = this.Component.WeekFromId;
            weekInput.Attributes["name"] = this.Component.WeekFromId;
            weekInput.AddCssClass("weekTextbox");
            weekInput.Attributes["value"] = this.Component.ViewModel.WeekFrom ?? string.Empty;
            weekInput.TagRenderMode = TagRenderMode.SelfClosing;

            var yearInput = new TagBuilder("input");
            yearInput.Attributes["type"] = "text";
            yearInput.Attributes["id"] = this.Component.YearFromId;
            yearInput.Attributes["name"] = this.Component.YearFromId;
            yearInput.AddCssClass("yearTextbox");
            yearInput.Attributes["value"] = this.Component.ViewModel.YearFrom ?? string.Empty;
            yearInput.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(weekInput));
            div.InnerHtml.AppendHtml(GetHtml(yearInput));
            return GetHtml(div);
        }

        private string CreateWeekToContainer()
        {
            var div = new TagBuilder("div");
            div.Attributes["id"] = this.Component.WeekToContainerId;
            div.AddCssClass("week-to-container");

            var weekInput = new TagBuilder("input");
            weekInput.Attributes["type"] = "text";
            weekInput.Attributes["id"] = this.Component.WeekToId;
            weekInput.Attributes["name"] = this.Component.WeekToId;
            weekInput.AddCssClass("weekTextbox");
            weekInput.Attributes["value"] = this.Component.ViewModel.WeekTo ?? string.Empty;
            weekInput.TagRenderMode = TagRenderMode.SelfClosing;

            var yearInput = new TagBuilder("input");
            yearInput.Attributes["type"] = "text";
            yearInput.Attributes["id"] = this.Component.YearToId;
            yearInput.Attributes["name"] = this.Component.YearToId;
            yearInput.AddCssClass("yearTextbox");
            yearInput.Attributes["value"] = this.Component.ViewModel.YearTo ?? string.Empty;
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
            input.Attributes["id"] = this.Component.ModelValueId;
            input.Attributes["name"] = this.Component.ModelValueId;
            input.AddCssClass("modelTextbox");
            input.Attributes["value"] = this.Component.ViewModel.ModelValue ?? string.Empty;
            input.Attributes["maxlength"] = "5";
            input.TagRenderMode = TagRenderMode.SelfClosing;

            div.InnerHtml.AppendHtml(GetHtml(input));
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
