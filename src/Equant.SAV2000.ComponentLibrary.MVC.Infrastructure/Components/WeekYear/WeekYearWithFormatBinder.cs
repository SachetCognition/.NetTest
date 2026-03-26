namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Equant.SAV2000.ComponentLibrary.MVC.Helpers;
    using Equant.SAV2000.ComponentLibrary.MVC.Extensions;

    /// <summary>
    /// ASP.NET Core model binder for WeekYearWithFormat.
    /// Migrated from IModelBinder (System.Web.Mvc) to IModelBinder (Microsoft.AspNetCore.Mvc.ModelBinding).
    /// Parses week/year form data from the request value provider.
    /// </summary>
    public class WeekYearWithFormatBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var weekPropertyName = modelName.AppendWithBuilder(".", "WeekText");
            var yearPropertyName = modelName.AppendWithBuilder(".", "YearText");
            var formatPropertyName = modelName.AppendWithBuilder(".", "Format");
            var timeOffsetPropertyName = modelName.AppendWithBuilder(".", "TimeOffset");
            var utcPropertyName = modelName.AppendWithBuilder(".", "IsUtcMode");

            var weekResult = bindingContext.ValueProvider.GetValue(weekPropertyName);
            var yearResult = bindingContext.ValueProvider.GetValue(yearPropertyName);
            var formatResult = bindingContext.ValueProvider.GetValue(formatPropertyName);
            var timeOffsetResult = bindingContext.ValueProvider.GetValue(timeOffsetPropertyName);
            var utcResult = bindingContext.ValueProvider.GetValue(utcPropertyName);

            var weekYear = new WeekYearWithFormat();

            if (weekResult != ValueProviderResult.None)
                weekYear.WeekText = weekResult.FirstValue;

            if (yearResult != ValueProviderResult.None)
                weekYear.YearText = yearResult.FirstValue;

            if (formatResult != ValueProviderResult.None)
            {
                if (Enum.TryParse<WeekFormat>(formatResult.FirstValue, true, out var format))
                {
                    weekYear.Format = format;
                }
            }

            if (timeOffsetResult != ValueProviderResult.None)
            {
                if (double.TryParse(timeOffsetResult.FirstValue, out var offset))
                {
                    weekYear.TimeOffset = offset;
                }
            }

            if (utcResult != ValueProviderResult.None)
            {
                if (bool.TryParse(utcResult.FirstValue, out var isUtc))
                {
                    weekYear.IsUtcMode = isUtc;
                }
            }

            // Parse the week and year to get the date
            weekYear.Date = WeekHelper.ParseWeekAndYear(weekYear);

            bindingContext.Result = ModelBindingResult.Success(weekYear);
            return Task.CompletedTask;
        }
    }
}
