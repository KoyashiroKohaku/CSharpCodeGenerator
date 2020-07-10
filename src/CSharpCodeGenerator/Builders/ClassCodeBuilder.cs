using System;
using KoyashiroKohaku.CSharpCodeGenerator.Helpers;

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
            if (propertySetting == null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            Append("private ").Append(TypeHelper.GetTypeString(propertySetting.PropertyType));

            if (propertySetting.Nullable)
            {
                Append("?");
            }

            Append(" ").Append(NameConverter.Convert(propertySetting.PropertyName, propertySetting.FieldNamingConvention ?? classFieldNamingConvention)).Append(";");

            return this;
        }

        public override IClassCodeBuilder AppendPropertyDeclaration(PropertySetting propertySetting)
        {
            if (propertySetting == null)
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
            if (propertySetting == null)
            {
                throw new ArgumentNullException(nameof(propertySetting));
            }

            AppendPropertyDeclaration(propertySetting).Append(" { get; set; }");

            return this;
        }
    }
}
