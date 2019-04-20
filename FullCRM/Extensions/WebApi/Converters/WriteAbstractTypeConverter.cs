using FullCRM.Extensions.WebApi.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Converters
{

    public class WriteAbstractTypeConverter : JsonConverter
    {
        private readonly Type[] _implementedTypes;

        public WriteAbstractTypeConverter(params Type[] implementedTypes)
        {
            _implementedTypes = implementedTypes;
        }

        public override bool CanConvert(Type objectType)
        {
            return _implementedTypes.Contains(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject result = new JObject();

            result["$type"] = value.GetType().Name;

            var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                            .Where(x => x.CanRead)
                                            .Where(x => !x.IsDefined(typeof(JsonIgnoreAttribute)));

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(value);

                var name = (property.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? property.Name).ToLowerFirstSymbol();

                if (propertyValue != null)
                {
                    result[name.ToLowerFirstSymbol()] = JToken.FromObject(propertyValue, serializer);
                }
                else if (serializer.NullValueHandling == NullValueHandling.Include)
                {
                    result[name] = null;
                }
            }

            result.WriteTo(writer);
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    
}
