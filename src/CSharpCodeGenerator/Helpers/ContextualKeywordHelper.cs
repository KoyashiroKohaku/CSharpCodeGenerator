using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class ContextualKeywordHelper
    {
        private static Dictionary<ContextualKeyword, string> ContextualKeywords => new Dictionary<ContextualKeyword, string>
        {
            { ContextualKeyword.Add, "add" },
            { ContextualKeyword.Alias, "alias" },
            { ContextualKeyword.Ascending, "ascending" },
            { ContextualKeyword.Async, "async" },
            { ContextualKeyword.Await, "await" },
            { ContextualKeyword.By, "by" },
            { ContextualKeyword.Descending, "descending" },
            { ContextualKeyword.Dynamic, "dynamic" },
            { ContextualKeyword.Equals, "equals" },
            { ContextualKeyword.From, "from" },
            { ContextualKeyword.Get, "get" },
            { ContextualKeyword.Global, "global" },
            { ContextualKeyword.Group, "group" },
            { ContextualKeyword.Into, "into" },
            { ContextualKeyword.Join, "join" },
            { ContextualKeyword.Let, "let" },
            { ContextualKeyword.Nameof, "nameof" },
            { ContextualKeyword.On, "on" },
            { ContextualKeyword.Orderby, "orderby" },
            { ContextualKeyword.PartialType, "partial" },
            { ContextualKeyword.PartialMethod, "partial" },
            { ContextualKeyword.Remove, "remove" },
            { ContextualKeyword.Select, "select" },
            { ContextualKeyword.Set, "set" },
            { ContextualKeyword.UnmanagedGenericTypeConstraint, "unmanaged" },
            { ContextualKeyword.Value, "value" },
            { ContextualKeyword.Var, "var" },
            { ContextualKeyword.WhenFilterCondition, "when" },
            { ContextualKeyword.WhereGenericTypeConstraint, "when" },
            { ContextualKeyword.WhereQueryClause, "where" },
            { ContextualKeyword.Yield, "yield" }
        };

        public static string GetValue(ContextualKeyword contextualKeyword)
        {
            if (!Enum.IsDefined(typeof(ContextualKeyword), contextualKeyword))
            {
                throw new InvalidEnumArgumentException(nameof(contextualKeyword), (int)contextualKeyword, typeof(ContextualKeyword));
            }

            return ContextualKeywords[contextualKeyword];
        }

        public static bool TryGetValue(ContextualKeyword contextualKeyword, [MaybeNullWhen(false)] out string value)
        {
            return ContextualKeywords.TryGetValue(contextualKeyword, out value);
        }

        public static bool IsContextualKeyword(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return ContextualKeywords.ContainsValue(value);
        }
    }
}
