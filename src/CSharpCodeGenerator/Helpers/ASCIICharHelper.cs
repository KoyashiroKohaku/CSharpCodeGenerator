using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class ASCIICharHelper
    {
        private static Dictionary<ASCIIChar, char> ASCIIChars => new Dictionary<ASCIIChar, char>
        {
            { ASCIIChar.Space, ' ' },
            { ASCIIChar.ExclamationMark, '!' },
            { ASCIIChar.QuotationMark, '"' },
            { ASCIIChar.NumberSign, '#' },
            { ASCIIChar.DollarSign, '$' },
            { ASCIIChar.PercentSign, '%' },
            { ASCIIChar.Ampersand, '&' },
            { ASCIIChar.Apostrophe, '\'' },
            { ASCIIChar.LeftParentheses, '(' },
            { ASCIIChar.RightParentheses, ')' },
            { ASCIIChar.Asterisk, '*' },
            { ASCIIChar.PlusSign, '+' },
            { ASCIIChar.Commma, ',' },
            { ASCIIChar.HyphenMinus, '-' },
            { ASCIIChar.FullStop, '.' },
            { ASCIIChar.Slash, '/' },
            { ASCIIChar.Colon, ':' },
            { ASCIIChar.Semicolon, ';' },
            { ASCIIChar.LessThanSign, '<' },
            { ASCIIChar.EqualSign, '=' },
            { ASCIIChar.GreaterThanSign, '>' },
            { ASCIIChar.QuestionMark, '?' },
            { ASCIIChar.AtSign, '@' },
            { ASCIIChar.LeftSquareBracket, '[' },
            { ASCIIChar.Backslash, '\\' },
            { ASCIIChar.RightSquareBracket, ']' },
            { ASCIIChar.CircumflexAccent, '^' },
            { ASCIIChar.LowLine, '_' },
            { ASCIIChar.GraveAccent, '`' },
            { ASCIIChar.LeftCurlyBracket, '{' },
            { ASCIIChar.VerticalBar, '|' },
            { ASCIIChar.RightCurlyBracket, '}' },
            { ASCIIChar.Tilde, '~' }
        };

        public static char GetValue(ASCIIChar asciiChar)
        {
            if (!Enum.IsDefined(typeof(ASCIIChar), asciiChar))
            {
                throw new InvalidEnumArgumentException(nameof(asciiChar), (int)asciiChar, typeof(ASCIIChar));
            }

            return ASCIIChars[asciiChar];
        }

        public static bool TryGetValue(ASCIIChar asciiChar, out char value)
        {
            return ASCIIChars.TryGetValue(asciiChar, out value);
        }

        public static bool IsASCIIChar(char value)
        {
            return ASCIIChars.ContainsValue(value);
        }

        public static bool IsASCIIChar(string value)
        {
            if (value is null)
            {
                return false;
            }

            if (value.Length != 1)
            {
                return false;
            }

            return ASCIIChars.ContainsValue(value[0]);
        }
    }
}
