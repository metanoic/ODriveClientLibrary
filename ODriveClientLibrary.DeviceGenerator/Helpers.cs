namespace ODrive.DeviceGenerator
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using ODrive.DeviceGenerator.DeviceSchema;

    internal static class Helpers
    {
        public static string UnderscoreToCamelCase(string input)
        {
            return input.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(segment =>
                {
                    return char.ToUpperInvariant(segment[0]) + segment.Substring(1, segment.Length - 1);
                }).Aggregate(string.Empty, (segment1, segment2) => segment1 + segment2);
        }

        public static string ToPascalCase(string input)
        {
            input = UnderscoreToCamelCase(input);

            if (input == null) return input;
            if (input.Length < 2) return input.ToUpper();

            string[] words = input.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }

        public static string ToCamelCase(string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length > 1)
            {
                return Char.ToLowerInvariant(input[0]) + input.Substring(1);
            }
            return input;
        }

        public static string DataTypeToString(DataType type)
        {
            return GetAttribute<DescriptionAttribute>(type).Description;
        }

        public static T GetAttribute<T>(Enum enumValue) where T : Attribute
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
