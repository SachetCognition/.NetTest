namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Newtonsoft.Json;

    /// <summary>
    /// ASP.NET Core model binder for DataTableContext.
    /// Migrated from IModelBinder (System.Web.Mvc) to IModelBinder (Microsoft.AspNetCore.Mvc.ModelBinding).
    /// Deserializes a JSON context string from form data into a DataTableContext object.
    /// </summary>
    public class DataTableContextModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

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

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}
