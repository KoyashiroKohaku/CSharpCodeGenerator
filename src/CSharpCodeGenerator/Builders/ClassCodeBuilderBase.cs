using System;
using System.Collections.Generic;
using System.Linq;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public abstract class ClassCodeBuilderBase : CodeBuilder, IClassCodeBuilder
    {
        public new IClassCodeBuilder Indent()
        {
            base.Indent();

            return this;
        }

        public new IClassCodeBuilder Unindent()
        {
            base.Unindent();

            return this;
        }

        public new IClassCodeBuilder Append(string value)
        {
            base.Append(value);

            return this;
        }

        public new IClassCodeBuilder Append(ReadOnlySpan<char> value)
        {
            base.Append(value);

            return this;
        }

        public new IClassCodeBuilder AppendLine()
        {
            base.AppendLine();

            return this;
        }

        public new IClassCodeBuilder AppendLine(string value)
        {
            base.AppendLine(value);

            return this;
        }

        public new IClassCodeBuilder AppendLine(ReadOnlySpan<char> value)
        {
            base.AppendLine(value);

            return this;
        }

        public new IClassCodeBuilder AppendIndent()
        {
            base.AppendIndent();

            return this;
        }

        public new IClassCodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket)
        {
            base.AppendCurlyBracket(curlyBracket);

            return this;
        }

        public new IClassCodeBuilder AppendDocumentationComment()
        {
            base.AppendDocumentationComment();

            return this;
        }

        public new IClassCodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag)
        {
            base.AppendXmlCommentTag(tagName, xmlCommentTag);

            return this;
        }

        public new IClassCodeBuilder AppendUsingDirective(string namespaceString)
        {
            base.AppendUsingDirective(namespaceString);

            return this;
        }

        public new IClassCodeBuilder AppendNamespaceDeclaration(string namespaceName)
        {
            base.AppendNamespaceDeclaration(namespaceName);

            return this;
        }

        public abstract IClassCodeBuilder AppendClassDeclaration(string className);

        public abstract IClassCodeBuilder AppendField(PropertySetting propertySetting, FieldNamingConvention classFieldNamingConvention);

        public abstract IClassCodeBuilder AppendPropertyDeclaration(PropertySetting propertySetting);

        public abstract IClassCodeBuilder AppendAutoImplementedProperties(PropertySetting propertySetting);

        public static IEnumerable<string> ExtractNamespace(IEnumerable<PropertySetting> propertySettings)
        {
            if (propertySettings == null)
            {
                throw new ArgumentNullException(nameof(propertySettings));
            }

            return propertySettings
                .Where(p => !TypeResolver.ExistsTypeAlias(p.PropertyType))
                .Select(p => p.PropertyType.Namespace)
                .Distinct();
        }
    }
}
