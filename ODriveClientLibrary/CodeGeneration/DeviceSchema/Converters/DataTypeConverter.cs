namespace ODrive.CodeGeneration.DeviceSchema.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    internal class DataTypeConverter : StringEnumConverter
    {
        public static readonly Lazy<Dictionary<string, DataType>> StringToDataTypeMap =
            new Lazy<Dictionary<string, DataType>>(() =>
            {
                return Enum.GetValues(typeof(DataType))
                    .Cast<DataType>()
                    .ToDictionary(key => GetAttribute<WirePropertyType>(key).WireString, val => val);
            });

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var dataTypeString = (string)reader.Value;
            var dataTypeEnum = StringToDataTypeMap.Value[dataTypeString];
            return dataTypeEnum;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static T GetAttribute<T>(Enum enumValue) where T : Attribute
        {
            T attribute;

            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                attribute = (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
                return attribute;
            }

            return null;
        }
    }
}
