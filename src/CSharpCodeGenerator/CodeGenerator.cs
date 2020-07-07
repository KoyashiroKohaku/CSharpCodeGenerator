using System;
using System.Linq;
using KoyashiroKohaku.CSharpCodeGenerator.Builders;

namespace KoyashiroKohaku.CSharpCodeGenerator
{
    public static class CodeGenerator
    {
        public static string Generate
        (
            POCOClass pocoClass,
            IndentStyle indentStyle = IndentStyle.Space,
            int indentSize = 4,
            EndOfLine endOfLine = EndOfLine.CRLF
        )
        {
            if (pocoClass == null)
            {
                throw new ArgumentNullException(nameof(pocoClass));
            }

            var codeBuilder = new CodeBuilder(pocoClass)
            {
                IndentStyle = indentStyle,
                IndentSize = indentSize,
                EndOfLine = endOfLine
            };

            // namespace declaration (start)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder
                    .AppendNamespaceDeclaration()
                    .AppendCurlyBracket(CurlyBracket.Left)
                    .DownIndent();
            }

            // class declaration (start)
            codeBuilder
                .AppendClassDeclaration()
                .AppendCurlyBracket(CurlyBracket.Left)
                .DownIndent();

            // Properties
            if (pocoClass.Properties.Any())
            {
                codeBuilder.AppendProperties();
            }

            // class declaration (end)
            codeBuilder
                .UpIndent()
                .AppendCurlyBracket(CurlyBracket.Right);

            // namespace declaration (end)
            if (!string.IsNullOrEmpty(pocoClass.Namepace))
            {
                codeBuilder
                    .UpIndent()
                    .AppendCurlyBracket(CurlyBracket.Right);
            }

            return codeBuilder.ToString();
        }
    }
}
