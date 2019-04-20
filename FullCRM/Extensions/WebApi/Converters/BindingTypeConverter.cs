using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Converters
{
    public class BindingTypeConverter : JsonConverter
    {
        private readonly Type _abstractType;
        private readonly Type _concreteType;

        public BindingTypeConverter(Type abstactType, Type concreteType)
        {
            _abstractType = abstactType;
            _concreteType = concreteType;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == _abstractType;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, _concreteType);
        }
    }
}
