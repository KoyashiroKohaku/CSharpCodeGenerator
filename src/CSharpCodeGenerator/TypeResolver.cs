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
                    _ => type.Name,
                };
            }
        }
    }
}
