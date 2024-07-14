using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.ModelBinders;
public class EnumerableModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var receivedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).ToString();
        if (string.IsNullOrEmpty(receivedValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        var typeParameter = bindingContext.ModelType.GenericTypeArguments[0];
        var typeParameterConverter = TypeDescriptor.GetConverter(typeParameter);
        var rawValues = receivedValue.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var parsedValues = rawValues.Select(typeParameterConverter.ConvertFromString).ToArray();

        var valuesArray = Array.CreateInstance(typeParameter, parsedValues.Length);
        parsedValues.CopyTo(valuesArray, 0);

        bindingContext.Model = valuesArray;
        bindingContext.Result = ModelBindingResult.Success(valuesArray);
        return Task.CompletedTask;
    }
}
