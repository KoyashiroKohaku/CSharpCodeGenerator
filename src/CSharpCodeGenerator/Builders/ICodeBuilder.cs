using System;

namespace KoyashiroKohaku.CSharpCodeGenerator.Builders
{
    public interface ICodeBuilder
    {
        string CurrentIndentString { get; }

        string CurrentIndentStringWithDepth { get; }

        string CurrentEndOfLineString { get; }

        IndentStyle IndentStyle { get; set; }

        int IndentSize { get; set; }

        int IndentDepth { get; set; }

        EndOfLine EndOfLine { get; set; }

        CodeBuilder Indent();

        CodeBuilder Unindent();

        CodeBuilder Append(string value);

        CodeBuilder Append(ReadOnlySpan<char> value);

        CodeBuilder AppendLine();

        CodeBuilder AppendLine(string value);

        CodeBuilder AppendLine(ReadOnlySpan<char> value);

        CodeBuilder AppendIndent();

        CodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        CodeBuilder AppendDocumentationComment();

        CodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        CodeBuilder AppendUsingDirective(string namespaceString);

        CodeBuilder AppendNamespaceDeclaration(string namespaceName);
    }
}
