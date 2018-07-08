namespace ODrive.CodeGeneration.DeviceSchema.Converters
{
    using System;
    using Newtonsoft.Json;

    internal class AccessModeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            var isNullableType = objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(Nullable<>);
            Type type = isNullableType ? Nullable.GetUnderlyingType(objectType) : objectType;
            return type.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            AccessMode accessModeEnum = AccessMode.None;

            var accessModeString = (string)reader.Value;

            if (accessModeString.Contains("r"))
            {
                accessModeEnum |= AccessMode.CanRead;
            }

            if (accessModeString.Contains("w"))
            {
                accessModeEnum |= AccessMode.CanWrite;
            }

            return accessModeEnum;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
