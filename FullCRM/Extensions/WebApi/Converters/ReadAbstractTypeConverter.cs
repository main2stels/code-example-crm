using FullCRM.Database.Sql.Attributes;
using FullCRM.Database.Sql.Filtering.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCRM.Extensions.WebApi.Converters
{
    public class ReadAbstractTypeConverter : JsonConverter
    {
        private readonly Type[] _implementedTypes;
        private readonly Type _abstractType;

        private Type Type;

        public ReadAbstractTypeConverter(Type type)
        {
            Type = type;
            var abstractType = Type.GetType($"{typeof(Filter<>).FullName}[{type.FullName}]");
            abstractType.GenericTypeArguments[0] = type;
            var implementedTypes = typeof(Filter<>).GetCustomAttributes(typeof(KnownTypesAttribute), false).Cast<KnownTypesAttribute>().FirstOrDefault().Types;
            var arguments = implementedTypes[2].GetGenericArguments();

            List<Type> implementedTypesList = new List<Type>();
            foreach (var typ in implementedTypes)
            {
                var argumentsCount = typ.GetGenericArguments().Count();
                if (argumentsCount == 1)
                    implementedTypesList.Add(Type.GetType($"{typ.FullName}[{type.FullName}]"));
                else if (argumentsCount == 2)
                    implementedTypesList.Add(Type.GetType($"{typ.FullName}[{type.FullName},{typeof(string).FullName}]"));
            }

            _implementedTypes = implementedTypesList.ToArray();
            _abstractType = abstractType;
        }

        public override bool CanConvert(Type objectType)
        {
            var result = (objectType == _abstractType);
            return result;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject;
            try
            {
                jObject = (JObject)JToken.Load(reader);
            }
            catch (System.InvalidCastException)
            {
                return null;
            }
            

            JToken typeToken;

            if (jObject.TryGetValue("modelType", out typeToken))
            {
                var typeName = typeToken.Value<string>();

                var type = _implementedTypes.FirstOrDefault(x => x.Name.StartsWith(typeName));

                if(type.GetGenericArguments().Count()== 2)
                {
                    var implementedTypes = typeof(Filter<>).GetCustomAttributes(typeof(KnownTypesAttribute), false).Cast<KnownTypesAttribute>().FirstOrDefault().Types;
                    var arguments = implementedTypes[2].GetGenericArguments();

                    jObject.TryGetValue("field", out typeToken);
                    var propertyTypeName = typeToken.Value<string>();
                    propertyTypeName = propertyTypeName.Substring(0, 1).ToUpper() + propertyTypeName.Remove(0, 1);
                    var propertyType = Type.GetProperty(propertyTypeName).PropertyType;
                    List<Type> implementedTypesList = new List<Type>();
                    foreach (var typ in implementedTypes)
                    {
                        var argumentsCount = typ.GetGenericArguments().Count();
                        if (argumentsCount == 1)
                            implementedTypesList.Add(Type.GetType($"{typ.FullName}[{Type.FullName}]"));
                        else if (argumentsCount == 2)
                            implementedTypesList.Add(Type.GetType($"{typ.FullName}[{Type.FullName},{propertyType.FullName}]"));
                    }
                    type = implementedTypesList.FirstOrDefault(x => x.Name.StartsWith(typeName));
                }

                if (type != null)
                {
                    return jObject.ToObject(type, serializer);
                }
            }
            else
            {
                throw new NotImplementedException("Для моделей без $type нет реализации");
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }
    }
}
