using System;

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

        public override IClassCodeBuilder AppendField(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Append("private ").Append(TypeResolver.GetTypeString(property.PropertyType));

            if (property.Nullable)
            {
                Append("?");
            }

            Append(" ").Append(NameConverter.Convert(property.PropertyName, property.FieldNamingConvention)).Append(";");

            return this;
        }

        public override IClassCodeBuilder AppendPropertyDeclaration(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            Append("public ").Append(TypeResolver.GetTypeString(property.PropertyType));

            if (property.Nullable)
            {
                Append("?");
            }

            Append(" ").Append(property.PropertyName);

            return this;
        }

        public override IClassCodeBuilder AppendAutoImplementedProperties(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            AppendPropertyDeclaration(property).Append(" { get; set; }");

            return this;
        }
    }
}
