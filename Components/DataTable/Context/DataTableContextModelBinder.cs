using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context;

public class DataTableContextModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            return Task.CompletedTask;
        }

        var contextName = bindingContext.ModelName + ".Context";
        var valueProviderResult = bindingContext.ValueProvider.GetValue(contextName);
        
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        var retrievedValue = JsonConvert.DeserializeObject<DataTableContext>(value);
        bindingContext.Result = ModelBindingResult.Success(retrievedValue);
        
        return Task.CompletedTask;
    }
}
