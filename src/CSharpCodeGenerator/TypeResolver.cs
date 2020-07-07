using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class TypeResolver
    {
        public static string GetTypeAlias(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsGenericType)
            {
                var arguments = string.Join(", ", type.GetGenericArguments().Select(GetTypeAlias));

                return $"{type.Name[0..^2]}<{arguments}>";
            }
            else if (type.IsArray)
            {
                return $"{GetTypeAlias(type.GetElementType())}[]";
            }
            else
            {
                return type switch
                {
                    Type _ when typeof(System.Boolean).IsAssignableFrom(type) => "bool",
                    Type _ when typeof(System.Byte).IsAssignableFrom(type) => "byte",
                    Type _ when typeof(System.SByte).IsAssignableFrom(type) => "sbyte",
                    Type _ when typeof(System.Char).IsAssignableFrom(type) => "char",
                    Type _ when typeof(System.Decimal).IsAssignableFrom(type) => "decimal",
                    Type _ when typeof(System.Double).IsAssignableFrom(type) => "double",
                    Type _ when typeof(System.Single).IsAssignableFrom(type) => "float",
                    Type _ when typeof(System.Int32).IsAssignableFrom(type) => "int",
                    Type _ when typeof(System.UInt32).IsAssignableFrom(type) => "uint",
                    Type _ when typeof(System.Int64).IsAssignableFrom(type) => "long",
                    Type _ when typeof(System.UInt64).IsAssignableFrom(type) => "ulong",
                    Type _ when typeof(System.Int16).IsAssignableFrom(type) => "short",
                    Type _ when typeof(System.UInt16).IsAssignableFrom(type) => "ushort",
                    Type _ when typeof(System.String).IsAssignableFrom(type) => "string",
                    Type _ when typeof(System.Object).IsAssignableFrom(type) => "object",
                    _ => type.Name,
                };
            }
        }
    }
}
