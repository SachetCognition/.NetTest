namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateTimeControl
{
    using System;
    using Equant.SAV2000.ComponentLibrary.Common.Resources;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.CustomLabel;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.ImageToolTip;
    using Equant.SAV2000.ComponentLibrary.MVC.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DateTimeBuilder : ComponentBuilderBase<DateTimeComponent, DateTimeBuilder>
    {
        private readonly CustomLabelBuilder customLabelBuilder;
        private readonly ImageToolTipBuilder informationIconBuilder;

        public DateTimeBuilder(DateTimeComponent component, ModelMetadata modelMetadata)
            : base(component, modelMetadata)
        {
            this.customLabelBuilder = new CustomLabelBuilder(this.Component.CustomLabel, modelMetadata);
            this.informationIconBuilder = new ImageToolTipBuilder(this.Component.InformationIcon, modelMetadata)
                .ImageUrl("/Images/picto-information.png").CssClassImage("img15").PersistanceMode(PersistanceMode.Click)
                .Text(ApplicationStrings.DateImageToolTip).Title(ApplicationStrings.TIP000024)
                .AlternateText(ApplicationStrings.TIP000024).Css("fortooltipclick").CssClassSpan("help").CssClassInnerSpan("tooltip");
        }

        public DateTimeBuilder CustomLabel(Action<CustomLabelBuilder> setup)
        {
            if (setup == null) return this;
            setup(this.customLabelBuilder);
            return this;
        }

        public DateTimeBuilder InformationIcon(Action<ImageToolTipBuilder> setup)
        {
            if (setup == null) return this;
            setup(this.informationIconBuilder);
            return this;
        }

        public DateTimeBuilder Value(DateTimeWithFormat value) { this.Component.Value = value; return this; }
        public DateTimeBuilder DisplayTime(bool value) { this.Component.DisplayTime = value; return this; }
        public DateTimeBuilder DisplayEraseButton(bool value) { this.Component.DisplayEraseButton = value; return this; }
        public DateTimeBuilder DisplayInformationIcon(bool value) { this.Component.DisplayInformationIcon = value; return this; }
        public DateTimeBuilder DisplayCurrentDateSelector(bool value) { this.Component.DisplayCurrentDateSelector = value; return this; }
        public DateTimeBuilder StartFromCurrentDate(bool value) { this.Component.StartFromCurrentDate = value; return this; }
        public DateTimeBuilder AssociatedDateHtmlId(string value) { this.Component.AssociatedDateHtmlId = value; return this; }
        public DateTimeBuilder OnDateChange(string value) { this.Component.OnDateChange = value; return this; }
        public DateTimeBuilder CssMainDiv(string value) { this.Component.CssMainDiv = value; return this; }
        public DateTimeBuilder CssClassDateDiv(string value) { this.Component.CssClassDateDiv = value; return this; }
        public DateTimeBuilder CssClassDateInput(string value) { this.Component.CssClassDateInput = value; return this; }
    }
}
