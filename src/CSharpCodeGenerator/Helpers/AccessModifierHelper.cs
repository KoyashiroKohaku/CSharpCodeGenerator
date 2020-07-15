using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace KoyashiroKohaku.CSharpCodeGenerator.Helpers
{
    public static class AccessModifierHelper
    {
        private static Dictionary<AccessModifier, string> AccessModifiers => new Dictionary<AccessModifier, string>
        {
            { AccessModifier.Public, KeywordHelper.GetValue(Keyword.Public) },
            { AccessModifier.Protected, KeywordHelper.GetValue(Keyword.Protected) },
            { AccessModifier.Internal, KeywordHelper.GetValue(Keyword.Internal) },
            { AccessModifier.ProtectedInternal, $"{KeywordHelper.GetValue(Keyword.Protected)} {KeywordHelper.GetValue(Keyword.Internal)}" },
            { AccessModifier.Private, KeywordHelper.GetValue(Keyword.Private) },
            { AccessModifier.PrivateProtected, $"{KeywordHelper.GetValue(Keyword.Private)} {KeywordHelper.GetValue(Keyword.Protected)}" }
        };

        public static string GetValue(AccessModifier accessModifier)
        {
            if (!Enum.IsDefined(typeof(AccessModifier), accessModifier))
            {
                throw new InvalidEnumArgumentException(nameof(accessModifier), (int)accessModifier, typeof(AccessModifier));
            }

            return AccessModifiers[accessModifier];
        }

        public static bool TryGetValue(AccessModifier accessModifier, [MaybeNullWhen(false)] out string value)
        {
            return AccessModifiers.TryGetValue(accessModifier, out value);
        }

        public static bool IsAccessModifier(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            return AccessModifiers.ContainsValue(value);
        }
    }
}
