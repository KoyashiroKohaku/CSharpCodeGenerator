using KoyashiroKohaku.CSharpCodeGenerator.Helpers;
using System;
using System.ComponentModel;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public class ClassCodeBuilder : ClassCodeBuilderBase
    {
        public override IClassCodeBuilder AppendClassDeclaration(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return this;
            }

            Append(TokenType.Public)
                .Append(TokenType.Space)
                .Append(TokenType.Class)
                .Append(TokenType.Space)
                .Append(className);

            return this;
        }

        public override IClassCodeBuilder AppendField(PropertySetting propertySetting, FieldNamingConvention classFieldNamingConvention)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            Append(TokenType.Private)
                .Append(TokenType.Space)
                .Append(TypeHelper.GetTypeString(propertySetting.PropertyType));

            if (propertySetting.Nullable)
            {
                Append(TokenType.QuestionMark);
            }

            Append(TokenType.Space);

            switch (propertySetting.FieldNamingConvention ?? classFieldNamingConvention)
            {
                case FieldNamingConvention.Camel:
                    Append(NameHelper.ToLowerCamel(propertySetting.PropertyName));
                    break;
                case FieldNamingConvention.CamelWithUnderscoreInThePrefix:
                    Append(NameHelper.ToLowerCamelWithUnderscore(propertySetting.PropertyName));
                    break;
                default:
                    if (propertySetting.FieldNamingConvention is null)
                    {
                        throw new InvalidEnumArgumentException(nameof(propertySetting.FieldNamingConvention), (int)classFieldNamingConvention, typeof(FieldNamingConvention));
                    }
                    else
                    {
                        throw new InvalidEnumArgumentException(nameof(propertySetting.FieldNamingConvention), (int)propertySetting.FieldNamingConvention, typeof(FieldNamingConvention));
                    }
            }

            Append(TokenType.Semicolon);

            return this;
        }

        public override IClassCodeBuilder AppendPropertyDeclaration(PropertySetting propertySetting)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            Append(TokenType.Public)
                .Append(TokenType.Space)
                .Append(TypeHelper.GetTypeString(propertySetting.PropertyType));

            if (propertySetting.Nullable)
            {
                Append(TokenType.QuestionMark);
            }

            Append(TokenType.Space).Append(propertySetting.PropertyName);

            return this;
        }

        public override IClassCodeBuilder AppendAutoImplementedProperties(PropertySetting propertySetting)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            AppendPropertyDeclaration(propertySetting)
                .Append(TokenType.Space)
                .Append(TokenType.LeftCurlyBracket)
                .Append(TokenType.Space)
                .Append(TokenType.Get)
                .Append(TokenType.Semicolon)
                .Append(TokenType.Space)
                .Append(TokenType.Set)
                .Append(TokenType.Semicolon)
                .Append(TokenType.Space)
                .Append(TokenType.RightCurlyBracket);

            return this;
        }
    }
}
