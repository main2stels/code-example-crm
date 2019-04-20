using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using FullCRM.Extensions.WebApi.Converters;
using FullCRM.Database.Sql.Filtering.Abstract;
using FullCRM.Database.Sql.Filtering;
using FullCRM.Database.Sql.Attributes;

namespace FullCRM.Database
{
    /// <summary>
    /// Объект привязки модели  к строковому json-значению из запроса для контроллера Mvc.
    /// Примечание: актуально для GET-запроса
    /// </summary>
    public class JsonObjectModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);
                var type = bindingContext.ModelType.GenericTypeArguments.FirstOrDefault();
                
                var converters = new JsonConverter[] { new ReadAbstractTypeConverter(type) };

                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None,
                    Converters = converters
                };
                // Attempt to convert the input value
                var valueAsString = valueProviderResult.FirstValue;
                var result = JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType, settings);
                if (result != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }
            return Task.CompletedTask;
        }
    }
}
