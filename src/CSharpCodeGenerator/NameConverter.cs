using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class NameConverter
    {
        public static string Convert(string input, FieldNamingConvention fieldNamingConvention)
        {
            return fieldNamingConvention switch
            {
                FieldNamingConvention.Camel => ToCamel(input),
                FieldNamingConvention.CamelWithUnderscoreInThePrefix => ToCamelWithUnderscoreInThePrefix(input),
                _ => throw new InvalidEnumArgumentException(nameof(fieldNamingConvention), (int)fieldNamingConvention, typeof(FieldNamingConvention))
            };
        }

        public static string ToCamel(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.Append(char.ToLower(input[0], new CultureInfo("en-US")));
            builder.Append(input[1..]);

            return builder.ToString();
        }

        public static string ToCamelWithUnderscoreInThePrefix(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length == 0)
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder();

            builder.Append('_');
            builder.Append(char.ToLower(input[0], new CultureInfo("en-US")));
            builder.Append(input[1..]);

            return builder.ToString();
        }
    }
}
