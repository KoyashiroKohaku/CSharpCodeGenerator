using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public class ClassCodeBuilder : CodeBuilder, IClassCodeBuilder
    {
        public new ClassCodeBuilder Indent()
        {
            base.Indent();

            return this;
        }

        public new ClassCodeBuilder Unindent()
        {
            base.Unindent();

            return this;
        }

        public new ClassCodeBuilder Append(string value)
        {
            base.Append(value);

            return this;
        }

        public new ClassCodeBuilder Append(ReadOnlySpan<char> value)
        {
            base.Append(value);

            return this;
        }

        public new ClassCodeBuilder AppendLine()
        {
            base.AppendLine();

            return this;
        }

        public new ClassCodeBuilder AppendLine(string value)
        {
            base.AppendLine(value);

            return this;
        }

        public new ClassCodeBuilder AppendLine(ReadOnlySpan<char> value)
        {
            base.AppendLine(value);

            return this;
        }

        public new ClassCodeBuilder AppendIndent()
        {
            base.AppendIndent();

            return this;
        }

        public new ClassCodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
        {
            base.AppendCurlyBracket(curlyBracket);

            return this;
        }

        public new ClassCodeBuilder AppendDocumentationComment()
        {
            base.AppendDocumentationComment();

            return this;
        }

        public new ClassCodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag)
        {
            base.AppendXmlCommentTag(tagName, xmlCommentTag);

            return this;
        }

        public new ClassCodeBuilder AppendUsingDirective(string namespaceString)
        {
            base.AppendUsingDirective(namespaceString);

            return this;
        }

        public new ClassCodeBuilder AppendNamespaceDeclaration(string namespaceName)
        {
            base.AppendNamespaceDeclaration(namespaceName);

            return this;
        }

        public ClassCodeBuilder AppendClassDeclaration(string className)
        {
            if (string.IsNullOrEmpty(className))
            {
                return this;
            }

            Append("public class ").Append(className);

            return this;
        }

        public ClassCodeBuilder AppendField(ClassProperty property)
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

        public ClassCodeBuilder AppendPropertyDeclaration(ClassProperty property)
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

        public ClassCodeBuilder AppendAutoImplementedProperties(ClassProperty property)
        {
            if (property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            AppendPropertyDeclaration(property).Append(" { get; set; }");

            return this;
        }

        public static IEnumerable<string> ExtractNamespace(IEnumerable<ClassProperty> properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException(nameof(properties));
            }

            return properties
                .Where(p => !TypeResolver.ExistsTypeAlias(p.PropertyType))
                .Select(p => p.PropertyType.Namespace)
                .Distinct();
        }
    }
}
