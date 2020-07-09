using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public interface IClassCodeBuilder : ICodeBuilder
    {
        new ClassCodeBuilder Indent();

        new ClassCodeBuilder Unindent();

        new ClassCodeBuilder Append(string value);

        new ClassCodeBuilder Append(ReadOnlySpan<char> value);

        new ClassCodeBuilder AppendLine();

        new ClassCodeBuilder AppendLine(string value);

        new ClassCodeBuilder AppendLine(ReadOnlySpan<char> value);

        new ClassCodeBuilder AppendIndent();

        new ClassCodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        new ClassCodeBuilder AppendDocumentationComment();

        new ClassCodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        new ClassCodeBuilder AppendUsingDirective(string namespaceString);

        new ClassCodeBuilder AppendNamespaceDeclaration(string namespaceName);

        ClassCodeBuilder AppendClassDeclaration(string className);

        ClassCodeBuilder AppendField(ClassProperty property);

        ClassCodeBuilder AppendPropertyDeclaration(ClassProperty property);

        ClassCodeBuilder AppendAutoImplementedProperties(ClassProperty property);
    }
}
