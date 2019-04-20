using FullCRM.Database.Sql.Filtering.Abstract;
using FullCRM.Extensions.WebApi.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Converters
{
    public class ExpressionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var result = typeof(Expression).IsAssignableFrom(objectType);
            return result;
            //objectType.GenericTypeArguments
            //return typeof(Filter).IsAssignableFrom();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return objectType.GetLambdaProperty(reader.Value.ToString());
            }
            return null;
        }
    }
}
