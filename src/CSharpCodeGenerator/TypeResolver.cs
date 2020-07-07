using System;
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
                    Type _ when typeof(bool).IsAssignableFrom(type) => "bool",
                    Type _ when typeof(byte).IsAssignableFrom(type) => "byte",
                    Type _ when typeof(sbyte).IsAssignableFrom(type) => "sbyte",
                    Type _ when typeof(char).IsAssignableFrom(type) => "char",
                    Type _ when typeof(decimal).IsAssignableFrom(type) => "decimal",
                    Type _ when typeof(double).IsAssignableFrom(type) => "double",
                    Type _ when typeof(float).IsAssignableFrom(type) => "float",
                    Type _ when typeof(int).IsAssignableFrom(type) => "int",
                    Type _ when typeof(uint).IsAssignableFrom(type) => "uint",
                    Type _ when typeof(long).IsAssignableFrom(type) => "long",
                    Type _ when typeof(ulong).IsAssignableFrom(type) => "ulong",
                    Type _ when typeof(short).IsAssignableFrom(type) => "short",
                    Type _ when typeof(ushort).IsAssignableFrom(type) => "ushort",
                    Type _ when typeof(string).IsAssignableFrom(type) => "string",
                    Type _ when typeof(object).IsAssignableFrom(type) => "object",
                    _ => type.Name,
                };
            }
        }
    }
}
