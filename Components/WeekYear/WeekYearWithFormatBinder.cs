namespace Equant.SAV2000.ComponentLibrary.MVC.Components.WeekYear
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class WeekYearWithFormatBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}
