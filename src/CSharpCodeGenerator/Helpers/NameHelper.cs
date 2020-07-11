using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class NameHelper
    {
        private const string ValidStringRegex = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
        private const string LowerCamelRegex = @"^[a-z][a-z0-9]*(?:[A-Z][a-z0-9]*)*$";
        private const string UpperCamelRegex = @"^[A-Z][a-z0-9]*(?:[A-Z][a-z0-9]*)*$";
        private const string LowerSnakeRegex = @"^[a-z0-9]+(?:_[a-z0-9]+)*\z$";
        private const string UpperSnakeRegex = @"^[A-Z0-9]+(?:_[A-Z0-9]+)*\z$";

        public static bool IsValidString(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsValidString(input);
        }

        public static bool IsCamel(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsCamel(input);
        }

        public static bool IsLowerCamel(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsLowerCamel(input);
        }

        public static bool IsUpperCamel(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsUpperCamel(input);
        }

        public static bool IsSnake(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsSnake(input);
        }

        public static bool IsLowerSnake(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsLowerSnake(input);
        }

        public static bool IsUpperSnake(string input)
        {
            if (input is null)
            {
                return false;
            }

            return InternalIsUpperSnake(input);
        }

        private static bool InternalIsUpperSnake(string input)
        {
            return Regex.Match(input, UpperSnakeRegex).Success;
        }

        public static string[] Split(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            return InternalSplit(input);
        }

        public static string[] SplitCamel(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!IsCamel(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a camel case value.", nameof(input));
            }

            return InternalSplitCamel(input);
        }

        public static string[] SplitSnake(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!IsSnake(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a snake case value.", nameof(input));
            }

            return InternalSplitSnake(input);
        }

        public static Range[] SplitCamelIntoRange(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!IsCamel(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a camel case value.", nameof(input));
            }

            return InternalSplitCamelIntoRange(input).ToArray();
        }

        public static Range[] SplitSnakeIntoRange(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!IsSnake(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a snake case value.", nameof(input));
            }

            return InternalSplitSnakeIntoRange(input).ToArray();
        }

        public static string ToLowerCamel(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!InternalIsValidString(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a valdid string.", nameof(input));
            }

            return InternalToLowerCamel(input);
        }

        public static string ToUpperCamel(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!InternalIsValidString(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a valdid string.", nameof(input));
            }

            return InternalToUpperCamel(input);
        }

        public static string ToLowerSnake(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!InternalIsValidString(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a valdid string.", nameof(input));
            }

            return InternalToLowerSnake(input);
        }

        public static string ToUpperSnake(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (!InternalIsValidString(input))
            {
                throw new ArgumentException($"{nameof(input)} is not a valdid string.", nameof(input));
            }

            return InternalToUpperSnake(input);
        }

        private static bool InternalIsValidString(string input)
        {
            return Regex.Match(input, ValidStringRegex).Success;
        }

        private static bool InternalIsCamel(string input)
        {
            return InternalIsLowerCamel(input) || InternalIsUpperCamel(input);
        }

        private static bool InternalIsLowerCamel(string input)
        {
            return Regex.Match(input, LowerCamelRegex).Success;
        }

        private static bool InternalIsUpperCamel(string input)
        {
            return Regex.Match(input, UpperCamelRegex).Success;
        }

        private static bool InternalIsSnake(string input)
        {
            return InternalIsLowerSnake(input) || InternalIsUpperSnake(input);
        }

        private static bool InternalIsLowerSnake(string input)
        {
            return Regex.Match(input, LowerSnakeRegex).Success;
        }

        private static string[] InternalSplitCamel(ReadOnlySpan<char> input)
        {
            var ranges = InternalSplitCamelIntoRange(input);
            return InternalSplitByRanges(input, ranges);
        }

        public static string[] InternalSplit(string input)
        {
            if (InternalIsCamel(input))
            {
                return InternalSplitCamel(input);
            }

            if (InternalIsSnake(input))
            {
                return InternalSplitSnake(input);
            }

            throw new ArgumentException($"{nameof(input)} is neither a camel case nor a snke case.", nameof(input));
        }

        private static string[] InternalSplitSnake(ReadOnlySpan<char> input)
        {
            var ranges = InternalSplitSnakeIntoRange(input);
            return InternalSplitByRanges(input, ranges);
        }

        private static List<Range> InternalSplitCamelIntoRange(ReadOnlySpan<char> input)
        {
            var ranges = new List<Range>();
            var startIndex = 0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (char.IsLower(input[i]) && char.IsUpper(input[i + 1]))
                {
                    ranges.Add(startIndex..(i + 1));
                    startIndex = i + 1;
                }
            }
            ranges.Add(startIndex..input.Length);

            return ranges;
        }

        private static List<Range> InternalSplitSnakeIntoRange(ReadOnlySpan<char> input)
        {
            var ranges = new List<Range>();
            var startIndex = 0;

            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == '_')
                {
                    ranges.Add(startIndex..i);
                    startIndex = i + 1;
                }
            }

            if (startIndex != input.Length)
            {
                ranges.Add(startIndex..input.Length);
            }

            return ranges;
        }

        private static string[] InternalSplitByRanges(ReadOnlySpan<char> input, IList<Range> ranges)
        {
            var maxBufferSize = ranges.Select(r => r.End.Value - r.Start.Value).Max();
            Span<char> buffer = maxBufferSize <= 128 ? stackalloc char[maxBufferSize] : new char[maxBufferSize];

            string[] results = new string[ranges.Count];

            for (int i = 0; i < results.Length; i++)
            {
                var range = ranges[i];
                var rangeLength = range.End.Value - range.Start.Value;

                for (int j = 0; j < rangeLength; j++)
                {
                    buffer[j] = char.ToLower(input[range.Start.Value + j], new CultureInfo("en-US"));
                }

                results[i] = new string(buffer.Slice(0, rangeLength));
            }

            return results;
        }

        private static string InternalToLowerCamel(string input)
        {
            StringBuilder builder = new StringBuilder();

            var spritResults = InternalSplit(input);

            for (int i = 0; i < spritResults.Length; i++)
            {
                if (i == 0)
                {
                    builder.Append(spritResults[i]);
                }
                else
                {
                    builder.Append(char.ToUpper(spritResults[i][0], new CultureInfo("en-US")));
                    builder.Append(spritResults[i].AsSpan(1));
                }
            }

            return builder.ToString();
        }

        private static string InternalToUpperCamel(string input)
        {
            StringBuilder builder = new StringBuilder();

            var spritResults = InternalSplit(input);

            for (int i = 0; i < spritResults.Length; i++)
            {
                builder.Append(char.ToUpper(spritResults[i][0], new CultureInfo("en-US")));
                builder.Append(spritResults[i].AsSpan(1));
            }

            return builder.ToString();
        }

        private static string InternalToLowerSnake(string input)
        {
            var spritResults = InternalSplit(input);
            return string.Join('_', spritResults);
        }

        private static string InternalToUpperSnake(string input)
        {
            var ranges = InternalSplitCamelIntoRange(input);

            var maxBufferSize = ranges.Select(r => r.End.Value - r.Start.Value).Max();
            Span<char> buffer = maxBufferSize <= 128 ? stackalloc char[maxBufferSize] : new char[maxBufferSize];

            var builder = new StringBuilder();

            for (int i = 0; i < ranges.Count; i++)
            {
                var range = ranges[i];
                var rangeLength = range.End.Value - range.Start.Value;

                for (int j = 0; j < rangeLength; j++)
                {
                    buffer[j] = char.ToUpper(input[range.Start.Value + j], new CultureInfo("en-US"));
                }

                builder.Append(buffer.Slice(0, rangeLength));

                if (i != ranges.Count - 1)
                {
                    builder.Append('_');
                }
            }

            return builder.ToString();
        }
    }
}
