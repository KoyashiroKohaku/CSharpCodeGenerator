using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class KeywordHelper
    {
        /// <summary>
        /// Keywords dictionary.
        /// </summary>
        private static Dictionary<Keyword, string> Keywords => new Dictionary<Keyword, string>
        {
            { Keyword.Abstract, "abstract" },
            { Keyword.As, "as" },
            { Keyword.Base, "base" },
            { Keyword.Bool, "bool" },
            { Keyword.Break, "break" },
            { Keyword.Byte, "byte" },
            { Keyword.Case, "case" },
            { Keyword.Catch, "catch" },
            { Keyword.Char, "char" },
            { Keyword.Checked, "checked" },
            { Keyword.Class, "class" },
            { Keyword.Const, "const" },
            { Keyword.Continue, "continue" },
            { Keyword.Decimal, "decimal" },
            { Keyword.Default, "default" },
            { Keyword.Delegate, "delegate" },
            { Keyword.Do, "do" },
            { Keyword.Double, "double" },
            { Keyword.Else, "else" },
            { Keyword.Enum, "enum" },
            { Keyword.Event, "event" },
            { Keyword.Explicit, "explicit" },
            { Keyword.Extern, "extern" },
            { Keyword.False, "false" },
            { Keyword.Finally, "finally" },
            { Keyword.Fixed, "fixed" },
            { Keyword.Float, "float" },
            { Keyword.For, "for" },
            { Keyword.Foreach, "foreach" },
            { Keyword.Goto, "goto" },
            { Keyword.If, "if" },
            { Keyword.Implicit, "implicit" },
            { Keyword.In, "in" },
            { Keyword.Int, "int" },
            { Keyword.Interface, "interface" },
            { Keyword.Internal, "internal" },
            { Keyword.Is, "is" },
            { Keyword.Lock, "lock" },
            { Keyword.Long, "long" },
            { Keyword.Namespace, "namespace" },
            { Keyword.New, "new" },
            { Keyword.Null, "null" },
            { Keyword.Object, "object" },
            { Keyword.Operator, "operator" },
            { Keyword.Out, "out" },
            { Keyword.Override, "override" },
            { Keyword.Params, "params" },
            { Keyword.Private, "private" },
            { Keyword.Protected, "protected" },
            { Keyword.Public, "public" },
            { Keyword.Readonly, "readonly" },
            { Keyword.Ref, "ref" },
            { Keyword.Return, "return" },
            { Keyword.Sbyte, "sbyte" },
            { Keyword.Sealed, "sealed" },
            { Keyword.Short, "short" },
            { Keyword.Sizeof, "sizeof" },
            { Keyword.Stackalloc, "stackalloc" },
            { Keyword.Static, "static" },
            { Keyword.String, "string" },
            { Keyword.Struct, "struct" },
            { Keyword.Switch, "switch" },
            { Keyword.This, "this" },
            { Keyword.Throw, "throw" },
            { Keyword.True, "true" },
            { Keyword.Try, "try" },
            { Keyword.Typeof, "typeof" },
            { Keyword.Uint, "uint" },
            { Keyword.Ulong, "ulong" },
            { Keyword.Unchecked, "unchecked" },
            { Keyword.Unsafe, "unsafe" },
            { Keyword.Ushort, "ushort" },
            { Keyword.Using, "using" },
            { Keyword.Virtual, "virtual" },
            { Keyword.Void, "void" },
            { Keyword.Volatile, "volatile" },
            { Keyword.While, "while" }
        };

        public static bool IsKeyword(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return Keywords.ContainsValue(value);
        }

        public static string GetValue(Keyword keyword)
        {
            if (!Enum.IsDefined(typeof(Keyword), keyword))
            {
                throw new InvalidEnumArgumentException(nameof(keyword), (int)keyword, typeof(Keyword));
            }

            return Keywords[keyword];
        }

        public static bool TryGetValue(Keyword keyword, [MaybeNullWhen(false)] out string value)
        {
            return Keywords.TryGetValue(keyword, out value);
        }
    }
}
