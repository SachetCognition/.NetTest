namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DateDurationControl
{
    using Equant.SAV2000.ComponentLibrary.MVC.Components.Api;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class DateDurationBuilder : ComponentBuilderBase<DateDurationComponent, DateDurationBuilder>
    {
        public DateDurationBuilder(DateDurationComponent component) : base(component) { }
        public DateDurationBuilder(DateDurationComponent component, ModelMetadata modelMetadata) : base(component, modelMetadata) { }

    }
}
