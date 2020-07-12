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

            Append("public class ").Append(className);

            return this;
        }

        public override IClassCodeBuilder AppendField(PropertySetting propertySetting, FieldNamingConvention classFieldNamingConvention)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            Append("private ").Append(TypeHelper.GetTypeString(propertySetting.PropertyType));

            if (propertySetting.Nullable)
            {
                Append("?");
            }

            Append(" ");

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

            Append(";");

            return this;
        }

        public override IClassCodeBuilder AppendPropertyDeclaration(PropertySetting propertySetting)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            Append("public ").Append(TypeHelper.GetTypeString(propertySetting.PropertyType));

            if (propertySetting.Nullable)
            {
                Append("?");
            }

            Append(" ").Append(propertySetting.PropertyName);

            return this;
        }

        public override IClassCodeBuilder AppendAutoImplementedProperties(PropertySetting propertySetting)
        {
            if (propertySetting is null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            AppendPropertyDeclaration(propertySetting).Append(" { get; set; }");

            return this;
        }
    }
}
