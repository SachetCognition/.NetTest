namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Newtonsoft.Json;

    /// <summary>
    /// The data table context model binder.
    /// </summary>
    public class DataTableContextModelBinder : IModelBinder
    {
        /// <summary>
        /// The bind model.
        /// </summary>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext != null)
            {
                var contextName = bindingContext.ModelName + ".Context";
                var value = bindingContext.ValueProvider.GetValue(contextName);
                if (value != ValueProviderResult.None)
                {
                    var attemptedValue = value.FirstValue;
                    if (attemptedValue != null)
                    {
                        var retrievedValue = JsonConvert.DeserializeObject<DataTableContext>(attemptedValue);
                        bindingContext.Result = ModelBindingResult.Success(retrievedValue);
                        return Task.CompletedTask;
                    }
                }     
            }

            return Task.CompletedTask;
        }
    }
}
