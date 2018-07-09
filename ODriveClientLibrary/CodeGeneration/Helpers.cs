namespace ODrive.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
    }
}
