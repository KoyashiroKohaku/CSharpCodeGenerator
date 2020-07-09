using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class TypeResolver
    {
        private static Dictionary<Type, string> TypeAliasDictionary => new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(sbyte), "sbyte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(uint), "uint" },
            { typeof(long), "long" },
            { typeof(ulong), "ulong" },
            { typeof(short), "short" },
            { typeof(ushort), "ushort" },
            { typeof(string), "string" },
            { typeof(object), "object" },
            { typeof(void), "void" }
        };

        public static string GetTypeString(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (ExistsTypeAlias(type))
            {
                return GetTypeAliasName(type);
            }

            if (type.IsGenericType)
            {
                var genericTypeArguments = type.GenericTypeArguments;

                if (!genericTypeArguments.Any())
                {
                    throw new ArgumentException("The generic parameter is required for generic types.", nameof(type));
                }

                var arguments = string.Join(", ", genericTypeArguments.Select(GetTypeString));

                return $"{type.Name[0..^2]}<{arguments}>";
            }
            else if (type.IsArray)
            {
                return $"{GetTypeString(type.GetElementType())}[]";
            }
            else
            {
                return type.Name;
            }
        }

        public static string GetTypeAliasName(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!ExistsTypeAlias(type))
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            return TypeAliasDictionary[type];
        }

        public static bool TryGetTypeAliasName(Type type, out string? typeAliasName)
        {
            if (type == null)
            {
                typeAliasName = null;
                return false;
            }

            return TypeAliasDictionary.TryGetValue(type, out typeAliasName);
        }

        public static bool ExistsTypeAlias(Type type)
        {
            if (type == null)
            {
                return false;
            }

            return TypeAliasDictionary.ContainsKey(type);
        }
    }
}
