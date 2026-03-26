namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    /// <summary>
    /// ASP.NET Core model binder for WeekYearWithFormat.
    /// Replaces the legacy DefaultModelBinder-based binder from System.Web.Mvc.
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

            var weekPropertyName = string.IsNullOrEmpty(modelName) ? "Week" : modelName + ".Week";
            var yearPropertyName = string.IsNullOrEmpty(modelName) ? "Year" : modelName + ".Year";
            var formatPropertyName = string.IsNullOrEmpty(modelName) ? "Format" : modelName + ".Format";

            var weekValue = bindingContext.ValueProvider.GetValue(weekPropertyName);
            var yearValue = bindingContext.ValueProvider.GetValue(yearPropertyName);
            var formatValue = bindingContext.ValueProvider.GetValue(formatPropertyName);

            var format = formatValue.FirstValue ?? "English";

            int? week = null;
            int? year = null;

            if (weekValue != ValueProviderResult.None && !string.IsNullOrEmpty(weekValue.FirstValue))
            {
                if (int.TryParse(weekValue.FirstValue, NumberStyles.Any, CultureInfo.InvariantCulture, out int parsedWeek))
                {
                    week = parsedWeek;
                }
            }

            if (yearValue != ValueProviderResult.None && !string.IsNullOrEmpty(yearValue.FirstValue))
            {
                if (int.TryParse(yearValue.FirstValue, NumberStyles.Any, CultureInfo.InvariantCulture, out int parsedYear))
                {
                    year = parsedYear;
                }
            }

            var result = new WeekYearWithFormat(week, year, format);

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
