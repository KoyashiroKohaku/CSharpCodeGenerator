using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public interface IClassCodeBuilder : ICodeBuilder
    {
        new IClassCodeBuilder Indent();

        new IClassCodeBuilder Unindent();

        new IClassCodeBuilder Append(string value);

        new IClassCodeBuilder Append(ReadOnlySpan<char> value);

        new IClassCodeBuilder AppendLine();

        new IClassCodeBuilder AppendLine(string value);

        new IClassCodeBuilder AppendLine(ReadOnlySpan<char> value);

        new IClassCodeBuilder AppendIndent();

        new IClassCodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        new IClassCodeBuilder AppendDocumentationComment();

        new IClassCodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        new IClassCodeBuilder AppendUsingDirective(string namespaceString);

        new IClassCodeBuilder AppendNamespaceDeclaration(string namespaceName);

        IClassCodeBuilder AppendClassDeclaration(string className);

        IClassCodeBuilder AppendField(PropertySetting propertySettings, FieldNamingConvention classFieldNamingConvention);

        IClassCodeBuilder AppendPropertyDeclaration(PropertySetting propertySettings);

        IClassCodeBuilder AppendAutoImplementedProperties(PropertySetting propertySettings);
    }
}
