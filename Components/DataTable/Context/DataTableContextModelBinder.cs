namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Newtonsoft.Json;

    public class DataTableContextModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var contextName = bindingContext.ModelName + ".Context";
            var valueProviderResult = bindingContext.ValueProvider.GetValue(contextName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                var value = valueProviderResult.FirstValue;
                if (value != null)
                {
                    var retrievedValue = JsonConvert.DeserializeObject<DataTableContext>(value);
                    bindingContext.Result = ModelBindingResult.Success(retrievedValue);
                    return Task.CompletedTask;
                }
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}
