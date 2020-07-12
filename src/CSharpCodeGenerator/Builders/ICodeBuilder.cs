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

        ICodeBuilder Indent();

        ICodeBuilder Unindent();

        ICodeBuilder Append(string value);

        ICodeBuilder AppendLine();

        ICodeBuilder AppendLine(string value);

        ICodeBuilder AppendIndent();

        ICodeBuilder AppendCurlyBracket(CurlyBracket curlyBracket);

        ICodeBuilder AppendDocumentationComment();

        ICodeBuilder AppendXmlCommentTag(string tagName, XmlCommentTag xmlCommentTag);

        ICodeBuilder AppendUsingDirective(string namespaceString);

        ICodeBuilder AppendNamespaceDeclaration(string namespaceName);
    }
}
