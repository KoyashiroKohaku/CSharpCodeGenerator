using System;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class TypeResolver
    {
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

            return type switch
            {
                Type _ when typeof(bool) == type => "bool",
                Type _ when typeof(byte) == type => "byte",
                Type _ when typeof(sbyte) == type => "sbyte",
                Type _ when typeof(char) == type => "char",
                Type _ when typeof(decimal) == type => "decimal",
                Type _ when typeof(double) == type => "double",
                Type _ when typeof(float) == type => "float",
                Type _ when typeof(int) == type => "int",
                Type _ when typeof(uint) == type => "uint",
                Type _ when typeof(long) == type => "long",
                Type _ when typeof(ulong) == type => "ulong",
                Type _ when typeof(short) == type => "short",
                Type _ when typeof(ushort) == type => "ushort",
                Type _ when typeof(string) == type => "string",
                Type _ when typeof(object) == type => "object",
                Type _ when typeof(void) == type => "void",
                _ => throw new ArgumentOutOfRangeException(nameof(type)),
            };
        }

        public static bool TryGetTypeAliasName(Type type, out string typeAliasName)
        {
            if (type == null)
            {
                typeAliasName = string.Empty;
                return false;
            }

            if (!ExistsTypeAlias(type))
            {
                typeAliasName = string.Empty;
                return false;
            }

            typeAliasName = GetTypeAliasName(type);

            return true;
        }

        public static bool ExistsTypeAlias(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var types = new Type[]
            {
                typeof(bool),
                typeof(byte),
                typeof(sbyte),
                typeof(char),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(short),
                typeof(ushort),
                typeof(string),
                typeof(object),
                typeof(void)
            };

            return types.Contains(type);
        }
    }
}
