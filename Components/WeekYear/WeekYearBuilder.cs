namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;

    public class WeekYearBuilder : ComponentBuilderBase<WeekYearComponent, WeekYearBuilder>
    {
        public WeekYearBuilder(WeekYearComponent component, ModelMetadata? modelMetadata)
            : base(component, modelMetadata)
        {
        }

        public WeekYearBuilder Value(WeekYearWithFormat value)
        {
            this.Component.Value = value;
            return this;
        }

        public WeekYearBuilder Name(string name)
        {
            this.Component.Name = name;
            return this;
        }

        public WeekYearBuilder Disabled(bool disabled)
        {
            this.Component.Disabled = disabled;
            return this;
        }
    }
}
