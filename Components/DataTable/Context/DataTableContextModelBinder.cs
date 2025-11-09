namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable.Context
{
    using System;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    /// <summary>
    /// The data table context model binder.
    /// </summary>
    public class DataTableContextModelBinder : IModelBinder
    {
        /// <summary>
        /// The bind model.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext != null)
            {
                var contextName = bindingContext.ModelName + ".Context";
                var value = bindingContext.ValueProvider.GetValue(contextName);
                if (value != null)
                {
                    if (value.AttemptedValue != null)
                    {
                        var retrievedValue = JsonConvert.DeserializeObject<DataTableContext>(value.AttemptedValue);
                        bindingContext.ModelState.Remove(contextName);
                        bindingContext.ModelState.Add(contextName, new ModelState());
                        bindingContext.ModelState.SetModelValue(contextName, new ValueProviderResult(retrievedValue, value.AttemptedValue, null));
                        return retrievedValue;
                    }
                }     
            }

            return null;
        }
    }
}
