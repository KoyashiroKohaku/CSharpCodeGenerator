using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class NameHelper
    {
        private const string LowerCamelRegex = @"^[a-z][a-z0-9]*(?:[A-Z][a-z0-9]*)*$";
        private const string UpperCamelRegex = @"^[A-Z][a-z0-9]*(?:[A-Z][a-z0-9]*)*$";
        private const string LowerSnakeRegex = @"^[a-z0-9]+(?:_[a-z0-9]+)*\z$";
        private const string UpperSnakeRegex = @"^[A-Z0-9]+(?:_[A-Z0-9]+)*\z$";

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
    }
}
